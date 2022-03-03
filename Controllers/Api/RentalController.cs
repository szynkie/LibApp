using LibApp.Data;
using LibApp.Models;
using LibApp.Respositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly RentalRepository _rentalsRep;

        public RentalController(ApplicationDbContext context)
        {
            _rentalsRep = new RentalRepository(context);
        }

        // GET api/Rentals/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            try
            {
                return (await _rentalsRep.GetAsync()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting records");
            }
        }

        // GET api/Rentals/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Rental>> GetRentalById(int id)
        {
            try
            {
                var result = await _rentalsRep.GetByIdAsync(id);
                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting record");
            }
        }

        // Delete api/Rentals/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var RentalToDelete = await _rentalsRep.GetByIdAsync(id);

                if (RentalToDelete == null)
                    return NotFound($"Rental with Id = {id} not found");

                await _rentalsRep.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while deleting record");
            }
        }

        // Post api/Rentals/
        [HttpPost]
        public async Task<ActionResult> Add(Rental Rental)
        {
            try
            {
                if (Rental == null)
                    return BadRequest();

                await _rentalsRep.AddAsync(Rental);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new record");
            }
        }

        // Put api/Rentals/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Rental Rental)
        {
            try
            {
                if (id != Rental.Id)
                    return BadRequest("Rental ID mismatch");

                var RentalToUpdate = await _rentalsRep.GetByIdAsync(id);

                if (RentalToUpdate == null)
                    return NotFound($"Rental with Id = {id} not found");

                await _rentalsRep.UpdateAsync(Rental);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

    }
}