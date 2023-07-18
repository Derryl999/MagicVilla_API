using AutoMapper;
using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Models.ViewModels;
using MagicVilla_web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace MagicVilla_web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNumberService;
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaNumberController(IVillaNumberService villaNumberService, IMapper mapper, IVillaService villaService)
        {
            _villaNumberService = villaNumberService;
            _mapper = mapper;
            _villaService = villaService;

        }
        public async Task<IActionResult> IndexVillaNumber()
        {
            List<VillaNumberDTO> villaNumberList = new();
            var response = await _villaNumberService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                 villaNumberList = JsonConvert.DeserializeObject<List<VillaNumberDTO>>(response.Result.ToString());
            }
            return View(villaNumberList);
        }
        public async Task<IActionResult> CreateVillaNumber()
        {
            VillaNumberCreateVM villaNumberVM = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                villaNumberVM.VillaList = JsonConvert.DeserializeObject<List<VillaDTO>>
                    (response.Result.ToString()).Select(u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    });
            }
            return View(villaNumberVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVillaNumber(VillaNumberCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaNumberService.CreateAsync<APIResponse>(model);
                if(response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVillaNumber));
                }
            }
            return View(model);
        }
    }
}
