using AutoMapper;
using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;
using MagicVilla_web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MagicVilla_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IMapper _mapper;
        public HomeController(IVillaService villaService, IMapper mapper)
        {
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> Index()
        {
            List<VillaDTO> villaList = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString());
            }
            return View(villaList);
        }

        
    }
}