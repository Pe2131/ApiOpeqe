using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiOpeqe.ViewModel;
using DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace ApiOpeqe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IunitofWork _db;
        private readonly UserManager<IdentityUser> _userManager;
        public HomeController(IunitofWork db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        // POST: api/Home
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> PostAsync([FromBody] Coordinates model)
        {
            if (ModelState.IsValid)
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = await _userManager.FindByNameAsync(User.Identity.Name);
                    History historyTable = new History { latitude = model.latitude, Longitude = model.Longitude,distance=model.latitude-model.Longitude, User = user };
                    _db.HistoryRepo.Insert(historyTable);
                    await _db.HistoryRepo.Save();
                    var result = _db.HistoryRepo.Get(a => a.User == user).Select(b=> new { b.latitude, b.Longitude, b.distance, b.User.Id });
                    return Ok(result);
                }
                else
                {
                    return Unauthorized();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
