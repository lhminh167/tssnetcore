using Tss.Core.Entities;

namespace Tss.Core.Specifications
{
    public class GetCustomerByEmailSpecification : BaseSpecification<Customer, Customer>
    {
        public GetCustomerByEmailSpecification(string email)
        : base(
            c => c.Email == email, 
            c => c
        )
        {
            SetIsNoTracking(true);  
        }
    }
}
