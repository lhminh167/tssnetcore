namespace Tss.FunctionalTests.Web.Urls
{
    public class CustomerUrl
    {
        private readonly string _baseUrl = "/api/Customer/";
        public string Index => _baseUrl;
        public string Login => _baseUrl + "Login";
        public string Detail(int id) => _baseUrl + id;
    }
}
