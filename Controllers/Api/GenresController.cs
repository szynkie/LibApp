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

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly GenresRepository _genresRep;

        private IMapper _mapper { get; }

        public GenresController(ApplicationDbContext context, IMapper mapper)
        {
            _genresRep = new GenresRepository(context);
            _mapper = mapper;
        }

        // GET api/Genres/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            try
            {
                var genres = (await _genresRep.GetAsync())
                .ToList()
                .Select(_mapper.Map<Genre, GenreDto>);

                return Ok(genres);
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

                return Ok(_mapper.Map<Genre, GenreDto>(result));
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

                await _genresRep.DeleteAsync(GenreToDelete);
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
        public async Task<ActionResult> Add(GenreDto genre)
        {
            try
            {
                if (genre == null)
                    return BadRequest();

                await _genresRep.AddAsync(_mapper.Map<GenreDto, Genre>(genre));

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
        public async Task<ActionResult> Update(int id, GenreDto genre)
        {
            try
            {
                if (id != genre.Id)
                    return BadRequest("Genre ID mismatch");

                var GenreToUpdate = await _genresRep.GetByIdAsync(id);

                if (GenreToUpdate == null)
                    return NotFound($"Genre with Id = {id} not found");

                await _genresRep.UpdateAsync(_mapper.Map<GenreDto, Genre>(genre));
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