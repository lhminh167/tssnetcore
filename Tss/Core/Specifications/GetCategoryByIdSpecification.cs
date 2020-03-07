using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetCategoryByIdSpecification : BaseSpecification<Category, Category>
    {
        public GetCategoryByIdSpecification(int categoryId, IAuthHelper authHelper)
        : base(
            a 
            => a.CategoryId == categoryId
            ,
            a => a
        )
        {
            SetIsNoTracking(true);
            AddInclude("ProductCategory.Product");
        }
    }

}
