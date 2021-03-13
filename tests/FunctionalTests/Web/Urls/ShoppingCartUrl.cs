using Tss.FunctionalTests.Core.Models;
using Tss.FunctionalTests.Core.Helpers;

namespace Tss.FunctionalTests.Web.Urls
{
    public class ShoppingCartUrl
    {
        private readonly string _baseUrl = "/api/ShoppingCart/";
        public string Index => _baseUrl;
        public string List(ShoppingCartGetRequest input) => _baseUrl + "?" + UrlHelper.ToQueryString(input);
        public string Detail(int id) => _baseUrl + id;
    }
}
