namespace Tss.Core.Entities
{
    public partial class Orders
    {
        public string GetCustomerAddressAsString()
        {
            return
            $@"{Customer.Name}
            {Customer.Address1}
            {Customer.Address2}
            {Customer.City}
            {Customer.Region}
            {Customer.PostalCode}
            {Customer.Country}";
        }

        public string GetOrderAsString(bool withCustomerDetails = true)
        {
            string orderDetails = "";

            if (withCustomerDetails)
            {
                orderDetails += $@"Customer address:
                    {GetCustomerAddressAsString()}

                
                Customer credit card:
                {Customer.CreditCard}
                ";
            }

            foreach (var item in OrderDetail)
            {
                orderDetails += $@"{item.Quantity} {item.ProductName}({item.Attributes}) ${item.UnitCost} each, total cost ${item.UnitCost * item.Quantity}";
            }

            // shipping details
            orderDetails += $@"Shipping: {Shipping.ShippingType}
                ";

            // tax details
            orderDetails += $@"Tax: {Tax.TaxType} ${GetTaxAmount()}
                ";

            orderDetails += $@"Total order cost: ${GetTaxAmount()}";

            return orderDetails;
        }
    }
}
