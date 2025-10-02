using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.AuthDtos;

namespace Services.Abstractions
{
    public interface IAuthService
    {
        Task<UserResultDto> LoginAsync(LogInDto loginDto);
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<string> AddRol(AddRoleDto addRoleDto);
        Task<UserResultDto> RefreshTokenAsync(string Token);
        Task<bool> RevokeTokenAsync(string Token);
    }
}
