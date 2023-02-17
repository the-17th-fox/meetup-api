using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels.Pagination
{
    public class PageParametersViewModel
    {
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public int PageNumber { get; set; } = 1;
        [Range(minimum: 1, maximum: 50, ErrorMessage = ViewModelsErrors.OutOfRange)]
        public int PageSize { get; set; } = 20;
    }
}
