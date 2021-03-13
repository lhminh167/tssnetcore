using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Tss.FunctionalTests.Core.Models;
using System.Net;
using System.Net.Http.Headers;
using Xunit;
using Newtonsoft.Json;
using System.Text;
using Xunit.Abstractions;
using Newtonsoft.Json.Linq;
using System;

namespace Tss.FunctionalTests.Web.Fixtures
{
    public partial class WebFixture
    {
        public HttpClient Client { get; set; }
        private readonly ITestOutputHelper _output;

        public WebFixture(
            WebApplicationFactory<Startup> startup,
            ITestOutputHelper output
        )
        {
            _output = output;
            Client = startup.CreateClient();
            SetTestData().Wait();
        }

        public void SetAuthHeader(string loginToken)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginToken);
        }

        public async Task<Response<T>> GetAsync<T>(
            string requestUri,
            HttpStatusCode expectedStatusCode
        )
        {
            var message = await Client.GetAsync(requestUri);
            var response = await Deserialize<T>(message, expectedStatusCode);
            return response;
        }

        public async Task<Response<T>> PostAsync<T>(
            string requestUri,
            object requestBody,
            HttpStatusCode expectedStatusCode
        )
        {
            var content = ToHttpContent(requestBody);
            var message = await Client.PostAsync(requestUri, content);
            var response = await Deserialize<T>(message, expectedStatusCode);
            return response;
        }

        public async Task<Response<T>> PutAsync<T>(
            string requestUri,
            object requestBody,
            HttpStatusCode expectedStatusCode
        )
        {
            var content = ToHttpContent(requestBody);
            var message = await Client.PutAsync(requestUri, content);
            var response = await Deserialize<T>(message, expectedStatusCode);
            return response;
        }

        public async Task<Response<T>> DeleteAsync<T>(
            string requestUri,
            object requestBody,
            HttpStatusCode expectedStatusCode
        )
        {
            var message = await Client.DeleteAsync(requestUri);
            var response = await Deserialize<T>(message, expectedStatusCode);
            return response;
        }

        private HttpContent ToHttpContent(object input)
        {
            return new StringContent(
                JsonConvert.SerializeObject(input),
                Encoding.UTF8,
                "application/json"
            );
        }

        private async Task<Response<T>> Deserialize<T>(
            HttpResponseMessage input,
            HttpStatusCode expectedStatusCode
        )
        {
            var stringResponse = await input.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Response<T>>(stringResponse);
            await LogFailedResponse<T>(input, response, expectedStatusCode);
            return response;
        }

        private async Task LogFailedResponse<T>(
            HttpResponseMessage message,
            Response<T> responseBody,
            HttpStatusCode expectedStatusCode
        )
        {
            bool isSuccessful =
                message.StatusCode == expectedStatusCode
                && responseBody.Status == expectedStatusCode;

            if (isSuccessful) return;

            var requestContent = message.RequestMessage.Content;
            var requestBody = requestContent is null ? null : await requestContent.ReadAsStringAsync();

            _output.WriteLine(
                "Expected response status code: {0}. " +
                "Actual: {1}. " +
                Environment.NewLine +
                "Response info: {2}. " +
                Environment.NewLine +
                "Resquest body: {3}." +
                Environment.NewLine +
                "Response body: {4}.",
                expectedStatusCode,
                message.StatusCode,
                ToIndentedJsonString(message),
                ToIndentedJsonString(requestBody),
                ToIndentedJsonString(responseBody)
            );

            Assert.True(isSuccessful);
        }

        public string ToIndentedJsonString(object input)
        {
            string json = JsonConvert.SerializeObject(input);
            string jsonFormatted = JValue.Parse(json).ToString(Formatting.Indented);
            return jsonFormatted;
        }
    }
}
