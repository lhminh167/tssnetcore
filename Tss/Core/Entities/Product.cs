using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Tss.Core.Interfaces;
using Tss.Core.Models;

namespace Tss.Core.Entities
{
    public class Product
    {
        public Product(ProductCreateRequest input)
        {
            Guard.Against.Null(input, nameof(input));

            OrderDetail = new List<OrderDetail>();
            ProductAttribute = new List<ProductAttribute>();
            ProductCategory = new List<ProductCategory>();
            Review = new List<Review>();
            ShoppingCart = new List<ShoppingCart>();
            AddInfo(input);
        }

        public Product()
        {
            OrderDetail = new HashSet<OrderDetail>();
            ProductAttribute = new HashSet<ProductAttribute>();
            ProductCategory = new HashSet<ProductCategory>();
            Review = new HashSet<Review>();
            ShoppingCart = new HashSet<ShoppingCart>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double DiscountedPrice { get; set; }
        public string Image { get; set; }
        public string Image2 { get; set; }
        public string Thumbnail { get; set; }
        public short Display { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<ProductAttribute> ProductAttribute { get; set; }
        public virtual ICollection<ProductCategory> ProductCategory { get; set; }
        public virtual ICollection<Review> Review { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCart { get; set; }

        public void CheckAttributes(string attributeList)
        {
            Guard.Against.NullOrEmpty(attributeList, nameof(attributeList));

            IDictionary<string, string> dict = AttributeValue.Decode(attributeList);

            List<ProductAttribute> matchedAttributeValues = ProductAttribute?.Where(a =>
                dict.Contains(
                    new KeyValuePair<string, string>(
                        a?.AttributeValue.Name,
                        a?.AttributeValue.Value
                    )
                )
            ).ToList();

            if (dict.Count == matchedAttributeValues.Count) return;

            Guard.Against.NotFound(null, "product with such attributes was");
        }

        public void Create(ProductCreateRequest input, List<AttributeValue> attributeValues, Category category)
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.NullOrEmpty(attributeValues, nameof(attributeValues));
            Guard.Against.Null(category, nameof(category));
            input.Attributes.Except(attributeValues.Select(a => a.AttributeValueId))
            .ToList().ForEach(a => Guard.Against.NotFound(a, nameof(AttributeValue)));

            AddProductCategory(input);
            AddProductAttributes(input);
        }

        public void UpdateAttributes(ProductCreateRequest input)
        {
            Guard.Against.Null(input, nameof(input));

            UpdateProductAttributes(input);
            UpdateProductCategory(input);
        }

        private void AddProductAttributes(ProductCreateRequest input)
        {
            input.Attributes.ForEach(item => ProductAttribute.Add(new ProductAttribute(ProductId, item)));
        }

        private void AddProductCategory(ProductCreateRequest input)
        {
            ProductCategory.Add(new ProductCategory(ProductId, input.CategoryId));
        }

        private void UpdateProductAttributes(ProductCreateRequest input)
        {
            RemoveProductAttributes();
            AddProductAttributes(input);
        }

        private void UpdateProductCategory(ProductCreateRequest category)
        {
            RemoveProductCategory();
            AddProductCategory(category);
        }

        private void RemoveProductAttributes()
        {
            ProductAttribute = new List<ProductAttribute>();
        }

        private void RemoveProductCategory()
        {
            ProductCategory = new List<ProductCategory>();
        }

        public async Task Update(
            ProductUpdateRequest input,
            List<AttributeValue> attributes,
            Category category,
            IMediaHelper mediaHelper
        )
        {
            Guard.Against.Null(input, nameof(input));
            Guard.Against.Null(attributes, nameof(attributes));
            Guard.Against.Null(category, nameof(category));
            Guard.Against.Null(mediaHelper, nameof(mediaHelper));

            await RenameImages(input, mediaHelper);
            AddInfo(input);
        }

        private void AddInfo(ProductCreateRequest input)
        {
            Guard.Against.NullOrWhiteSpace(input.Name, nameof(input.Name));
            Guard.Against.NullOrWhiteSpace(input.Description, nameof(input.Description));
            Guard.Against.BadRequest(input.Price <= 0.0, "Price should be greater than zero.");
            Guard.Against.BadRequest(input.DiscountedPrice < 0.0, "Discounted price should be greater than or equal to zero.");
            Guard.Against.BadRequest(input.DiscountedPrice >= input.Price, "Discounted price should be less than price.");
            Guard.Against.BadRequest(input.Display < 0.0, "Display value should be greater than or equal to zero.");
            Guard.Against.NullOrWhiteSpace(input.Image, nameof(Image));
            Guard.Against.NullOrWhiteSpace(input.Image2, nameof(Image2));
            Guard.Against.NullOrWhiteSpace(input.Thumbnail, nameof(Thumbnail));

            Name = input.Name;
            Description = input.Description;
            Price = input.Price;
            DiscountedPrice = input.DiscountedPrice;
            Display = input.Display;
            AddImages(input);
        }

        public async Task Remove(IMediaHelper mediaHelper)
        {
            Guard.Against.Null(mediaHelper, nameof(mediaHelper));

            await DeleteImages(mediaHelper);
        }

        private string GetImageName(string productName)
        {
            productName = new Regex("[*'\",_&#^@]").Replace(productName, string.Empty);
            productName = new Regex(@"\s+").Replace(productName, "-");
            return productName.ToLower();
        }

        private List<string> GetImagePrefixes()
        {
            return new List<string>() {
                "-thumbnail",
                "",
                "-2"
            };
        }

        private List<string> GetImageNames(string productName)
        {
            string imageName = GetImageName(productName);
            List<string> imagePrefixes = GetImagePrefixes();
            return imagePrefixes.Select(prefix => imageName + prefix).ToList();
        }

        private async Task DeleteImages(IMediaHelper mediaHelper)
        {
            List<ImageDeleteRequest> images = GetImageNames(Name).Select(e => new ImageDeleteRequest(e)).ToList();
            await mediaHelper.Delete(new ImagesDeleteRequest(images));
        }

        private async Task RenameImages(ProductUpdateRequest input, IMediaHelper mediaHelper)
        {
            if (input.Name == Name) return;
            List<ImageRenameRequest> images = GetImageNames(Name)
                .Select((e, i) => new ImageRenameRequest(e, GetImageNames(input.Name)[i])).ToList();
            await mediaHelper.Rename(new ImagesRenameRequest(images));
            AddImages(input);
        }

        private void AddImages(ProductCreateRequest input)
        {
            List<string> images = GetImageNames(input.Name);

            Thumbnail = images[0];
            Image = images[1];
            Image2 = images[2];
        }
    }
}
