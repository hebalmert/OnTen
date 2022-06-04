﻿using OnTen.Common.Entities;
using OnTen.Web.Models;

namespace OnTen.Web.Helper
{
    public interface IConverterHelper
    {
        Category ToCategory(CategoryViewModel model, string imageId, bool isNew);

        CategoryViewModel ToCategoryViewModel(Category category);

        //Task<Product> ToProductAsync(ProductViewModel model, bool isNew);

        //ProductViewModel ToProductViewModel(Product product);
    }
}
