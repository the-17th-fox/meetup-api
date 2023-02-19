using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Users
{
    /// <summary>
    /// TODO: Add validation attributes
    /// </summary>
    public class TokensRefreshingViewModel
    {
        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        public string AccessToken { get; set; } = string.Empty;
        [Required(ErrorMessage = ViewModelsErrors.RequiredNotProvided)]
        public Guid RefreshToken { get; set; } = Guid.Empty;
    }
}
