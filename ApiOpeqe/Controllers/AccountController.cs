using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiOpeqe.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Interfaces;
namespace ApiOpeqe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IunitofWork unitOfWork;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AccountController(IunitofWork _unitOfWork, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager, RoleManager<IdentityRole> _roleManager)
        {
            unitOfWork = _unitOfWork;
            userManager = _userManager;
            signInManager = _signInManager;
            roleManager = _roleManager;
        }
        // POST: api/Account
        [HttpPost]
        [Route("/api/account/login")]
        public async Task<IActionResult> Login(Login model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(model.username, model.password, false, false);
                    if (result.Succeeded)
                    {
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurVerifyTokenKey"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var tokenOption = new JwtSecurityToken(
                            issuer: "https://localhost:44387",
                            claims: new List<Claim>
                            {
                                new Claim(ClaimTypes.Name,model.username)
                            },
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: signinCredentials
                        );
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);
                        return Ok(new { token = tokenString });
                    }
                    if (result.IsLockedOut)
                    {
                        return Unauthorized("your user name was locked");
                    }
                    else
                    {
                        return Unauthorized("user name or password was incorrect");
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }


            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost]
        [Route("/api/account/signUp")]
        public async Task<IActionResult> signUp(SignUp model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new IdentityUser { UserName = model.username };
                    var result = await userManager.CreateAsync(user, model.password);
                    if (result.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return StatusCode(500,result.Errors);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }


            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
