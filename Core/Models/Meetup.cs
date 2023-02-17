using System.ComponentModel.DataAnnotations.Schema;
using Core.Interfaces.Models;

namespace Core.Models
{
    public class Meetup : IBaseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartsAt { get; set; }

        public string Speaker { get; set; } = string.Empty;
        public string MeetupManager { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
