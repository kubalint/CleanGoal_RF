using App.Models.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.ViewModels.Basket
{
    public class BasketViewModel
    {
        //public Guid ID { get; set; }
        private Dictionary<ProductViewModel, int> productsInBasket = new Dictionary<ProductViewModel, int>();

        public Dictionary<ProductViewModel, int> ProductsInBasket
        {
            get { return productsInBasket; }
            set { productsInBasket = value; }
        }

        public string UserId { get; set; }

        public int Sum { get; set; }
    }
}