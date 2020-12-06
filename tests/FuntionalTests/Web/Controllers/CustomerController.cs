using System;
using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Tss;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tss.Core.Models;
using MySqlConnector;
using Tss.Core.Extensions;
using Tss.FuntionalTests.Web.Extensions;

namespace Tss.FuntionalTests.Web.Controllers
{
    public class CustomerController : IClassFixture<WebTestFixture>
    {
        public CustomerController(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task Login_RootAdminAccountPassed_ReturnsOkResult()
        {
            var body = new LoginRequest
            {
                Email = "lhminh.phs@gmail.com",
                Password = "string"
            }
            .ToJsonHttpContent();

            var response = await Client.PostAsync("/api/Customer/Login", body);
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var von = JsonConvert.DeserializeObject<Response<LoginResponse>>(stringResponse);
            Assert.Same(".NET Bot Black Sweatshirt", von.Content.Customer);

        }



    }
}
