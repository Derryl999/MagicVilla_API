using MagicVilla_web.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MagicVilla_web.Models.ViewModels
{
    public class VillaNumberUpdateVM
    {
        public VillaNumberUpdateVM()
        {
            VillaNumber = new();
        }
        public VillaNumberUpdateDTO VillaNumber { get; set; }
        public IEnumerable<SelectListItem> VillaList { get; set; }
    }
}
