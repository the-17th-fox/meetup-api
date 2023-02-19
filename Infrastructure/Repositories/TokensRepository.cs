using Core.Constants.CustomExceptions;
using Core.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TokensRepository : ITokensRepository
    {
        private readonly MeetupsDbContext _context;

        public TokensRepository(MeetupsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<RefreshToken> IssueRefreshTokenAsync(RefreshToken newRefreshToken)
        {
            var refrToken = await _context.RefreshTokens.Where(rt => rt.UserId == newRefreshToken.UserId).FirstOrDefaultAsync();

            if (refrToken == null)
            {
                _context.RefreshTokens.Add(newRefreshToken);
                await _context.SaveChangesAsync();
                return newRefreshToken;
            }

            _context.RefreshTokens.Update(refrToken);
            refrToken.ExpiresAt = newRefreshToken.ExpiresAt;
            refrToken.IssuedAt = newRefreshToken.IssuedAt;
            refrToken.UserId = newRefreshToken.UserId;
            refrToken.IsRevoked = false;
            await _context.SaveChangesAsync();

            return refrToken!;
        }

        public async Task RemoveRefreshTokenAsync(Guid userId)
        {
            var refrToken = await _context.RefreshTokens.Where(rt => rt.UserId == userId).FirstOrDefaultAsync();
            if (refrToken == null)
                throw new NotFoundException(nameof(refrToken));

            _context.RefreshTokens.Update(refrToken);
            refrToken.IsRevoked = true;

            await _context.SaveChangesAsync();
        }
    }
}
