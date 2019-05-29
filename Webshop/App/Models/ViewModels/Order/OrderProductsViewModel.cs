using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Models.ViewModels.Basket;

namespace App.Models.ViewModels.Order
{
    public class OrderProductsViewModel
    {

        public BasketViewModel Basket { get; set; }

        public string OrderID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public DateTime DateOfOrder { get; set; }


    }
}