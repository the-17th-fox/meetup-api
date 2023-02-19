using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Meetups
{
    public class UpdateMeetupViewModel : CreateMeetupViewModel
    {
        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        public Guid Id { get; set; }
    }
}
