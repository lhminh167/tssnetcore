using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;
using Tss.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Tss.Core.Entities;

namespace Tss.Web.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(
            ICustomerService service
        )
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Users were successfully retrieved.",
                await _service.Get()
            );
            return Ok(response);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int customerId)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "User was successfully retrieved.",
                await _service.Get(customerId)
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateRequest input)
        {
            LoginResponse content = await _service.Create(input);
            Response response = new Response
            (            
                HttpStatusCode.Created,
                "User was created successfully.",
                content
            );
            return CreatedAtAction(nameof(GetCustomer), new { customerId = content.Customer.CustomerId }, response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest input)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "Logged in successfully.",
                await _service.Login(input)
            );
            return Ok(response);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateRequest input)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "User was updated successfully.",
                await _service.Update(input)
            );
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int customerId)
        {
            Response response = new Response
            (            
                HttpStatusCode.OK,
                "User was successfully deleted.",
                await _service.Delete(customerId)
            );
            return Ok(response);
        }
    }
}