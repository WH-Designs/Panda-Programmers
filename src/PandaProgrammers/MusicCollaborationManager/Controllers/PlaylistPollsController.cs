using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.Services.Abstract;
using MusicCollaborationManager.Services.Concrete;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistPollsController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IListenerRepository _listenerRepository;
        private readonly SpotifyAuthService _spotifyService;
        private readonly IPollsService _pollsService;
        private readonly IPlaylistPollRepository _playlistPollRepository;

        public PlaylistPollsController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyAuthService spotifyService, IListenerRepository listenerRepository, 
            IPollsService pollsService, IPlaylistPollRepository playlistPollRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _spotifyService = spotifyService;
            _listenerRepository = listenerRepository;
            _pollsService = pollsService;
            _playlistPollRepository = playlistPollRepository;
        }

        public void ChangePollValues(GeneralPollInfoDTO infoToAlter)
        {
            infoToAlter.TrackArtist = "A COLORFUL HORSE!";
        }

        public async Task GetPollOptionsInfo(GeneralPollInfoDTO infoToAlter, string playlistid)
        {
            int totalPollVotes = 0;
            IEnumerable<OptionInfoDTO> PollOptions = await _pollsService.GetPollOptionsByPollID(playlistid);
            foreach (var option in PollOptions)
            {
                if (option.OptionText == "Yes")
                {

                    totalPollVotes += option.OptionCount;
                    infoToAlter.YesVotes = option.OptionCount;
                    infoToAlter.YesOptionID = option.OptionID;
                }
                else if (option.OptionText == "No")
                {
                    totalPollVotes += option.OptionCount;
                    infoToAlter.NoVotes = option.OptionCount;
                    infoToAlter.NoOptionID = option.OptionID;
                }
            }
            infoToAlter.TotalPollVotes = totalPollVotes.ToString();
        }

        public async Task GetPolledTrackInfo(GeneralPollInfoDTO infoToAlter, string playlistid)
        {
            FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(playlistid, SpotifyAuthService.GetTracksClientAsync());
            infoToAlter.TrackArtist = PolledTrack.Id;
            infoToAlter.TrackTitle = PolledTrack.Name;
            infoToAlter.TrackDuration = PolledTrack.DurationMs.ToString(); //Currently in MS!
        }

        public async Task GetPlaylistFollowerCount(GeneralPollInfoDTO infoToRetrieve,string playlistid)
        {
            FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(playlistid);
            infoToRetrieve.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();
        }

        public async Task GetUserVote(GeneralPollInfoDTO infoToRetrieve, Poll pollInfo, string username) 
        {
            //This "pollsService" method will return "null" if a vote does not exist.
            VoteIdentifierInfoDTO UserVote = await _pollsService.GetSpecificUserVoteForAGivenPlaylist(pollInfo.PollId, username);
            if (UserVote != null)
            {

                if (infoToRetrieve.YesOptionID == UserVote.VoteID) //User voted "yes"
                {
                    infoToRetrieve.UserVotedYes = true;
                }
                else if (infoToRetrieve.NoOptionID == UserVote.VoteID) //User voted "no"
                {
                    infoToRetrieve.UserVotedYes = false;
                }
            }
            else //User did NOT vote.
            {
                infoToRetrieve.UserVotedYes = null;
            }
        }

        //WARNING: The methods above only cover retrieving info. NONE of them cover ANY type of saving!

        //(Should be) FINISHED.
        [HttpGet("checkifpollexists/{username}/{playlistid}")]
        public async Task<GeneralPollInfoDTO> CheckIfPollExists(string username, string playlistid) 
        {
             GeneralPollInfoDTO PotentialExistingPoll = new GeneralPollInfoDTO();
             Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(playlistid);

            if(ExistingPoll != null) 
            {
                await GetPollOptionsInfo(PotentialExistingPoll, playlistid);
                await GetPlaylistFollowerCount(PotentialExistingPoll, playlistid);
                await GetPolledTrackInfo(PotentialExistingPoll, playlistid);
                await GetUserVote(PotentialExistingPoll, ExistingPoll, username);

                return PotentialExistingPoll;
            }

            return null;
        }


        //(Should be) FINISHED (except js safeguard).
        [HttpPost("createpoll")]
        public async Task<GeneralPollInfoDTO> CreateNewPoll([Bind("NewPollPlaylistId,NewPollTrackId, NewPollUsername")] PollCreationDTO newPollInput) //TrackID passed here (instead of "trackuri"). (Just haven't updated the name in DB yet.)
        {
                GeneralPollInfoDTO PotentialNewPoll = new GeneralPollInfoDTO();

                Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(newPollInput.NewPollPlaylistId);
                if (ExistingPoll == null) //No poll is in progress. Create one.
                {
                    Poll NewPoll = new Poll();
                    NewPoll.PollId = await _pollsService.CreatePollForSpecificPlaylist(newPollInput.NewPollPlaylistId);
                    NewPoll.SpotifyPlaylistId = newPollInput.NewPollPlaylistId;
                    NewPoll.SpotifyTrackUri = newPollInput.NewPollTrackId;
                    _playlistPollRepository.AddOrUpdate(NewPoll);
                    
                    //---
                    await GetPollOptionsInfo(PotentialNewPoll, newPollInput.NewPollPlaylistId);

                    FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(newPollInput.NewPollPlaylistId);
                    PotentialNewPoll.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();

                    await GetPlaylistFollowerCount(PotentialNewPoll, newPollInput.NewPollPlaylistId);

                    await _pollsService.CreateVoteForTrack(NewPoll.PollId, PotentialNewPoll.YesOptionID, newPollInput.NewPollUsername);

                    //This method does NOT have a safeguard if the vote does not exist, but the vote should exist (because we just created one).
                    await GetUserVote(PotentialNewPoll, NewPoll, newPollInput.NewPollUsername);

                    int PlaylistFollowerCountAsInt = 0;
                    try
                    {
                        PlaylistFollowerCountAsInt = Int32.Parse(PotentialNewPoll.PlaylistFollowerCount);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to parse playlist follower count. Defaulting to 0 as playlist follower count.");
                    }
                    
                    
                    if ((PlaylistFollowerCountAsInt == 0) || (PlaylistFollowerCountAsInt >= Int32.Parse(PotentialNewPoll.TotalPollVotes)))
                    {
                        _playlistPollRepository.Delete(NewPoll);
                        await _pollsService.RemovePoll(NewPoll.PollId);
                    }


                    return PotentialNewPoll;
                    
                }
                return null; //JS does not have a safeguard in case "null" is the return value.
        }


        //(Should be) FINISHED .NO js safeguard for "null" return.
        [HttpPost("createvote")]
        public async Task<GeneralPollInfoDTO> CreateVoteOnExistingPoll([Bind("CreateVotePlaylistId, CreateVoteUsername, CreateVoteOptionId")] SubmitVoteDTO userVote)
        {

            GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();

            Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(userVote.CreateVotePlaylistId);
            if(ExistingPoll != null) 
            {

                await _pollsService.CreateVoteForTrack(ExistingPoll.PollId, userVote.CreateVoteOptionId, userVote.CreateVoteUsername);


                await GetPollOptionsInfo(InfoToReturn, userVote.CreateVotePlaylistId);
                await GetPolledTrackInfo(InfoToReturn, userVote.CreateVotePlaylistId);
                await GetPlaylistFollowerCount(InfoToReturn, userVote.CreateVotePlaylistId);
                await GetUserVote(InfoToReturn, ExistingPoll, userVote.CreateVoteUsername);

                int PlaylistFollowerCountAsInt = 0;
                try
                {
                    PlaylistFollowerCountAsInt = Int32.Parse(InfoToReturn.PlaylistFollowerCount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to parse playlist follower count. Defaulting to 0 as playlist follower count.");
                }


                if (PlaylistFollowerCountAsInt >= Int32.Parse(InfoToReturn.TotalPollVotes))
                {
                    _playlistPollRepository.Delete(ExistingPoll);
                    await _pollsService.RemovePoll(ExistingPoll.PollId);
                }
            }

            return null;
        }

        [HttpPost("removevote")]
        public async Task<GeneralPollInfoDTO> RemoveVoteOnExistingPoll([Bind("RemoveVotePlaylistID, RemoveVoteUsername")] RemoveVoteDTO removeVoteInput)
        {
            GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();
            Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(removeVoteInput.RemoveVotePlaylistID);
            if(ExistingPoll != null) 
            {
                
            }
            return null;
        }
    }
}
