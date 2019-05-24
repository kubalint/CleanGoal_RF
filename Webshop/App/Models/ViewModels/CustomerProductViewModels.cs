using Persistence.Model;
using System.Collections.Generic;
using System;

namespace App.Models.ViewModels
{
    public class ProductViewModel
    {
        public Guid ID { get; set; }
        
        public CategoryViewModel Category { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }

        public double Price { get; set; }
        
        public bool Available { get; set; }

        public Guid? PhotoId { get; set; }

        public PhotoViewModel Photo { get; set; }

    }
    
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
    }

    public class CategoriesViewModel
    {
        private List<CategoryViewModel> categoryList = new List<CategoryViewModel>();

        public List<CategoryViewModel> CategoryList
        {
            get { return this.categoryList; }
            set { this.categoryList = value; }
        }
    }

    public class ProductsViewModel
    {
        private List<ProductViewModel> productList = new List<ProductViewModel>();

        public List<ProductViewModel> ProductList
        {
            get { return this.productList; }
            set { this.productList = value; }
        }
    }

    public class PhotoViewModel
    {
        public Guid ID { get; set; }
        public string MimeType { get; set; }
        public byte[] PhotoFile { get; set; }
    }


}