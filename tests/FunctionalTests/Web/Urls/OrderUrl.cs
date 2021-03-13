namespace Tss.FunctionalTests.Web.Urls
{
    public class OrderUrl
    {
        private readonly string _baseUrl = "/api/Order/";
        public string Index => _baseUrl;
        public string Payment => _baseUrl + "Payment";
        public string New => _baseUrl + "New";
        public string StockOk(int id) => _baseUrl + "StockOk/" + id;
        public string ShipOk(int id) => _baseUrl + "ShipOk/" + id;
        public string Detail(int id) => _baseUrl + id;
    }
}
