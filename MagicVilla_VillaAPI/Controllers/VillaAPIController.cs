using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    //[Route("api/VillaAPI")]
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        private readonly ILogger<VillaAPIController> logger;

        public VillaAPIController(ILogger<VillaAPIController> _logger)
        {
            logger = _logger;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            logger.LogInformation("Getting all villas");
            return Ok(VillaStore.villaList);
        }
        [HttpGet("{id:int}" , Name = "GetVilla")]
        //[ProducesResponseType(200 , Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if(id == 0)
            {
                logger.LogError("Get villa error with id " + id);
                return BadRequest();
            }
            var villa = VillaStore.villaList.Find(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody]VillaDTO villa)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if(VillaStore.villaList.FirstOrDefault(u => u.Name.ToLower() == villa.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Name", "Villa already exists");
                return BadRequest(ModelState);
            }
            if (villa == null)
            {
                return BadRequest(villa);
            }
            if(villa.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //villa.Id = VillaStore.villaList.OrderByDescending(u => u.Id).Select(u => u.Id).FirstOrDefault() + 1;
            villa.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;

            VillaStore.villaList.Add(villa);
            return CreatedAtRoute("GetVilla", new {id = villa.Id}, villa);
        }
        [HttpDelete("{id:int}" , Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteVilla(int id)
        {
            if(id <= 0)
            {
                return BadRequest();
            }
            var villa = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if(villa == null)
            {
                return NotFound();
            }
            VillaStore.villaList.Remove(villa);
            return NoContent();
        }
        [HttpPut("id:int" , Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateVilla(int id , [FromBody]VillaDTO villa)
        {
            if(villa == null || id != villa.Id)
            {
                return BadRequest();
            }
            var newVilla = VillaStore.villaList.FirstOrDefault(u => u.Id == villa.Id);
            newVilla.Name = villa.Name;
            newVilla.Occupancy = villa.Occupancy;
            newVilla.Sqft = villa.Sqft;
            return NoContent();
        }
        [HttpPatch("{id:int}" , Name = "PatchVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PatchVilla(int id , JsonPatchDocument<VillaDTO> villa)
        {
            if(villa == null || id == 0)
            {
                return BadRequest();
            }
            var newVilla = VillaStore.villaList.FirstOrDefault(u => u.Id == id);
            if(newVilla == null)
            {
                return BadRequest();
            }
            villa.ApplyTo(newVilla, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
