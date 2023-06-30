using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    //[Route("api/VillaAPI")]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        [HttpGet]
        public List<VillaDTO> GetVillas()
        {
            return VillaStore.villaList;
        }
        [HttpGet("{id:int}")]
        public VillaDTO GetVilla(int id)
        {
            return VillaStore.villaList.Find(u => u.Id == id);
        }
    }
}
