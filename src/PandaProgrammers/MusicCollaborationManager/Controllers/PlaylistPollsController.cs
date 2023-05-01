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
using System.Dynamic;

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

        //(Should be) FINISHED.
        //[HttpGet("checkifpollexists/{username}/{playlistid}")]
        //public async Task<GeneralPollInfoDTO> CheckIfPollExists(string username, string playlistid)
        //{
        //    GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();
        //    Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(playlistid);

        //    if (ExistingPoll != null)
        //    {
        //        int totalPollVotes = 0;
        //        IEnumerable<OptionInfoDTO> PollOptions = await _pollsService.GetPollOptionsByPollID(playlistid);
        //        foreach (var option in PollOptions)
        //        {
        //            if (option.OptionText == "Yes")
        //            {

        //                totalPollVotes += option.OptionCount;
        //                InfoToReturn.YesVotes = option.OptionCount;
        //                InfoToReturn.YesOptionID = option.OptionID;
        //            }
        //            else if (option.OptionText == "No")
        //            {
        //                totalPollVotes += option.OptionCount;
        //                InfoToReturn.NoVotes = option.OptionCount;
        //                InfoToReturn.NoOptionID = option.OptionID;
        //            }
        //        }
        //        InfoToReturn.TotalPollVotes = totalPollVotes.ToString();

        //        FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(playlistid);
        //        InfoToReturn.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();

        //        FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(ExistingPoll.SpotifyTrackUri, SpotifyAuthService.GetTracksClientAsync());
        //        InfoToReturn.TrackArtist = PolledTrack.Id;
        //        InfoToReturn.TrackTitle = PolledTrack.Name;
        //        InfoToReturn.TrackDuration = PolledTrack.DurationMs.ToString(); //Currently in Milliseconds!


        //        //This "pollsService" method will return "null" if a vote does not exist.
        //        VoteIdentifierInfoDTO UserVote = await _pollsService.GetSpecificUserVoteForAGivenPlaylist(ExistingPoll.PollId, username);
        //        if (UserVote != null)
        //        {

        //            if (InfoToReturn.YesOptionID == UserVote.VoteID) //User voted "yes"
        //            {
        //                InfoToReturn.UserVotedYes = true;
        //            }
        //            else if (InfoToReturn.NoOptionID == UserVote.VoteID) //User voted "no"
        //            {
        //                InfoToReturn.UserVotedYes = false;
        //            }
        //        }
        //        else //User did NOT vote.
        //        {
        //            InfoToReturn.UserVotedYes = null;
        //        }
        //        return InfoToReturn;
        //    }
        //    return null;
        //}






        //(Should be) FINISHED (except js safeguard).
        [HttpPost("createpoll")]
        public async Task<ActionResult<GeneralPollInfoDTO>> CreateNewPoll([Bind("NewPollPlaylistId,NewPollTrackId, NewPollUsername")] PollCreationDTO newPollInput) //TrackID passed here (instead of "trackuri"). (Just haven't updated the name in DB yet.)
        {
            GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();

            Poll ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(newPollInput.NewPollPlaylistId);
            if (ExistingPoll == null) //No poll is in progress. Create one.
            {
                Poll NewPoll = new Poll();
                NewPoll.PollId = _pollsService.CreatePollForSpecificPlaylist(newPollInput.NewPollPlaylistId);
                NewPoll.SpotifyPlaylistId = newPollInput.NewPollPlaylistId;
                NewPoll.SpotifyTrackUri = newPollInput.NewPollTrackId;
                _playlistPollRepository.AddOrUpdate(NewPoll);


                int totalPollVotes = 0;
                IEnumerable<OptionInfoDTO> PollOptions = _pollsService.GetPollOptionsByPollID(NewPoll.PollId);
                foreach (var option in PollOptions)
                {
                    if (option.OptionText == "Yes")
                    {

                        totalPollVotes += option.OptionCount;
                        InfoToReturn.YesVotes = option.OptionCount;
                        InfoToReturn.YesOptionID = option.OptionID;
                    }
                    else if (option.OptionText == "No")
                    {
                        totalPollVotes += option.OptionCount;
                        InfoToReturn.NoVotes = option.OptionCount;
                        InfoToReturn.NoOptionID = option.OptionID;
                    }
                }
                InfoToReturn.TotalPollVotes = totalPollVotes.ToString();

                FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(newPollInput.NewPollPlaylistId);
                InfoToReturn.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();



                _pollsService.CreateVoteForTrack(NewPoll.PollId, InfoToReturn.YesOptionID, newPollInput.NewPollUsername);


                FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(newPollInput.NewPollTrackId, SpotifyAuthService.GetTracksClientAsync());
                InfoToReturn.TrackArtist = PolledTrack.Id;
                InfoToReturn.TrackTitle = PolledTrack.Name;
                InfoToReturn.TrackDuration = PolledTrack.DurationMs.ToString(); //Currently in Milliseconds!
                                                                                //---------




                InfoToReturn.UserVotedYes = true;


                //-----------

                int PlaylistFollowerCountAsInt = 0;
                try
                {
                    PlaylistFollowerCountAsInt = Int32.Parse(InfoToReturn.PlaylistFollowerCount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to parse playlist follower count. Defaulting to 0 as playlist follower count.");
                }


                if ((PlaylistFollowerCountAsInt == 0) || (PlaylistFollowerCountAsInt <= Int32.Parse(InfoToReturn.TotalPollVotes)))
                {

                    if (InfoToReturn.NoVotes < InfoToReturn.YesVotes)
                    {
                        List<string> TrackToAdd = new List<string>
                            {
                                PolledTrack.Uri
                            };
                        await _spotifyService.AddSongsToPlaylistAsync(CurPlaylist, TrackToAdd);
                    }

                    _playlistPollRepository.Delete(NewPoll);
                     _pollsService.RemovePoll(NewPoll.PollId);
                }

                return CreatedAtAction("Playlist", "createpoll", InfoToReturn);
            }
            return InfoToReturn; //JS does not have a safeguard in case "null" is the return value.
        }






        //(Should be) FINISHED. No js safeguard for "null" return.
        //[HttpPost("createvote")]
        //public async Task<GeneralPollInfoDTO> CreateVoteOnExistingPoll([Bind("CreateVotePlaylistId, CreateVoteUsername, CreateVoteOptionId")] SubmitVoteDTO userVote)
        //{

        //    GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();

        //    Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(userVote.CreateVotePlaylistId);
        //    if (ExistingPoll != null)
        //    {

        //        await _pollsService.CreateVoteForTrack(ExistingPoll.PollId, userVote.CreateVoteOptionId, userVote.CreateVoteUsername);

        //        //-----------------------------


        //        int totalPollVotes = 0;
        //        IEnumerable<OptionInfoDTO> PollOptions = await _pollsService.GetPollOptionsByPollID(userVote.CreateVotePlaylistId);
        //        foreach (var option in PollOptions)
        //        {
        //            if (option.OptionText == "Yes")
        //            {

        //                totalPollVotes += option.OptionCount;
        //                InfoToReturn.YesVotes = option.OptionCount;
        //                InfoToReturn.YesOptionID = option.OptionID;
        //            }
        //            else if (option.OptionText == "No")
        //            {
        //                totalPollVotes += option.OptionCount;
        //                InfoToReturn.NoVotes = option.OptionCount;
        //                InfoToReturn.NoOptionID = option.OptionID;
        //            }
        //        }
        //        InfoToReturn.TotalPollVotes = totalPollVotes.ToString();




        //        //---------------------------
        //        FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(ExistingPoll.SpotifyTrackUri, SpotifyAuthService.GetTracksClientAsync());
        //        InfoToReturn.TrackArtist = PolledTrack.Id;
        //        InfoToReturn.TrackTitle = PolledTrack.Name;
        //        InfoToReturn.TrackDuration = PolledTrack.DurationMs.ToString(); //Currently in Milliseconds!

        //        //-------------------

        //        FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(userVote.CreateVotePlaylistId);
        //        InfoToReturn.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();

        //        //----------------------------
        //        //This "pollsService" method will return "null" if a vote does not exist.
        //        VoteIdentifierInfoDTO UserVote = await _pollsService.GetSpecificUserVoteForAGivenPlaylist(ExistingPoll.PollId, userVote.CreateVoteUsername);
        //        if (InfoToReturn.YesOptionID == UserVote.VoteID) //User voted "yes"
        //        {
        //            InfoToReturn.UserVotedYes = true;
        //        }
        //        else if (InfoToReturn.NoOptionID == UserVote.VoteID) //User voted "no"
        //        {
        //            InfoToReturn.UserVotedYes = false;
        //        }


        //        //----------------------

        //        int PlaylistFollowerCountAsInt = 0;
        //        try
        //        {
        //            PlaylistFollowerCountAsInt = Int32.Parse(InfoToReturn.PlaylistFollowerCount);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine("Unable to parse playlist follower count. Defaulting to 0 as playlist follower count.");
        //        }


        //        if (PlaylistFollowerCountAsInt <= Int32.Parse(InfoToReturn.TotalPollVotes))
        //        {
        //            if (InfoToReturn.NoVotes < InfoToReturn.YesVotes)
        //            {
        //                List<string> TrackToAdd = new List<string>
        //                    {
        //                        PolledTrack.Uri
        //                    };
        //                await _spotifyService.AddSongsToPlaylistAsync(CurPlaylist, TrackToAdd);
        //            }


        //            _playlistPollRepository.Delete(ExistingPoll);
        //            await _pollsService.RemovePoll(ExistingPoll.PollId);
        //        }
        //        return InfoToReturn;
        //    }

        //    return null; //No js safeguard for "null" return.
        //}








        ////(Should be) FINISHED. No js safeguard for "null" return.
        //[HttpPost("removevote")]
        //public async Task<GeneralPollInfoDTO> RemoveVoteOnExistingPoll([Bind("RemoveVotePlaylistID, RemoveVoteUsername")] RemoveVoteDTO removeVoteInput)
        //{
        //    GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();
        //    Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(removeVoteInput.RemoveVotePlaylistID);
        //    if (ExistingPoll != null)
        //    {

        //        VoteIdentifierInfoDTO UserVote = await _pollsService.GetSpecificUserVoteForAGivenPlaylist(ExistingPoll.PollId, removeVoteInput.RemoveVoteUsername);
        //        await _pollsService.RemoveVote(UserVote.VoteID);

        //        //--------------------

        //        int totalPollVotes = 0;
        //        IEnumerable<OptionInfoDTO> PollOptions = await _pollsService.GetPollOptionsByPollID(removeVoteInput.RemoveVotePlaylistID);
        //        foreach (var option in PollOptions)
        //        {
        //            if (option.OptionText == "Yes")
        //            {

        //                totalPollVotes += option.OptionCount;
        //                InfoToReturn.YesVotes = option.OptionCount;
        //                InfoToReturn.YesOptionID = option.OptionID;
        //            }
        //            else if (option.OptionText == "No")
        //            {
        //                totalPollVotes += option.OptionCount;
        //                InfoToReturn.NoVotes = option.OptionCount;
        //                InfoToReturn.NoOptionID = option.OptionID;
        //            }
        //        }
        //        InfoToReturn.TotalPollVotes = totalPollVotes.ToString();

        //        //---------------------

        //        FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(ExistingPoll.SpotifyTrackUri, SpotifyAuthService.GetTracksClientAsync());
        //        InfoToReturn.TrackArtist = PolledTrack.Id;
        //        InfoToReturn.TrackTitle = PolledTrack.Name;
        //        InfoToReturn.TrackDuration = PolledTrack.DurationMs.ToString(); //Currently in Milliseconds!


        //        //-------------------------------------------

        //        FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(removeVoteInput.RemoveVotePlaylistID);
        //        InfoToReturn.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();


        //        //----------------------------
        //        InfoToReturn.UserVotedYes = null;

        //        //---------------------------


        //        return InfoToReturn;
        //    }
        //    return null; //No js safeguard for "null" return.
        //}
    }
}
