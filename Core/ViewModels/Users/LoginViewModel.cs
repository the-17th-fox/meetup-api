using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Users
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        [EmailAddress(ErrorMessage = "Provided email is incorrect.")]
        public string Email { get; set; } = string.Empty;
    }
}
