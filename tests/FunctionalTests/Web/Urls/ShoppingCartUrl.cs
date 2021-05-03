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
        public string MoveItemToCart(int id) => _baseUrl + "MoveItemToCart/" + id;
        public string SaveItemForLater(int id) => _baseUrl + "SaveItemForLater/" + id;
        public string GetTotalQuantity(bool buyNow) => _baseUrl + "TotalQuantity/?BuyNow=" + buyNow;
        public string Recommendation(PaginationRequest input) => _baseUrl + "Recommendation" + "?" + UrlHelper.ToQueryString(input);
    }
}
