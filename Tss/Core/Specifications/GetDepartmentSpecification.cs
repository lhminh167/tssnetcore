using Tss.Core.Entities;
using Tss.Core.Interfaces;

namespace Tss.Core.Specifications
{
    public class GetDepartmentSpecification : BaseSpecification<Department, Department>
    {
        public GetDepartmentSpecification(IAuthHelper authHelper)
        : base(
            a
            => true
            ,
            a => a
        )
        {
            SetIsNoTracking(true);
            AddInclude(d => d.Category);
        }
    }

}
