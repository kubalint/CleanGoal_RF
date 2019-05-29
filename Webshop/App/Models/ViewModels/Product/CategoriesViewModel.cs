using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.ViewModels.Product
{
    public class CategoriesViewModel
    {
        private List<CategoryViewModel> categoryList = new List<CategoryViewModel>();

        public List<CategoryViewModel> CategoryList
        {
            get { return this.categoryList; }
            set { this.categoryList = value; }
        }
    }
}