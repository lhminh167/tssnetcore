namespace Tss.FunctionalTests.Web.Urls
{
    public class DepartmentUrl
    {
        private readonly string _baseUrl = "/api/Department/";
        public string Index => _baseUrl;
        public string Login => _baseUrl + "Login";
        public string Detail(int id) => _baseUrl + id;
        public string Update(int id) => $"{_baseUrl}{id}";
        public string UpdatePassword(int id) => $"{_baseUrl}{id}/Password";
    }
}
