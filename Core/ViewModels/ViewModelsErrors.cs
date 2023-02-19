using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ViewModels
{
    public abstract class ViewModelsErrors
    {
        public const string OutOfRange = "Provided values is out of permitted borders.";
        public const string RequiredNotProvided = "Required field wasn't provided.";
    }
}
