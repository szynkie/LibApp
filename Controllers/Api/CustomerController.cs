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
    public class CustomersController : ControllerBase
    {
        private readonly CustomersRepository _customersRep;

        private IMapper _mapper { get; }

        public CustomersController(ApplicationDbContext context, IMapper mapper)
        {
            _customersRep = new CustomersRepository(context);
            _mapper = mapper;
        }

        // GET api/Customers/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                var customers = (await _customersRep.GetAsync())
                .ToList()
                .Select(_mapper.Map<Customer, CustomerDto>);

                return Ok(customers);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting records");
            }
        }

        // GET api/Customers/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            try
            {
                var result = await _customersRep.GetByIdAsync(id);
                if (result == null) return NotFound();

                return Ok(_mapper.Map<Customer, CustomerDto>(result));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting record");
            }
        }

        // Delete api/Customers/{id}
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var CustomerToDelete = await _customersRep.GetByIdAsync(id);

                if (CustomerToDelete == null)
                    return NotFound($"Customer with Id = {id} not found");

                await _customersRep.DeleteAsync(CustomerToDelete);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while deleting record");
            }
        }

        // Post api/Customers/
        [HttpPost]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Add(CustomerDto customer)
        {
            try
            {
                if (customer == null)
                    return BadRequest();

                await _customersRep.AddAsync(_mapper.Map<CustomerDto, Customer>(customer));

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error creating new record");
            }
        }

        // Put api/Customers/{id}
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Owner")]
        public async Task<ActionResult> Update(int id, CustomerDto customer)
        {
            try
            {
                if (id != customer.Id)
                    return BadRequest("Customer ID mismatch");

                var CustomerToUpdate = await _customersRep.GetByIdAsync(id);

                if (CustomerToUpdate == null)
                    return NotFound($"Customer with Id = {id} not found");

                await _customersRep.UpdateAsync(_mapper.Map<CustomerDto, Customer>(customer));
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