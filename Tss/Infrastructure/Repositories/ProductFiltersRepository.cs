using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using Tss.Core.Generics;
using Tss.Core.ProductFilters;
using Tss.Infrastructure.Contexts;
using Tss.Core.Entities;
using System.Collections.Generic;

namespace Tss.Infrastructure.Repositories
{
    public class ProductFiltersRepository : IProductFiltersRepository
    {
        private readonly TssContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ProductFiltersRepository(
            TssContext context,
            IUnitOfWork unitOfWork
        )
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));            
    
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWork.Register(this);

        }

        public virtual IQueryable<ProductFiltersProduct> Products(
            ProductFiltersProductSpecifications specs
        )
        {
            var products = _context.Product.AsNoTracking().ToList();

            // Console.WriteLine("Measuring...");
            // Stopwatch sw = Stopwatch.StartNew();

            var productFiltersProducts = new List<ProductFiltersProduct>();

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

                var productFiltersProduct = new ProductFiltersProduct
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



                productFiltersProducts.Add(productFiltersProduct);

            }



            // sw.Stop();
            // long elapsed = sw.ElapsedMilliseconds; // or sw.ElapsedTicks
            // Console.WriteLine("Total query time: {0} ms", elapsed);
            // Console.WriteLine("Press any key to exit.");
            // Console.ReadKey();

            return productFiltersProducts.AsQueryable();
        }
    }
}
