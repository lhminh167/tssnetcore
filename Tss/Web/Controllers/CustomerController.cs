using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tss.Core.Interfaces;
using Tss.Core.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

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

        [Authorize(Roles = "Admin")]
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

        [HttpPut("{customerId}/Password")]
        public async Task<IActionResult> UpdatePassword([FromBody] CustomerPasswordUpdateRequest input)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Password was updated successfully.",
                await _service.UpdatePassword(input)
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPut("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] CustomerPasswordForgotRequest input)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Your request was successfully processed. Please check your email inbox for reset password link.",
                await _service.ForgotPassword(input)
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{customerId}/ResetPassword/{resetPasswordToken}")]
        public async Task<IActionResult> ValidateResetPasswordToken(
            [FromRoute] int customerId,
            [FromRoute] string resetPasswordToken
        )
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Success! You provided a valid reset password link.",
                await _service.ValidateResetPasswordToken(
                    new CustomerPasswordResetTokenValidationRequest(
                        customerId,
                        resetPasswordToken
                    )
                )
            );
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPut("{customerId}/ResetPassword/{resetPasswordToken}")]
        public async Task<IActionResult> ResetForgottenPassword([FromBody] CustomerPasswordResetRequest input)
        {
            Response response = new Response
            (
                HttpStatusCode.OK,
                "Password was reset successfully. A confirmation email was sent to your email address.",
                await _service.ResetForgottenPassword(input)
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