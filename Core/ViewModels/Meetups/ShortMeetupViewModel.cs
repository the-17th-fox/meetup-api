using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Meetups
{
    public class ShortMeetupViewModel
    {
        public string Title { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public DateTime StartsAt { get; set; }
        public string Speaker { get; set; } = string.Empty;
    }
}
