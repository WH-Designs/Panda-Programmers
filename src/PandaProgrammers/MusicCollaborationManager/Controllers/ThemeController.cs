using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Abstract;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IListenerRepository _listenerRepository;

        public ThemeController(IListenerRepository listenerRepository, UserManager<IdentityUser> userManager)
        {
            _listenerRepository = listenerRepository;
            _userManager = userManager;
        }

        // POST: api/Chore
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("themeAdd/{theme}")]
        public ActionResult themeAdd(string theme)
        {
            try{
                string aspId = _userManager.GetUserId(User);
                Listener listener = new Listener();
                listener = _listenerRepository.FindListenerByAspId(aspId);

                listener.Theme = theme;

                return Ok(_listenerRepository.AddOrUpdate(listener));

            } catch(NullReferenceException) {
                
                return Ok();
            }
        }
    }
}