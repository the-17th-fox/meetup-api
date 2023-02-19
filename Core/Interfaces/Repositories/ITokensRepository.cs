using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface ITokensRepository
    {
        public Task RemoveRefreshTokenAsync(Guid userId);
        public Task<RefreshToken> IssueRefreshTokenAsync(RefreshToken newRefreshToken);
    }
}
