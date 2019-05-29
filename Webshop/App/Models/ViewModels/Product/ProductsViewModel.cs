using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.ViewModels.Product
{
    public class ProductsViewModel
    {
        private List<ProductViewModel> productList = new List<ProductViewModel>();

        public List<ProductViewModel> ProductList
        {
            get { return this.productList; }
            set { this.productList = value; }
        }
    }
}