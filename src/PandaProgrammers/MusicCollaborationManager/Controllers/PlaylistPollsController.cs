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

        //[HttpGet("checkcuruservote")]
        //public async Task<string?> CheckCurUserVote() 
        //{

        //}



        [HttpPost("createpoll")]
        public async Task<NewPlaylistPollDTO> CreateNewPoll([Bind("NewPollPlaylistId,NewPollTrackId")] PollCreationDTO newPollInput) //TrackID passed here (instead of "trackuri"). (Just haven't updated the name in DB yet.)
        {
                NewPlaylistPollDTO PotentialNewPoll = new NewPlaylistPollDTO();

                Poll? NewPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(newPollInput.NewPollPlaylistId);
                if (NewPoll == null)
                {

                    //string NewPollID = await _pollsService.CreatePollForSpecificPlaylist(playlistId);
                    //string NewPollID = await _pollsService.CreatePollForSpecificPlaylist(newPollInput.PlaylistID);


                    //Debugging (below)------------
                    PotentialNewPoll.TrackArtist = newPollInput.NewPollPlaylistId;
                    PotentialNewPoll.TrackTitle = "PRACTICE TRACK #1";
                    PotentialNewPoll.TrackDuration = "4 MIN";
                    PotentialNewPoll.YesOptionID = "#1234_YES";
                    PotentialNewPoll.NoOptionID = "#5678_NO";
                    PotentialNewPoll.TotalPollVotes = "4";

                   return PotentialNewPoll;
                    

                    //Debugging (Above)-----------

                    //NewPoll.PollId = NewPollID;
                    //NewPoll.SpotifyPlaylistId = newPollInput.PlaylistID;
                    //NewPoll.SpotifyTrackUri = newPollInput.TrackID;
              
                    //_playlistPollRepository.AddOrUpdate(NewPoll);
                    //IEnumerable<OptionInfoDTO> PollOptions = await _pollsService.GetPollOptionsByPollID(NewPollID);

                    //FullTrack TrackBeingPolled = await _spotifyService.GetSpotifyTrackByID(newPollInput.TrackID, SpotifyAuthService.GetTracksClientAsync());
                    //PotentialNewPoll.TrackArtist = TrackBeingPolled.Artists[0].Name;
                    //PotentialNewPoll.TrackTitle = TrackBeingPolled.Name;
                    //PotentialNewPoll.TrackDuration = TrackBeingPolled.DurationMs.ToString(); //Convert to minutes later if possible.

                    //foreach(var option in PollOptions) 
                    //{
                    //    if(option.OptionText == "Yes") 
                    //    {
                    //        PotentialNewPoll.YesOptionID = option.OptionID;
                    //    }
                    //    if(option.OptionText == "No") 
                    //    {
                    //        PotentialNewPoll.NoOptionID = option.OptionID;
                    //    }
                    //    PotentialNewPoll.TotalPollVotes += option.OptionCount;
                    //}
                    //return PotentialNewPoll;
                }
                return null;
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

        [HttpPost("createvote/{playlistid}/{optionid}/{username}")]
        public async Task<bool?> CreateVoteOnExistingPoll(string playlistid, string optionID, string username)
        {
            bool? trackAddedToPlaylist = false;
            try 
            {
                Poll PollInfo = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(playlistid);
                IEnumerable<OptionInfoDTO> PollOptions = await _pollsService.GetPollOptionsByPollID(PollInfo.PollId);
                await _pollsService.CreateVoteForTrack(PollInfo.PollId, optionID, username);

                
                //NEED LOGIC HERE TO CHECK IF NUM FOLLOWERS IS EQUAL TO NUM VOTES. (If so, END the poll).
                FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(playlistid);
                

                if(CurPlaylist.Followers.Total == PollOptions.ToList().Count()) 
                {
                    int yesCount = 0;
                    int noCount = 0;
                    foreach(var pollingoption in PollOptions) 
                    {
                        if(pollingoption.OptionText == "Yes") 
                        {
                            yesCount = pollingoption.OptionCount;
                        }
                        else if(pollingoption.OptionText == "No") 
                        {
                            noCount = pollingoption.OptionCount;
                        }
                    }
                    if (yesCount > noCount)
                    {
                        FullTrack TrackBeingVoted = await _spotifyService.GetSpotifyTrackByID(PollInfo.SpotifyTrackUri, SpotifyAuthService.GetTracksClientAsync());
                        List<string> TrackToAddAsList = new List<string>
                        {
                            PollInfo.SpotifyTrackUri
                        };

                        await _spotifyService.AddSongsToPlaylistAsync(CurPlaylist, TrackToAddAsList);
                        trackAddedToPlaylist = true;
                    }
                    await _pollsService.RemovePoll(playlistid);
                    _playlistPollRepository.Delete(PollInfo);
                }
                return trackAddedToPlaylist;
            }
            catch
            {
                trackAddedToPlaylist = null;
                return trackAddedToPlaylist ;
            }
        }

        [HttpPost("removevote")]
        public async Task<NewPlaylistPollDTO> RemoveVoteOnExistingPoll(string RemoveVotePlaylistID, string RemoveVoteUsername)
        {
            NewPlaylistPollDTO ExistingPoll = new NewPlaylistPollDTO();
            ExistingPoll.TrackArtist = "Generic artist #55";
            ExistingPoll.TrackTitle = "PRACTICE TRACK #1";
            ExistingPoll.TrackDuration = "4 MIN";
            ExistingPoll.YesOptionID = "#1234_YES";
            ExistingPoll.NoOptionID = "#5678_NO";
            ExistingPoll.TotalPollVotes = "4";

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
