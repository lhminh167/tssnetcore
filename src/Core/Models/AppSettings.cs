using System;

namespace Tss.Core.Models
{
    public class AppSettings
    {
        public AppSettings()
        {
            SendgridApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            FrontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "https://tssnextjs.vercel.app";
            StripeSecretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
            StripePublicKey = Environment.GetEnvironmentVariable("STRIPE_PUBLIC_KEY");
            CustomerServiceEmail = Environment.GetEnvironmentVariable("CUSTOMER_SERVICE_EMAIL");
            CustomerServiceName = Environment.GetEnvironmentVariable("CUSTOMER_SERVICE_NAME");
            SupplierEmail = Environment.GetEnvironmentVariable("SUPPLIER_EMAIL");
            SupplierName = Environment.GetEnvironmentVariable("SUPPLIER_NAME");
            // OrderProcessorEmail = Environment.GetEnvironmentVariable("ORDER_PROCESSOR_EMAIL");
            // OrderProcessorName = Environment.GetEnvironmentVariable("ORDER_PROCESSOR_NAME");
            AdminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
            AdminName = Environment.GetEnvironmentVariable("ADMIN_NAME");
            AuthenticationTokenSecret = Environment.GetEnvironmentVariable("AUTHORIZATION_TOKEN_SECRET");
            DatabaseConnectionString = Environment.GetEnvironmentVariable("JAWSDB_CONNECTION");
            CloudinaryCloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME");
            CloudinaryFolderName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_FOLDER");
            CloudinaryApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY");
            CloudinaryApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET");
        }

        public string SendgridApiKey { get; set; }
        public string FrontendUrl { get; set; }
        public string StripeSecretKey { get; set; }
        public string StripePublicKey { get; set; }
        public string CustomerServiceEmail { get; set; }
        public string CustomerServiceName { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierName { get; set; }
        // public string OrderProcessorEmail { get; set; }
        // public string OrderProcessorName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminName { get; set; }
        public string AuthenticationTokenSecret { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string CloudinaryCloudName { get; set; }
        public string CloudinaryFolderName { get; set; }
        public string CloudinaryApiKey { get; set; }
        public string CloudinaryApiSecret { get; set; }
    }
}
