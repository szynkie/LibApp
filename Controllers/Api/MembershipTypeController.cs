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
    public class MembershipTypesController : ControllerBase
    {
        private readonly MembershipTypesRepository _MsTRep;
        private readonly IMapper _mapper;

        public MembershipTypesController(ApplicationDbContext context, IMapper mapper)
        {
            _MsTRep = new MembershipTypesRepository(context);
            _mapper = mapper;
        }

        // GET api/membershipTypes/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MembershipType>>> Get()
        {
            try
            {
                var membershipTypes = (await _MsTRep.GetAsync())
                .ToList()
                .Select(_mapper.Map<MembershipType, MembershipTypeDto>);

                return Ok(membershipTypes);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting records");
            }
        }

        // GET api/membershipTypes/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MembershipType>> GetById(int id)
        {
            try
            {
                var membershipType = await _MsTRep.GetByIdAsync(id);
                if (membershipType == null) return NotFound();

                return Ok(_mapper.Map<MembershipType, MembershipTypeDto>(membershipType));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting record");
            }
        }

        // Delete api/membershipTypes/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var membershipTypeToDelete = await _MsTRep.GetByIdAsync(id);

                if (membershipTypeToDelete == null)
                    return NotFound($"MembershipType with Id = {id} not found");

                await _MsTRep.DeleteAsync(membershipTypeToDelete);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error while deleting record {e.Message}");
            }
        }

        // Post api/books/
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Add(MembershipTypeDto membershipType)
        {
            try
            {
                if (membershipType == null)
                    return BadRequest();

                await _MsTRep.AddAsync(_mapper.Map<MembershipTypeDto, MembershipType>(membershipType));

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new record");
            }
        }

        // Put api/books/{id}
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Update(int id, MembershipTypeDto membershipType)
        {
            try
            {
                if (id != membershipType.Id)
                    return BadRequest("Book ID mismatch");

                var bookToUpdate = await _MsTRep.GetByIdAsync(id);

                if (bookToUpdate == null)
                    return NotFound($"Book with Id = {id} not found");

                await _MsTRep.UpdateAsync(_mapper.Map<MembershipTypeDto, MembershipType>(membershipType));
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