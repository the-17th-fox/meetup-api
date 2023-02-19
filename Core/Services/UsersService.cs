using Core.Constants.CustomExceptions;
using Core.Interfaces.Services;
using Core.Models;
using Core.Utilities;
using Core.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Core.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokensService _tokensService;

        public UsersService(UserManager<User> userManager, ITokensService tokensService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokensService = tokensService ?? throw new ArgumentNullException(nameof(tokensService));
        }

        public async Task<TokensViewModel> LoginAsync(LoginViewModel loginViewModel)
        {
            var user = await _userManager.Users
                .Include(a => a.RefreshToken)
                .FirstOrDefaultAsync(a => a.Email == loginViewModel.Email);
            if (user == null)
                throw new NotFoundException($"There is no user with the specified email.");

            var isCorrect = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (!isCorrect)
                throw new UnauthorizedException("The password is incorrect");

            var userClaims = GetClaims(user);

            var accessToken = _tokensService.CreateAccessToken(userClaims);
            var refreshToken = await _tokensService.IssueRefreshTokenAsync(user.Id);

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(accessToken),
                RefreshToken = refreshToken.Token,
                RefreshTokenExpiresAt = refreshToken.ExpiresAt,
                AccessTokenExpiresAt = accessToken.ValidTo
            };
        }

        private static List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
				new Claim(ClaimTypes.Email, user.Email)
            };

            return claims;
        }

        public async Task LogoutAsync(Guid id)
        {
            if (await _userManager.FindByIdAsync(id.ToString()) == null)
                throw new NotFoundException("There is no user with the specified id.");

            await _tokensService.RevokeRefreshTokenAsync(id);
        }
    }
}
