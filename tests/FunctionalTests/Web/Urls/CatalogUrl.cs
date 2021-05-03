using Tss.FunctionalTests.Core.Helpers;
using Tss.FunctionalTests.Core.Models;

namespace Tss.FunctionalTests.Web.Urls
{
    public class CatalogUrl
    {
        private readonly string _baseUrl = "/api/Catalog/";
        public string List(CatalogGetRequest input) => _baseUrl + "?" + UrlHelper.ToQueryString(input);
    }
}
