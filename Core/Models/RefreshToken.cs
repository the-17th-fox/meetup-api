using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    [Index(nameof(UserId), IsUnique = true)]
    public class RefreshToken
    {
        [Key]
        public Guid Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public bool IsRevoked { get; set; } = false;

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [NotMapped]
        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        [NotMapped]
        public bool IsActive => !(IsExpired || IsRevoked);

        public RefreshToken(DateTime expiresAt, Guid userId)
        {
            ExpiresAt = expiresAt;
            UserId = userId;
        }
    }
}
