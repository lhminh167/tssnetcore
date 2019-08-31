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

        public ProductFilterFilter Filter()
        {
            var attributeIds = new List<int>() { 1, 2 };

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
            ).AsNoTracking();

            var categories = (
                from c in _context.Category
                select new ProductFilterCategory
                {
                    CategoryId = c.CategoryId,
                    Name = c.Name
                }
            ).AsNoTracking();

            var ProductFilter = new ProductFilterFilter
            {
                Attributes = attributes.ToList(),
                Categories = categories.ToList()
            };

            return ProductFilter;
        }

        public virtual IQueryable<ProductFilterProduct> Product(
            ProductFilterProductSpecifications specs
        )
        {
            var products = _context.Product.AsNoTracking().ToList();

            // Console.WriteLine("Measuring...");
            // Stopwatch sw = Stopwatch.StartNew();

            var ProductFilterProducts = new List<ProductFilterProduct>();

            foreach (var p in products)
            {


                var attributes = (
                    from pa in _context.ProductAttribute
                    where pa.ProductId == p.ProductId

                    from a in _context.Attribute
                    join av in _context.AttributeValue
                    on a.AttributeId equals av.AttributeId
                    where av.AttributeValueId == pa.AttributeValueId

                    select new AttributeValue
                    {
                        AttributeId = av.AttributeId,
                        AttributeValueId = av.AttributeValueId,
                        Value = av.Value
                    }
                ).AsNoTracking().ToList();

                var ProductFilterProduct = new ProductFilterProduct
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    DiscountedPrice = p.DiscountedPrice,
                    Image = p.Image,
                    Image2 = p.Image2,
                    Thumbnail = p.Thumbnail,
                    Display = p.Display,
                    Attributes = attributes
                };



                ProductFilterProducts.Add(ProductFilterProduct);

            }



            // sw.Stop();
            // long elapsed = sw.ElapsedMilliseconds; // or sw.ElapsedTicks
            // Console.WriteLine("Total query time: {0} ms", elapsed);
            // Console.WriteLine("Press any key to exit.");
            // Console.ReadKey();

            return ProductFilterProducts.AsQueryable();
        }
    }
}
