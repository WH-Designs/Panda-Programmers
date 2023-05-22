﻿using Google.Apis.YouTube.v3.Data;
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
        [HttpGet("checkifpollexists/{username}/{playlistid}")]
        public async Task<GeneralPollInfoDTO> CheckIfPollExists(string username, string playlistid)
        {

            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();
            Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(playlistid);

            if (ExistingPoll != null)
            {
                int totalPollVotes = 0;
                IEnumerable<OptionInfoDTO> PollOptions = _pollsService.GetPollOptionsByPollID(ExistingPoll.PollId);
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

                FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(playlistid, spotifyClient);
                InfoToReturn.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();

                FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(ExistingPoll.SpotifyTrackUri, SpotifyAuthService.GetTracksClientAsync(spotifyClient));
                InfoToReturn.TrackArtist = PolledTrack.Artists[0].Name;
                InfoToReturn.TrackTitle = PolledTrack.Name;
                InfoToReturn.TrackDuration = PolledTrack.DurationMs.ToString(); //Currently in Milliseconds!


                //This "pollsService" method will return "null" if a vote does not exist.
                VoteIdentifierInfoDTO UserVote = _pollsService.GetSpecificUserVoteForAGivenPlaylist(ExistingPoll.PollId, username);
                if (UserVote != null)
                {

                    if (InfoToReturn.YesOptionID == UserVote.OptionID) //User voted "yes"
                    {
                        InfoToReturn.UserVotedYes = true;
                    }
                    else if (InfoToReturn.NoOptionID == UserVote.OptionID) //User voted "no"
                    {
                        InfoToReturn.UserVotedYes = false;
                    }
                }
                else //User did NOT vote.
                {
                    InfoToReturn.UserVotedYes = null;
                }

                //Simulating that playlist has 2 followers (below)------
                //PlaylistFollowerCountAsInt = 2;
                //InfoToReturn.PlaylistFollowerCount = "2";

                //Simulating that playlist has 2 followers (above)------
                return InfoToReturn;
            }
            return null;
        }






        //(Should be) FINISHED (except js safeguard).
        [HttpPost("createpoll")]
        public async Task<ActionResult<GeneralPollInfoDTO>> CreateNewPoll([Bind("NewPollPlaylistId,NewPollTrackId, NewPollUsername")] PollCreationDTO newPollInput) //TrackID passed here (instead of "trackuri"). (Just haven't updated the name in DB yet.)
        {

            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

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
                 _pollsService.CreateVoteForTrack(NewPoll.PollId, InfoToReturn.YesOptionID, newPollInput.NewPollUsername);
                InfoToReturn.TotalPollVotes = "1";

                FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(newPollInput.NewPollPlaylistId, spotifyClient);
                InfoToReturn.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();



               


                FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(newPollInput.NewPollTrackId, SpotifyAuthService.GetTracksClientAsync(spotifyClient));
                InfoToReturn.TrackArtist = PolledTrack.Artists[0].Name;
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
                catch (Exception)
                {
                    Console.WriteLine("Unable to parse playlist follower count. Defaulting to 0 as playlist follower count.");
                }


                //Simulating that playlist has 2 followers (below)------
                //PlaylistFollowerCountAsInt = 2;
                //InfoToReturn.PlaylistFollowerCount = "2";

                //Simulating that playlist has 2 followers (above)------
                if (PlaylistFollowerCountAsInt <= Int32.Parse(InfoToReturn.TotalPollVotes))
                {

                    if (InfoToReturn.NoVotes < InfoToReturn.YesVotes)
                    {
                        List<string> TrackToAdd = new List<string>
                            {
                                PolledTrack.Uri
                            };
                        await _spotifyService.AddSongsToPlaylistAsync(CurPlaylist, TrackToAdd, spotifyClient);
                    }

                    _playlistPollRepository.Delete(NewPoll);
                     _pollsService.RemovePoll(NewPoll.PollId);
                }

                return CreatedAtAction("Playlist", "createpoll", InfoToReturn);
            }
            return InfoToReturn; //JS does not have a safeguard in case "null" is the return value.
        }





        //NEEDS TESTING W/ REAL DEAL.
        //(Should be) FINISHED. No js safeguard for "null" return.
        [HttpPost("createvote")]
        public async Task<GeneralPollInfoDTO> CreateVoteOnExistingPoll([Bind("CreateVotePlaylistId, CreateVoteUsername, CreateVoteOptionId")] SubmitVoteDTO userVote)
        {


            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();

            Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(userVote.CreateVotePlaylistId);
            if (ExistingPoll != null)
            {

                _pollsService.CreateVoteForTrack(ExistingPoll.PollId, userVote.CreateVoteOptionId, userVote.CreateVoteUsername);

                //-----------------------------


                int totalPollVotes = 0;
                IEnumerable<OptionInfoDTO> PollOptions = _pollsService.GetPollOptionsByPollID(ExistingPoll.PollId);
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




                //---------------------------
                FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(ExistingPoll.SpotifyTrackUri, SpotifyAuthService.GetTracksClientAsync(spotifyClient));
                InfoToReturn.TrackArtist = PolledTrack.Artists[0].Name;
                InfoToReturn.TrackTitle = PolledTrack.Name;
                InfoToReturn.TrackDuration = PolledTrack.DurationMs.ToString(); //Currently in Milliseconds!

                //-------------------

                FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(userVote.CreateVotePlaylistId, spotifyClient);
                InfoToReturn.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();

                //----------------------------
                //This "pollsService" method will return "null" if a vote does not exist.
                VoteIdentifierInfoDTO UserVote = _pollsService.GetSpecificUserVoteForAGivenPlaylist(ExistingPoll.PollId, userVote.CreateVoteUsername);
                if (InfoToReturn.YesOptionID == UserVote.OptionID) //User voted "yes"
                {
                    InfoToReturn.UserVotedYes = true;
                }
                else if (InfoToReturn.NoOptionID == UserVote.OptionID) //User voted "no"
                {
                    InfoToReturn.UserVotedYes = false;
                }


                //----------------------

                int PlaylistFollowerCountAsInt = 0;
                try
                {
                    PlaylistFollowerCountAsInt = Int32.Parse(InfoToReturn.PlaylistFollowerCount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to parse playlist follower count. Defaulting to 0 as playlist follower count.");
                }

                //Simulating that playlist has 2 followers (below)------
                //PlaylistFollowerCountAsInt = 2;
                //InfoToReturn.PlaylistFollowerCount = "2";

                //Simulating that playlist has 2 followers (above)------


                if (PlaylistFollowerCountAsInt <= Int32.Parse(InfoToReturn.TotalPollVotes))
                {
                    if (InfoToReturn.NoVotes < InfoToReturn.YesVotes)
                    {
                        List<string> TrackToAdd = new List<string>
                            {
                                PolledTrack.Uri
                            };
                        await _spotifyService.AddSongsToPlaylistAsync(CurPlaylist, TrackToAdd, spotifyClient);
                    }


                    _playlistPollRepository.Delete(ExistingPoll);
                    _pollsService.RemovePoll(ExistingPoll.PollId);
                }
                return InfoToReturn;
            }

            return null; //No js safeguard for "null" return.
        }








        ////(Should be) FINISHED. No js safeguard for "null" return.
        [HttpPost("removevote")]
        public async Task<GeneralPollInfoDTO> RemoveVoteOnExistingPoll([Bind("RemoveVotePlaylistID, RemoveVoteUsername")] RemoveVoteDTO removeVoteInput)
        {

            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            GeneralPollInfoDTO InfoToReturn = new GeneralPollInfoDTO();
            Poll? ExistingPoll = _playlistPollRepository.GetPollDetailsBySpotifyPlaylistID(removeVoteInput.RemoveVotePlaylistID);
            if (ExistingPoll != null)
            {

                VoteIdentifierInfoDTO UserVote = _pollsService.GetSpecificUserVoteForAGivenPlaylist(ExistingPoll.PollId, removeVoteInput.RemoveVoteUsername);
                _pollsService.RemoveVote(UserVote.VoteID);

                //--------------------

                int totalPollVotes = 0;
                IEnumerable<OptionInfoDTO> PollOptions = _pollsService.GetPollOptionsByPollID(ExistingPoll.PollId);
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

                //---------------------

                FullTrack PolledTrack = await _spotifyService.GetSpotifyTrackByID(ExistingPoll.SpotifyTrackUri, SpotifyAuthService.GetTracksClientAsync(spotifyClient));
                InfoToReturn.TrackArtist = PolledTrack.Artists[0].Name;
                InfoToReturn.TrackTitle = PolledTrack.Name;
                InfoToReturn.TrackDuration = PolledTrack.DurationMs.ToString(); //Currently in Milliseconds!


                //-------------------------------------------

                FullPlaylist CurPlaylist = await _spotifyService.GetPlaylistFromIDAsync(removeVoteInput.RemoveVotePlaylistID, spotifyClient);
                InfoToReturn.PlaylistFollowerCount = CurPlaylist.Followers.Total.ToString();

                int PlaylistFollowerCountAsInt = 0;
                try
                {
                    PlaylistFollowerCountAsInt = Int32.Parse(InfoToReturn.PlaylistFollowerCount);
                }
                catch (Exception)
                {
                    Console.WriteLine("Unable to parse playlist follower count. Defaulting to 0 as playlist follower count.");
                }


                //Simulating that playlist has 2 followers (below)------
                //PlaylistFollowerCountAsInt = 2;
                //InfoToReturn.PlaylistFollowerCount = "2";

                //Simulating that playlist has 2 followers (above)------
                //----------------------------
                InfoToReturn.UserVotedYes = null;

                //---------------------------


                return InfoToReturn;
            }
            return null; //No js safeguard for "null" return.
        }
    }
}
