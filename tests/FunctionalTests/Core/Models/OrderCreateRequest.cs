using FluentValidation;

namespace Tss.FunctionalTests.Core.Models
{
    public class OrderCreateRequest
    {
        public string Comments { get; set; }
        public int ShippingId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
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

    public class OrderCreateRequestValidator : AbstractValidator<OrderCreateRequest>
    {
        public OrderCreateRequestValidator()
        {
            // RuleFor(model => model.Comments).NotEmpty();
            RuleFor(model => model.ShippingId).GreaterThan(0);
            RuleFor(model => model.CustomerId).GreaterThan(0);
            RuleFor(model => model.Name).NotEmpty();
            // RuleFor(model => model.Email).NotEmpty().EmailAddress();
            RuleFor(model => model.Address1).NotEmpty();
            // RuleFor(model => model.Address2).NotEmpty();
            RuleFor(model => model.City).NotEmpty();
            // RuleFor(model => model.Region).NotEmpty();
            RuleFor(model => model.PostalCode).NotEmpty();
            RuleFor(model => model.Country).NotEmpty();
            RuleFor(model => model.ShippingRegionId).GreaterThan(0);
            // RuleFor(model => model.DayPhone).NotEmpty();
            // RuleFor(model => model.EvePhone).NotEmpty();
            RuleFor(model => model.MobPhone).NotEmpty();
            RuleFor(model => model.PaymentMethodId).NotEmpty();
            RuleFor(model => model.PaymentId).NotEmpty();
            RuleFor(model => model.PaymentSecret).NotEmpty();
        }
    }
}
