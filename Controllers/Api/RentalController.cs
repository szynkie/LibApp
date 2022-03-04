using LibApp.Data;
using LibApp.Models;
using LibApp.Dtos;
using LibApp.Respositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Owner, StoreManager")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly RentalsRepository _rentalsRep;

        private IMapper _mapper { get; }

        public RentalsController(ApplicationDbContext context, IMapper mapper)
        {
            _rentalsRep = new RentalsRepository(context);
            _mapper = mapper;
        }

        // GET api/Rentals/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            try
            {
                var rentals = (await _rentalsRep.GetAsync())
                .ToList()
                .Select(_mapper.Map<Rental, RentalDto>);

                return Ok(rentals);
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

                return Ok(_mapper.Map<Rental, RentalDto>(result));
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

                await _rentalsRep.DeleteAsync(RentalToDelete);
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
        public async Task<ActionResult> Add(RentalDto rental)
        {
            try
            {
                if (rental == null)
                    return BadRequest();

                await _rentalsRep.AddAsync(_mapper.Map<RentalDto, Rental>(rental));

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
        public async Task<ActionResult> Update(int id, RentalDto rental)
        {
            try
            {
                if (id != rental.Id)
                    return BadRequest("Rental ID mismatch");

                var RentalToUpdate = await _rentalsRep.GetByIdAsync(id);

                if (RentalToUpdate == null)
                    return NotFound($"Rental with Id = {id} not found");

                await _rentalsRep.UpdateAsync(_mapper.Map<RentalDto, Rental>(rental));
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