﻿using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
    [ApiController]
    //[Route("api/VillaAPI")] 
    [Route("api/[controller]")]
    public class VillaAPIController : ControllerBase
    {
        //private readonly ILogger<VillaAPIController> logger;
        //private readonly ILogging logging;
        //public VillaAPIController(ILogger<VillaAPIController> _logger)
        //{
        //    logger = _logger;
        //}
        private readonly ApplicationDbContext context;
        public VillaAPIController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<IEnumerable<VillaDTO>> GetVillas()
        {
            //logger.LogInformation("Getting all villas");
            //logging.Log("", "Getting all villas");
            return Ok(context.Villas);
        }
        [HttpGet("{id:int}", Name = "GetVilla")]
        //[ProducesResponseType(200 , Type = typeof(VillaDTO))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDTO> GetVilla(int id)
        {
            if (id == 0)
            {
                //logging.Log("Error", "Get villa error with id " + id);
                return BadRequest();
            }
            var villa = context.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            return Ok(villa);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<VillaDTO> CreateVilla([FromBody] VillaDTO villaDTO)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            if (context.Villas.FirstOrDefault(u => u.Name.ToLower() == villaDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("Name", "Villa already exists");
                return BadRequest(ModelState);
            }
            if (villaDTO == null)
            {
                return BadRequest(villaDTO);
            }
            if (villaDTO.Id > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //villa.Id = VillaStore.villaList.OrderByDescending(u => u.Id).Select(u => u.Id).FirstOrDefault() + 1;
            //villa.Id = VillaStore.villaList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            Villa villa = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            context.Villas.Add(villa);
            context.SaveChanges();
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteVilla(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var villa = context.Villas.FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            context.Villas.Remove(villa);
            context.SaveChanges();
            return NoContent();
        }
        [HttpPut("id:int", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateVilla(int id, [FromBody] VillaDTO villaDTO)
        {
            if (villaDTO == null || id != villaDTO.Id)
            {
                return BadRequest();
            }
            //var newVilla = context.Villas.FirstOrDefault(u => u.Id == villa.Id);
            //newVilla.Name = villa.Name;
            //newVilla.Occupancy = villa.Occupancy;
            //newVilla.Sqft = villa.Sqft;
            Villa villa = new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            context.Update(villa);
            context.SaveChanges();
            return NoContent();
        }
        [HttpPatch("{id:int}", Name = "PatchVilla")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult PatchVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var villa = context.Villas.AsNoTracking().FirstOrDefault(u => u.Id == id);
            if (villa == null)
            {
                return BadRequest();
            }
            VillaDTO villaDTO = new VillaDTO()
            {
                Amenity = villa.Amenity,
                Details = villa.Details,
                Id = villa.Id,
                ImageUrl = villa.ImageUrl,
                Name = villa.Name,
                Occupancy = villa.Occupancy,
                Rate = villa.Rate,
                Sqft = villa.Sqft
            };
            patchDTO.ApplyTo(villaDTO, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Villa model= new Villa()
            {
                Amenity = villaDTO.Amenity,
                Details = villaDTO.Details,
                Id = villaDTO.Id,
                ImageUrl = villaDTO.ImageUrl,
                Name = villaDTO.Name,
                Occupancy = villaDTO.Occupancy,
                Rate = villaDTO.Rate,
                Sqft = villaDTO.Sqft
            };
            context.Villas.Update(model);
            context.SaveChanges();
            return NoContent();
        }
    }
}
