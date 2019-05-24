using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Models.ViewModels;
using Persistence.Model;

namespace App.Mappers
{
    public static class CustomerProductMappers
    {
        public static ProductViewModel ProductToViewModel(Product product)
        {
            ProductViewModel toReturn = new ProductViewModel()
            {
                ID = product.ID,
                Name = product.Name,
                Category = CategoryToViewModel(product.Category),
                Photo = PhotoToViewModel(product.Photo),
                Available = product.Available,
                Description = product.Description,
                Price = product.Price,
                PhotoId = product.PhotoID

            };
            
            return toReturn;
            
        }

        public static CategoryViewModel CategoryToViewModel(ProductCategory category)
        {
            CategoryViewModel toReturn = new CategoryViewModel();

            toReturn.CategoryId = category.CategoryId;
            toReturn.CategoryName = category.CategoryName;

            return toReturn;
        }

        public static PhotoViewModel PhotoToViewModel(Photo photo)
        {
            PhotoViewModel toReturn = new PhotoViewModel();

            toReturn.ID = photo.ID;
            toReturn.MimeType = photo.MimeType;
            toReturn.PhotoFile = photo.PhotoFile;

            return toReturn;
        }
    }
}