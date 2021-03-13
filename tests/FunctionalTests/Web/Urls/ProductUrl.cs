namespace Tss.FunctionalTests.Web.Urls
{
    public class ProductUrl
    {
        private readonly string _baseUrl = "/api/Product/";
        public string Index => _baseUrl;
        public string Homepage => _baseUrl + "Homepage";
        public string Filter => _baseUrl + "Filter";
        public string Detail(int id) => _baseUrl + id;
    }
}
