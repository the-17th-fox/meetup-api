using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Users
{
    public class TokensViewModel : TokensRefreshingViewModel
    {
        public DateTime RefreshTokenExpiresAt { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }
    }
}
