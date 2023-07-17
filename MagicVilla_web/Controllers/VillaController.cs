﻿using AutoMapper;
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
        public VillaController(IVillaService villaService, IMapper mapper)
        {
            _mapper = mapper;
            _villaService = villaService;
        }

        public async Task<IActionResult> IndexVilla()
        {
            List<VillaDTO> villaList = new();
            var response = await _villaService.GetAllAsync<APIResponse>();
            if (response != null && response.IsSuccess)
            {
                villaList = JsonConvert.DeserializeObject<List<VillaDTO>>(response.Result.ToString());
            }
            return View(villaList);
        }
        public async Task<IActionResult> CreateVilla()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateVilla(VillaCreateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.CreateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(model);

        }
        public async Task<IActionResult> UpdateVilla(int id)
        {
            var response = await _villaService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                VillaDTO villa = JsonConvert.DeserializeObject<VillaDTO>(response.Result.ToString());
                VillaUpdateDTO model = _mapper.Map<VillaUpdateDTO>(villa);
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateVilla(VillaUpdateDTO model)
        {
            if (ModelState.IsValid)
            {
                var response = await _villaService.UpdateAsync<APIResponse>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(IndexVilla));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteVilla(int id)
        {
            var response = await _villaService.GetAsync<APIResponse>(id);
            if (response != null && response.IsSuccess)
            {
                VillaDTO villa = JsonConvert.DeserializeObject<VillaDTO>(response.Result.ToString());
                return View(villa);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteVilla(VillaDTO villa)
        {
            var response = await _villaService.DeleteAsync<APIResponse>(villa.Id);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(IndexVilla));
            }
            return View(villa);
        }
    }
}
