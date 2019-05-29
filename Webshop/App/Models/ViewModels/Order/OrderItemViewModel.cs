using Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.ViewModels.Order
{

    public class OrderItemViewModel
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public string ProductID { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public OrderStatus Status { get; set; }

    }

}