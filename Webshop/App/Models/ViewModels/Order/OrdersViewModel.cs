using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.ViewModels.Order
{
    public class OrdersViewModel
    {

        private List<OrderViewModel> ordersList = new List<OrderViewModel>();

        public List<OrderViewModel> OrdersList
        {
            get { return this.ordersList; }
            set { this.ordersList = value; }
        }
    }
}