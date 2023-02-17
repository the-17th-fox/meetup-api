using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Event : IBaseModel<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartsAt { get; set; }

        [InverseProperty(nameof(User.EventsAsSpeaker))]
        public User Speaker { get; set; } = null!;
        public Guid SpeakerId { get; set; }

        [InverseProperty(nameof(User.EventsAsManager))]
        public User EventManager { get; set; } = null!;
        public Guid EventManagerId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
