using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.AuthDtos;
using Shared.FluentValidation.Auth;

namespace Services
{
    public class AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager,IOptions<JwtOpt> options) : IAuthService
    {
        public async Task<UserResultDto> LoginAsync(LogInDto loginDto)
        {
            var userresultdto = new UserResultDto();
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user is null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                userresultdto.IsAuthenticated = false;
                userresultdto.Message = "Email or Password is InCorrect";
                return userresultdto;
            }
            var roles = await userManager.GetRolesAsync(user);
            userresultdto.DisplayName = user.DisplayName;
            userresultdto.Email = user.Email;
            userresultdto.Roles = roles;
            userresultdto.IsAuthenticated = true;
            userresultdto.Message = "Login Success";
            userresultdto.Token = await GenerateToken(user);
            if(user.RefreshTokens.Any(r=>r.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(r => r.IsActive);
                userresultdto.RefreshToken = activeRefreshToken.Token;
                userresultdto.RefreshTokenExpiration = activeRefreshToken.ExpireOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                userresultdto.RefreshToken = refreshToken.Token;
                userresultdto.RefreshTokenExpiration = refreshToken.ExpireOn;
                user.RefreshTokens.Add(refreshToken);
                await userManager.UpdateAsync(user);
            }

            return userresultdto;

        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var user1 = await userManager.FindByEmailAsync(registerDto.Email);
            if (user1 is not null) return new UserResultDto { Message = "Email is already registed", IsAuthenticated = false };

            var user2 = await userManager.FindByNameAsync(registerDto.Username);
            if (user2 is not null) return new UserResultDto { Message = "Username is already registed",IsAuthenticated=false };

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
            };
            var result = await userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new UserResultDto { Message = errors };
            }
            await userManager.AddToRoleAsync(user, "User");
            var userresultdto= new UserResultDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                IsAuthenticated = true,
                Message = "Register Success",
                Token =  await GenerateToken(user),
                Roles = new List<string> { "User" }
            };
            var refreshToken = GenerateRefreshToken();
            userresultdto.RefreshToken = refreshToken.Token;
            userresultdto.RefreshTokenExpiration = refreshToken.ExpireOn;
            user.RefreshTokens.Add(refreshToken);
            await userManager.UpdateAsync(user);
            return userresultdto;

        }
        public async Task<string> AddRol(AddRoleDto addRoleDto)
        {
            var user = await userManager.FindByIdAsync(addRoleDto.UserId);
            if (user is null || !await roleManager.RoleExistsAsync(addRoleDto.Role))
                return "Invalid UserId or Role";

            if (await userManager.IsInRoleAsync(user, addRoleDto.Role))
                return "User already in this role";

           var result= await userManager.AddToRoleAsync(user, addRoleDto.Role);

            return result.Succeeded? string.Empty : "Something went wrong";

        }
        public async Task<UserResultDto> RefreshTokenAsync(string Token)
        {
            var userresultdto = new UserResultDto();
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == Token));
            if (user is null)
            {
                userresultdto.IsAuthenticated = false;
                userresultdto.Message = "Invalid Token";
                return userresultdto;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == Token);
            if (!refreshToken.IsActive)
            {
                userresultdto.IsAuthenticated = false;
                userresultdto.Message = "InActive Token";
                return userresultdto;
            }
            refreshToken.RevokeOn = DateTime.UtcNow;
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await userManager.UpdateAsync(user);

            var roles = await userManager.GetRolesAsync(user);
            var jwttoken = await GenerateToken(user);

            userresultdto.DisplayName = user.DisplayName;
            userresultdto.Email = user.Email;
            userresultdto.Roles = roles;
            userresultdto.IsAuthenticated = true;
            userresultdto.Token = jwttoken;
            userresultdto.RefreshToken = newRefreshToken.Token;
            userresultdto.RefreshTokenExpiration = newRefreshToken.ExpireOn;

            return userresultdto;
        }
        public async Task<bool> RevokeTokenAsync(string Token)
        {
            var user = await userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == Token));
            if (user is null) return false;

            var refreshtoken = user.RefreshTokens.Single(r=>r.Token == Token);
            if (!refreshtoken.IsActive) return false;

            refreshtoken.RevokeOn = DateTime.UtcNow;
            await userManager.UpdateAsync(user);
            return true;
        }

        private async Task<string> GenerateToken(AppUser user)
        {
            var jwtoption = options.Value; 

            var authclaims = await userManager.GetClaimsAsync(user);
            var authCalims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.Email,user.Email),
            };
            authCalims.AddRange(authclaims);
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authCalims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoption.SecretKey));

            var token = new JwtSecurityToken(
                issuer: jwtoption.Issuer,
                audience: jwtoption.Audience,
                expires: DateTime.Now.AddDays(jwtoption.DurationInDays),
                claims: authCalims,
                signingCredentials: new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            RandomNumberGenerator.Fill(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpireOn = DateTime.Now.AddDays(10),
                CreateOn = DateTime.Now
            };
        }

    }
}
