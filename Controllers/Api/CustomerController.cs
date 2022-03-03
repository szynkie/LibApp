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
    public class CustomerController : ControllerBase
    {
        private readonly CustomersRepository _customersRep;

        public CustomerController(ApplicationDbContext context)
        {
            _customersRep = new CustomersRepository(context);
        }

        // GET api/Customers/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                return (await _customersRep.GetAsync()).ToList();
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

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error while getting record");
            }
        }

        // Delete api/Customers/{id}
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var CustomerToDelete = await _customersRep.GetByIdAsync(id);

                if (CustomerToDelete == null)
                    return NotFound($"Customer with Id = {id} not found");

                await _customersRep.DeleteAsync(id);
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
        public async Task<ActionResult> Add(Customer Customer)
        {
            try
            {
                if (Customer == null)
                    return BadRequest();

                await _customersRep.AddAsync(Customer);

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
        public async Task<ActionResult> Update(int id, Customer Customer)
        {
            try
            {
                if (id != Customer.Id)
                    return BadRequest("Customer ID mismatch");

                var CustomerToUpdate = await _customersRep.GetByIdAsync(id);

                if (CustomerToUpdate == null)
                    return NotFound($"Customer with Id = {id} not found");

                await _customersRep.UpdateAsync(Customer);
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