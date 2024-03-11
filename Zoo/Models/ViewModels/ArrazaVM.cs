using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Zoo.Models.ViewModels
{
    public class ArrazaVM
    {
        public Arraza Arraza { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> LekuakList { get; set; }
    }
}
