using Core.Interfaces.Models;
using Microsoft.AspNetCore.Identity;

namespace Core.Models
{
    public class User : IdentityUser<Guid>, IBaseModel
    {
        public RefreshToken? RefreshToken { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
