using Core.Constants.CustomExceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Core.Services
{
    public class TokensService : ITokensService
    {
        private readonly JwtConfigModel _jwtConfig;
        private readonly UserManager<User> _userManager;
        private readonly ITokensRepository _tokensRepository;

        public TokensService(IOptions<JwtConfigModel> jwtConfig, UserManager<User> userManager, ITokensRepository tokensRepository)
        {
            _jwtConfig = jwtConfig != null ? jwtConfig.Value : throw new ArgumentNullException(nameof(jwtConfig));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _tokensRepository = tokensRepository ?? throw new ArgumentNullException(nameof(tokensRepository));
        }

        public async Task RevokeRefreshTokenAsync(Guid userId)
        {
            await _tokensRepository.RemoveRefreshTokenAsync(userId);
        }

        public JwtSecurityToken CreateAccessToken(IList<Claim> claims)
        {
            var symSecurityKey = _jwtConfig.Key;
            return new(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.AuthTokenLifetimeInMinutes),
                signingCredentials: new SigningCredentials(symSecurityKey, SecurityAlgorithms.HmacSha256));
        }

        private RefreshToken GenerateRefreshToken(Guid userId)
        {
            var expiresAt = _jwtConfig.RefreshTokenLifetimeInDays;
            return new RefreshToken(DateTime.UtcNow.AddDays(expiresAt), userId);
        }

        public async Task<RefreshToken> IssueRefreshTokenAsync(Guid userId)
        {
            var newRefreshToken = GenerateRefreshToken(userId);
            return await _tokensRepository.IssueRefreshTokenAsync(newRefreshToken);
        }

        public async Task<TokensViewModel> RefreshAccessTokenAsync(string refreshToken, string accessToken)
        {
            var principal = GetClaimsPrincipalFromToken(accessToken);
            if (principal == null)
                throw new BadRequestException("Invalid access token or refresh token.");

            var user = await FindUserByClaimsAsync(principal);

            ValidateRefreshToken(user.RefreshToken, refreshToken);

            var newRefreshToken = await IssueRefreshTokenAsync(user.Id);

            var newAccessToken = CreateAccessToken(principal!.Claims.ToList());

            return new()
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiresAt = newRefreshToken.ExpiresAt,
                AccessTokenExpiresAt = newAccessToken.ValidTo
            };
        }

        private static void ValidateRefreshToken(RefreshToken? userRefreshToken, string providedRefreshToken)
        {
            if (userRefreshToken == null)
                throw new UnauthorizedException("User's refresh token is null");

            if (!userRefreshToken.IsActive)
                throw new UnauthorizedException("User's refresh token is expired or revoked");

            if (!userRefreshToken.Token.ToString().Equals(providedRefreshToken))
                throw new UnauthorizedException("User's refresh token doesn't equal to the provided refresh token");
        }

        private async Task<User> FindUserByClaimsAsync(ClaimsPrincipal claimsPrincipal)
        {
            string? userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                throw new BadRequestException("Could not get an user id from the claims.");

            var user = await _userManager.Users
                .Where(a => a.UserName == userId)
                .Include(a => a.RefreshToken)
                .FirstOrDefaultAsync();

            if (user == null)
                throw new NotFoundException("There is no user with the specified id.");

            return user;
        }

        private ClaimsPrincipal? GetClaimsPrincipalFromToken(string accessToken)
        {
            var tokenValidationParams = new TokenValidationParameters()
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _jwtConfig.Key,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParams, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,                     StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid access token or refresh token.");
            }

            return principal;
        }
    }
}
