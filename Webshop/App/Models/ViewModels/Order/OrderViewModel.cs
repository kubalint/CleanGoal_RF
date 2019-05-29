using Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.ViewModels.Order
{
    public class OrderViewModel
    {
        public string OrderId { get; set; }

        public string UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus Status { get; set; }

        public int Sum { get; set; }

        private Dictionary<OrderItemViewModel, int> items = new Dictionary<OrderItemViewModel, int>();

        public Dictionary<OrderItemViewModel, int> Items
        {
            get { return this.items; }
            set { this.items = value; }
        }

    }
}