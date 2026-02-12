using Backend.Models;
using Backend.Models.DTOs;

namespace Backend.Services
{
    public interface IAuthService
    {
        Task<(string? AccessToken, string? RefreshToken)> LoginAsync(LoginDto loginDto);
        Task<(string? AccessToken, string? RefreshToken)> RefreshTokenAsync(string refreshToken);
        Task<User> RegisterAsync(RegisterDto registerDto);
        bool VerifyPassword(string plainPassword, string passwordHash);
        string GenerateJwtToken(User user);
        string GenerateRefreshToken();
    }
}
