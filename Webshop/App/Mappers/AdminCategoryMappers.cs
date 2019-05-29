using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Models.ViewModels.Product;
using Persistence.Model;

namespace App.Mappers
{
    public static class AdminCategoryMappers
    {
        public static CategoryViewModel CategoryToViewModel(ProductCategory category)
        {
            CategoryViewModel toReturn = new CategoryViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName
            };

            return toReturn;
        }
    }
}