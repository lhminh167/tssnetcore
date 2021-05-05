using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.Urls
{
    public class OrderUrl
    {
        private readonly string _baseUrl = "/api/Order/";
        public string Index => _baseUrl;
        public string Payment => _baseUrl + "Payment";
        public string New => _baseUrl + "New";
        public string StockOk(int id) => _baseUrl + "StockOk/" + id;
        public string StockNotOk(int id) => _baseUrl + "StockNotOk/" + id;
        public string ShipOk(int id) => _baseUrl + "ShipOk/" + id;
        public string Detail(int id) => _baseUrl + id;
        public string List(PaginationRequest input) => _baseUrl + "?" + UrlHelper.ToQueryString(input);
    }
}
