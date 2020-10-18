namespace Tss.Core.Models
{
    public class OrderCreateRequest
    {
        public string Comments { get; set; }
        public int ShippingId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // public string CreditCard { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public int ShippingRegionId { get; set; }
        public string DayPhone { get; set; }
        public string EvePhone { get; set; }
        public string MobPhone { get; set; }
        public string PaymentMethodId { get; set; }
        public string PaymentId { get; set; }
        public string PaymentSecret { get; set; }
    }
}
