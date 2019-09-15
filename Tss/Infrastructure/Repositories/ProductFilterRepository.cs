using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Tss.Core.Specifications;
using Tss.Infrastructure.Contexts;
using Tss.Core.Entities;
using System.Collections.Generic;
using Tss.Core.Generics;
using Tss.Core.Models;
using Tss.Core.Repositories;
using System.Threading.Tasks;

namespace Tss.Infrastructure.Repositories
{
    public class ProductFilterRepository : IProductFilterRepository
    {
        private readonly TssContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ProductFilterRepository(
            TssContext context,
            IUnitOfWork unitOfWork
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWork.Register(this);

        }

        public async Task<ProductFilterFilter> Filter()
        {
            var attributeIds = new List<int>() {
                (int)ProductFilterFilterEnum.Color,
                (int)ProductFilterFilterEnum.Size
            };

            var attributes = (
                from a in _context.Attribute
                join av in _context.AttributeValue
                on a.AttributeId equals av.AttributeId
                where attributeIds.Contains(av.AttributeId)

                select new ProductFilterAttribute
                {
                    AttributeId = a.AttributeId,
                    AttributeValueId = av.AttributeValueId,
                    Value = av.Value,
                    Name = a.Name
                }
            );

            var categories = (
                from c in _context.Category
                select new ProductFilterCategory
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                }
            );

            var ProductFilter = new ProductFilterFilter
            {
                Attributes = await attributes.AsNoTracking().ToListAsync(),
                Categories = await categories.AsNoTracking().ToListAsync()
            };

            return ProductFilter;
        }

        public async Task<List<Product>> Product(
            ProductFilterProductSpecifications specs
        )
        {
            return await _context.Product.AsNoTracking().ToListAsync();
        }
    }
}
