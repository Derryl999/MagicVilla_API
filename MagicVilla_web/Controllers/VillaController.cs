using AutoMapper;
using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MagicVilla_web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public VillaController(IVillaService villaService , IMapper mapper)
        {
            _mapper = mapper;
            _villaService = villaService;
        }
        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> villaList = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if(response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString());
            }
            return View(villaList);
        }
    }
}
