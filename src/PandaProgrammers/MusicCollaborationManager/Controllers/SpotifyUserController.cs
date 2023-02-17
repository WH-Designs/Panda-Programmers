using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.Services.Abstract;
using MusicCollaborationManager.Services.Concrete;
using SpotifyAPI.Web;
using System.Collections.Generic;
using MusicCollaborationManager.Models.DTO;
using static System.Net.Mime.MediaTypeNames;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyUserController : ControllerBase
    {
        private readonly ISpotifyUserService _spotifyUserService;

        public SpotifyUserController(ISpotifyUserService spotifyService)
        {
            _spotifyUserService = spotifyService;
        }
    }
}