using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Persistence.Model;

namespace App.Models
{
    public class Basket
    {
        public Guid ID { get; set; }
        private Dictionary<Product,int> productsInBasket = new Dictionary<Product,int>();

        public Dictionary<Product,int> ProductsInBasket
        {
            get { return productsInBasket; }
            set { productsInBasket = value; }
        }


    }
}