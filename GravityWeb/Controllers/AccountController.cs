using Domain.Entities;
using GravityDTO;
using GravityWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GravityWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppSettings _appsettings;
        public AccountController(
            IOptions<AppSettings> appSettings,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _appsettings = appSettings.Value;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginCredentials userLoginCredentials)
        {

            var user = await _userManager.FindByEmailAsync(userLoginCredentials.userEmail);

            if (user != null && await _userManager.CheckPasswordAsync(user,userLoginCredentials.password))
            {              

                //get the role of a user
                var roles = await _userManager.GetRolesAsync(user);

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appsettings.Secret));

                //prepare token details
                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor 
                {
                    Subject = new ClaimsIdentity(new Claim[] 
                    { 
                        new Claim(JwtRegisteredClaimNames.Sub,userLoginCredentials.userEmail),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.UserName),
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                        new Claim("LoggedOn",DateTime.Now.ToString())
                
                    }),
                
                    SigningCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256),
                    Issuer = _appsettings.Site,
                    Audience = _appsettings.Audience,
                    Expires = DateTime.UtcNow.AddDays(_appsettings.ExpireTimeDays)
                };


                var token = tokenHandler.CreateToken(tokenDescriptor);
                
                return Ok(new { 
                    AccessToken = tokenHandler.WriteToken(token),
                    expiration = token.ValidTo,
                    userEmail = user.Email,
                    userRole = roles.FirstOrDefault().ToString()
                });

            }          

            return Unauthorized();
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterCredentials userRegisterCredentials)
        {

            var user = new ApplicationUser
            {
                UserName = userRegisterCredentials.userName,
                Email = userRegisterCredentials.userEmail,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var result = await _userManager.CreateAsync(user, userRegisterCredentials.password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Client");

                //email confirmation

                return Ok(new 
                { 
                    userEmail = user.Email,
                    message = "Registration Successful"
                });

            }

            return BadRequest(new JsonResult(result.Errors));

        }


        #region userName Remote Validation
        [AllowAnonymous]
        [HttpPost("checkname")]
        public async Task<IActionResult> CheckName([FromBody]UserRegisterCredentials userRegisterCredentials)
        {
            var user = await _userManager.FindByNameAsync(userRegisterCredentials.userName);

            return Ok(new { nameTaken = user != null });           
           
        }
        #endregion
    }

}
