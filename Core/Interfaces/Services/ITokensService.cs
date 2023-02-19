using Core.Models;
using Core.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface ITokensService
    {
        public Task<TokensViewModel> RefreshAccessTokenAsync(string refreshToken, string accessToken);
        public Task<RefreshToken> IssueRefreshTokenAsync(Guid userId);
        public Task RevokeRefreshTokenAsync(Guid userId);
        public JwtSecurityToken CreateAccessToken(IList<Claim> claims);
    }
}
