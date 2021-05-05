using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.Urls
{
    public class ReviewUrl
    {
        private readonly string _apiUrl = "/api";
        private readonly string _moduleUrl = "/Review/";
        private string _baseUrl => _apiUrl + _moduleUrl;
        public string Index => _baseUrl;
        public string List(ReviewGetRequest input) => new ProductUrl().Detail(input.ProductId) +
            _moduleUrl + "?" + UrlHelper.ToQueryString(input);
        public string Detail(int id) => _baseUrl + id;
        public string Update(int id) => $"{_baseUrl}{id}";
    }
}
