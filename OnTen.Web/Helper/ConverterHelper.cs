﻿using OnTen.Common.Entities;
using OnTen.Web.Data.Entities;
using OnTen.Web.Data;
using OnTen.Web.Models;
using System.Globalization;
using System.Threading.Tasks;
using Product = OnTen.Web.Data.Entities.Product;

namespace OnTen.Web.Helper
{
    public class ConverterHelper : IConverterHelper
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;

        public ConverterHelper(DataContext context,
            ICombosHelper combosHelper)
        {
            _context = context;
            _combosHelper = combosHelper;
        }

        public Category ToCategory(CategoryViewModel model, string imageId, bool isNew)
        {
            return new Category
            {
                CategoryId = isNew ? 0 : model.CategoryId,
                ImageId = imageId,
                Name = model.Name
            };
        }

        public CategoryViewModel ToCategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                ImageId = category.ImageId,
                Name = category.Name
            };
        }

        public async Task<Product> ToProductAsync(ProductViewModel model, bool isNew)
        {
            return new Product
            {
                Category = await _context.Categories.FindAsync(model.CategoryId),
                Description = model.Description,
                ProductId = isNew ? 0 : model.ProductId,
                IsActive = model.IsActive,
                IsStarred = model.IsStarred,
                Name = model.Name,
                Price = ToPrice(model.PriceString),
                ProductImages = model.ProductImages
            };
        }

        public ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Categories = _combosHelper.GetComboCategories(),
                Category = product.Category,
                CategoryId = product.Category.CategoryId,
                Description = product.Description,
                ProductId = product.ProductId,
                IsActive = product.IsActive,
                IsStarred = product.IsStarred,
                Name = product.Name,
                Price = product.Price,
                PriceString = $"{product.Price}",
                ProductImages = product.ProductImages
            };
        }


        private decimal ToPrice(string priceString)
        {
            string nds = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (nds == ".")
            {
                priceString = priceString.Replace(',', '.');

            }
            else
            {
                priceString = priceString.Replace('.', ',');
            }

            return decimal.Parse(priceString);
        }

    }
}
