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

namespace Tss.FuntionalTests.Web.Controllers
{
    public class DepartmentController : IClassFixture<WebTestFixture>
    {
        public DepartmentController(WebTestFixture factory)
        {
            Client = factory.CreateClient();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task Get()
        {
            var response = await Client.GetAsync("/api/Product/Filter");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var von = JsonConvert.DeserializeObject<Response<LoginResponse>>(stringResponse);

            // Assert.Contains(".NET Bot Black Sweatshirt", stringResponse);

        }



    }
}
