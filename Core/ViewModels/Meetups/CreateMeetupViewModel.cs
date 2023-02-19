using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Meetups
{
    public class CreateMeetupViewModel
    {
        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        [StringLength(maximumLength: 500, MinimumLength = 5, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        [StringLength(maximumLength: 100, MinimumLength = 1, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public string Location { get; set; } = string.Empty;

        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        public DateTime StartsAt { get; set; }

        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public string Speaker { get; set; } = string.Empty;

        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        [StringLength(maximumLength: 50, MinimumLength = 1, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public string MeetupManager { get; set; } = string.Empty;
    }
}
