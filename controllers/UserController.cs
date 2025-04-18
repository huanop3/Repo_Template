//api-controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.EntityFrameworkCore;
using web_api_base.Helper;
using web_api_base.Models.dbebay;
using web_api_base.Models.ViewModel;
//using webapi_blazor.Models;

namespace web_api_base.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService { get; }
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetCookie")]
        public async Task<ActionResult> GetCookie()
        {
            string value ="";
            bool rs = HttpContext.Request.Cookies.TryGetValue("access_token",out value);
            return Ok(value);
        }
        [HttpPost("/user/login")]
        public async Task<ActionResult> Login(UserLoginVM userLogin)
        {
            var res = await _userService.Login(userLogin) as OkObjectResult;
            var userResult = res.Value as HTTPResponseClient<UserLoginResultVM>;
            // Tao cookie từ server
            var cookieOption = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            };
            HttpContext.Response.Cookies.Append("access_token",userResult.Data.AccessToken,cookieOption);
            return res;
        }
        
        [Authorize]
        [HttpGet("/user/GetProfile")]
        public async Task<ActionResult> GetProfile([FromHeader] string authorization)
        {
            // string token = authorization.Replace("Bearer ",""); 
            // // string token  = HttpContext.Request.Headers["Authorization"];
            // string account = _jwtService.DecodePayloadToken(token);
            // var user = _context.Users.SingleOrDefault(us => us.Username == account || us.Email == account);
            return Ok("");
        }

        [Authorize(Roles = "Buyer,Seller")]
        [HttpGet("/user/PostNew")]
        public async Task<ActionResult> PostNew()
        {

            return Ok("");
        }

    }
}



