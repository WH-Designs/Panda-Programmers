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
                    await GetPolledTrackInfo(PotentialNewPoll, newPollInput.NewPollPlaylistId);
                    await GetPlaylistFollowerCount(PotentialNewPoll, newPollInput.NewPollPlaylistId);

                    await _pollsService.CreateVoteForTrack(NewPoll.PollId, PotentialNewPoll.YesOptionID, newPollInput.NewPollUsername);

                    //This method does NOT have a safeguard if the vote does not exist, but the vote should exist (because we just created one).
                    await GetUserVote(PotentialNewPoll, NewPoll, newPollInput.NewPollUsername);


                    return PotentialNewPoll;
                    
                }
                return null; //JS does not have a safeguard in case "null" is the return value.
        }


        //Old version (below)---

        ////NEEDS TESTING
        //[HttpPost("createpoll/{playlistid}/{trackuri}")]
        //public async Task<IEnumerable<OptionInfoDTO>> CreateNewPoll(string playlistid, string trackuri) //TrackID passed here (instead of "trackuri"). (Just haven't update the name in DB yet.)
        //{
        //    try 
        //    {
        //        Poll? NewPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(playlistid);
        //        if (NewPoll == null) 
        //        {
        //            string? NewPollID = await _pollsService.CreatePollForSpecificPlaylist(playlistid);

        //            NewPoll.PollId = NewPollID;
        //            NewPoll.SpotifyPlaylistId = playlistid;
        //            NewPoll.SpotifyTrackUri = trackuri;

        //            _playlistPollRepository.AddOrUpdate(NewPoll);
        //            return await _pollsService.GetPollOptionsByPollID(NewPollID);
        //        }
        //        return Enumerable.Empty<OptionInfoDTO>();

        //    }
        //    catch (Exception ex) 
        //    {
        //        return Enumerable.Empty<OptionInfoDTO>();
        //    }
        // }

        //Should give user option for enabling a vote AND cover the possibility of the poll ending HERE.
        //[HttpPost("createvote")]
        //public async Task<bool?> CreateVoteOnExistingPoll(string playlistid, string optionID, string username)
        //{
        //    bool? trackAddedToPlaylist = false;
        //    try 
        //    {
        //        Poll PollInfo = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(playlistid);
        //        IEnumerable<OptionInfoDTO> PollOptions = await _pollsService.GetPollOptionsByPollID(PollInfo.PollId);
        //        await _pollsService.CreateVoteForTrack(PollInfo.PollId, optionID, username);


        //        //NEED LOGIC HERE TO CHECK IF NUM FOLLOWERS IS EQUAL TO NUM VOTES. (If so, END the poll).
        //        FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(playlistid);


        //        if(CurPlaylist.Followers.Total == PollOptions.ToList().Count()) 
        //        {
        //            int yesCount = 0;
        //            int noCount = 0;
        //            foreach(var pollingoption in PollOptions) 
        //            {
        //                if(pollingoption.OptionText == "Yes") 
        //                {
        //                    yesCount = pollingoption.OptionCount;
        //                }
        //                else if(pollingoption.OptionText == "No") 
        //                {
        //                    noCount = pollingoption.OptionCount;
        //                }
        //            }
        //            if (yesCount > noCount)
        //            {
        //                FullTrack TrackBeingVoted = await _spotifyService.GetSpotifyTrackByID(PollInfo.SpotifyTrackUri, SpotifyAuthService.GetTracksClientAsync());
        //                List<string> TrackToAddAsList = new List<string>
        //                {
        //                    PollInfo.SpotifyTrackUri
        //                };

        //                await _spotifyService.AddSongsToPlaylistAsync(CurPlaylist, TrackToAddAsList);
        //                trackAddedToPlaylist = true;
        //            }
        //            await _pollsService.RemovePoll(playlistid);
        //            _playlistPollRepository.Delete(PollInfo);
        //        }
        //        return trackAddedToPlaylist;
        //    }
        //    catch
        //    {
        //        trackAddedToPlaylist = null;
        //        return trackAddedToPlaylist ;
        //    }
        //}

        [HttpPost("createvote")]
        public async Task<GeneralPollInfoDTO> CreateVoteOnExistingPoll([Bind("CreateVotePlaylistId, CreateVoteUsername, CreateVoteOptionId")] SubmitVoteDTO userVote)
        {
            int numFollowersServerSide = 5; //Just pretending this is the actual follower count from spotify's end.
            int YesVote = 2;
            int NoVote = 2;
            if (userVote.CreateVoteOptionId == "1234")
            {
                YesVote++;
            }
            else if(userVote.CreateVoteOptionId == "5678")
            {
                NoVote++;
            }

            GeneralPollInfoDTO ExistingPoll = new GeneralPollInfoDTO();


            //THIS CURRENTLY DOES NOT COVER SHOWING WHAT THE USER VOTED!
            if (numFollowersServerSide >= 5) 
            {
                ExistingPoll.TrackArtist = "PlaylistID: " + userVote.CreateVotePlaylistId;
                ExistingPoll.TrackTitle = "PRACTICE TRACK #100___Created_by_'removevote'";
                ExistingPoll.TrackDuration = "CUR_USER_IS: " + userVote.CreateVoteUsername;
                ExistingPoll.YesOptionID = "#1234_YES";
                ExistingPoll.NoOptionID = "#5678_NO";
                ExistingPoll.TotalPollVotes = "5";
                ExistingPoll.PlaylistFollowerCount = "5";
                ExistingPoll.YesVotes = YesVote;
                ExistingPoll.NoVotes = NoVote;

                return ExistingPoll;
            }
            else
            {
                ExistingPoll.UserVotedYes = false; //"False" displays correct value. "True" display correct value.//"null" display the correct value. "null" is meant for the "checkifpollexists" method. 
                ExistingPoll.TrackArtist = "PlaylistID: " + userVote.CreateVotePlaylistId;
                ExistingPoll.TrackTitle = "PRACTICE TRACK #100___Created_by_'removevote'";
                ExistingPoll.TrackDuration = "CUR_USER_IS: " + userVote.CreateVoteUsername;
                ExistingPoll.YesOptionID = "#1234_YES";
                ExistingPoll.NoOptionID = "#5678_NO";
                ExistingPoll.TotalPollVotes = "2";
                ExistingPoll.PlaylistFollowerCount = "5";

                return ExistingPoll;
            }
        }

        [HttpPost("removevote")]
        public async Task<GeneralPollInfoDTO> RemoveVoteOnExistingPoll([Bind("RemoveVotePlaylistID, RemoveVoteUsername")] RemoveVoteDTO removeVoteInput)
        {
            GeneralPollInfoDTO ExistingPoll = new GeneralPollInfoDTO();
            ExistingPoll.TrackArtist = "PlaylistID: " + removeVoteInput.RemoveVotePlaylistID;
            ExistingPoll.TrackTitle = "PRACTICE TRACK #100___Created_by_'removevote'";
            ExistingPoll.TrackDuration = "CUR_USER_IS: " + removeVoteInput.RemoveVoteUsername;
            ExistingPoll.YesOptionID = "#1234_YES";
            ExistingPoll.NoOptionID = "#5678_NO";
            ExistingPoll.TotalPollVotes = "4";
            ExistingPoll.PlaylistFollowerCount = "EMPTY_ON_PURPOSE";

            //bool successfulVoteRemoval = true;
            //try
            //{
            //    Poll PollInfo = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(RemoveVotePlaylistID);
            //    VoteIdentifierInfoDTO CurUserVote = await _pollsService.GetSpecificUserVoteForAGivenPlaylist(PollInfo.PollId, RemoveVoteUsername);
            //    await _pollsService.RemoveVote(CurUserVote.VoteID);
            //    return successfulVoteRemoval;
            //}
            //catch(Exception ex) 
            //{
            //    successfulVoteRemoval = false;
            //    return successfulVoteRemoval;
            //}

            return ExistingPoll;

            /*
             * Need to return:
             * -Options
             */
        }
    }
}
