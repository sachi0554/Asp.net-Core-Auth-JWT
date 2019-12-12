using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using App.Core.Contract;
using App.Core.Error;
using App.Core.RequestFlow;
using App.Core.ResponseFlow;
using Auth20_V1.Cache;
using Auth20_V1.Routes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Auth20_V1.Controllers.v1
{
    public class UsersController : Controller
    {
        
        private readonly IIdentityServices _identityService;
        public UsersController(IIdentityServices identityService)
        {
            _identityService = identityService;
        }

        [Authorize]
        [HttpGet("api/RedisCacheTesting")]
        [Cached(60)]
        public IActionResult Get()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread.Sleep(2000);
            stopwatch.Stop();
            return Ok(new { name = "testing working" });
        }

        [Authorize]
        [HttpGet("api/EmailTotoken")]
        public async Task<IActionResult> Test(string token)
        {
            try
            {
                var id = await _identityService.GetUser(token);
                return Ok(new { id = id });
            }
            catch (Exception)
            {

                return BadRequest(new { error = "something went wrong"});
            }

        }
        
       
        [HttpPost(ApiRoute.Users.Login)]
        [EnableCors("AllowOrigin")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var authResponse = await _identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
        /* [Route("api/register")]*/
        [HttpPost(ApiRoute.Users.Register)]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest request)
        {
           
            var authResponse = await _identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoute.Users.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var authResponse = await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.Success)
            {
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });
            }

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

    }
}