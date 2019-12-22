using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Tss.Core.Constants;
using Tss.Core.Models;
using Microsoft.AspNetCore.Authorization;

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
            var dbcustomer = await _service.Get();
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbcustomer,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Users were successfully retrieved."
            };
            return Ok(response);
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int customerId)
        {
            var dbcustomer = await _service.Get(customerId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbcustomer,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "User was successfully retrieved."
            };
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] RegistrationForm input)
        {
            var dbcustomer = await _service.Create(input);
            var status = StatusCodes.Status201Created;
            var response = new Response
            {
                Content = null,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "User was created successfully.",
            };
            return CreatedAtAction(nameof(GetCustomer), new { customerId = dbcustomer.CustomerId }, response);
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginForm input)
        {
            LoginResponse result = await _service.Login(input);
            int status = StatusCodes.Status200OK;
            Response response = new Response
            {
                Content = result,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "Logged in successfully.",
            };
            return Ok(response);
        }

        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer([FromBody] RegistrationForm input)
        {
            var dbcustomer = await _service.Update(input);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbcustomer,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "User was updated successfully.",
            };
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int customerId)
        {
            var dbcustomer = await _service.Delete(customerId);
            var status = StatusCodes.Status200OK;
            var response = new Response
            {
                Content = dbcustomer,
                Status = status,
                Type = URL.GetErrorType(status),
                Title = "User was successfully deleted."
            };
            return Ok(response);
        }
    }
}