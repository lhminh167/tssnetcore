namespace Tss.FunctionalTests.Web.Urls
{
    public class CategoryUrl
    {
        private readonly string _baseUrl = "/api/Category/";
        public string Detail(int id) => _baseUrl + id;
    }
}
