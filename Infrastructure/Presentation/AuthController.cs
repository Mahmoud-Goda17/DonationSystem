using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.AuthDtos;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManger serviceManger) :ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LogInDto loginDto)
        {
            var result = await serviceManger.AuthService.LoginAsync(loginDto);
            if (!result.IsAuthenticated) return BadRequest(result.Message);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                setRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            }
            return Ok(result);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await serviceManger.AuthService.RegisterAsync(registerDto);
            if (!result.IsAuthenticated) return BadRequest(result.Message);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            setRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);

            return Ok(result);
        }
        [HttpPost("addrole")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleDto addRoleDto)
        {
            var result = await serviceManger.AuthService.AddRol(addRoleDto);
            if (!string.IsNullOrEmpty(result)) return BadRequest(result);
            return Ok(addRoleDto);
        }
        [HttpPost("refreshtoken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = await serviceManger.AuthService.RefreshTokenAsync(refreshToken);
            if (!result.IsAuthenticated) return BadRequest(result);
            if (!string.IsNullOrEmpty(result.RefreshToken))
            {
                setRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            }
            return Ok(result);
        }
        [HttpPost("revoketoken")]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result= await serviceManger.AuthService.RevokeTokenAsync(refreshToken);
            if(result==false) return BadRequest("Token is Invalid");
            return Ok();

        }
        private void setRefreshTokenInCookie(string refreshToken, DateTime refreshTokenExpiration)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshTokenExpiration.ToLocalTime(),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
