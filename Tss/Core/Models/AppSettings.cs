using System;

namespace Tss.Core.Models
{
    public class AppSettings
    {
        public string SendgridApiKey { get; set; } = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        public string StripeSecretKey { get; set; } = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
        public string StripePublicKey { get; set; } = Environment.GetEnvironmentVariable("STRIPE_PUBLIC_KEY");
        public string CustomerServiceEmail { get; set; } = Environment.GetEnvironmentVariable("CUSTOMER_SERVICE_EMAIL");
        public string CustomerServiceName { get; set; } = Environment.GetEnvironmentVariable("CUSTOMER_SERVICE_NAME");
        public string SupplierEmail { get; set; } = Environment.GetEnvironmentVariable("SUPPLIER_EMAIL");
        public string SupplierName { get; set; } = Environment.GetEnvironmentVariable("SUPPLIER_NAME");
        public string OrderProcessorEmail { get; set; } = Environment.GetEnvironmentVariable("ORDER_PROCESSOR_EMAIL");
        public string OrderProcessorName { get; set; } = Environment.GetEnvironmentVariable("ORDER_PROCESSOR_NAME");
        public string AdminEmail { get; set; } = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
        public string AdminName { get; set; } = Environment.GetEnvironmentVariable("ADMIN_NAME");
        public string AuthenticationTokenSecret { get; set; } = Environment.GetEnvironmentVariable("AUTHORIZATION_TOKEN_SECRET");
        public string DatabaseConnectionString { get; set; } = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");
    }
}
