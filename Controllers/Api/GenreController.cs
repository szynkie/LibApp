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
    public class GenreController : ControllerBase
    {
        private readonly GenreRepository _genresRep;

        public GenreController(ApplicationDbContext context)
        {
            _genresRep = new GenreRepository(context);
        }

        // GET api/Genres/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            try
            {
                return (await _genresRep.GetAsync()).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting records");
            }
        }

        // GET api/Genres/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Genre>> GetGenreById(int id)
        {
            try
            {
                var result = await _genresRep.GetByIdAsync(id);
                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting record");
            }
        }

        // Delete api/Genres/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var GenreToDelete = await _genresRep.GetByIdAsync(id);

                if (GenreToDelete == null)
                    return NotFound($"Genre with Id = {id} not found");

                await _genresRep.DeleteAsync(id);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while deleting record");
            }
        }

        // Post api/Genres/
        [HttpPost]
        public async Task<ActionResult> Add(Genre Genre)
        {
            try
            {
                if (Genre == null)
                    return BadRequest();

                await _genresRep.AddAsync(Genre);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new record");
            }
        }

        // Put api/Genres/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, Genre Genre)
        {
            try
            {
                if (id != Genre.Id)
                    return BadRequest("Genre ID mismatch");

                var GenreToUpdate = await _genresRep.GetByIdAsync(id);

                if (GenreToUpdate == null)
                    return NotFound($"Genre with Id = {id} not found");

                await _genresRep.UpdateAsync(Genre);
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