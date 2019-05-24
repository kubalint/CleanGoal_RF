using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Persistence.Model;


namespace App.Models.ViewModels
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

    public class OrdersViewModel
    {

        private List<OrderViewModel> ordersList = new List<OrderViewModel>();

        public List<OrderViewModel> OrdersList
        {
            get { return this.ordersList; }
            set { this.ordersList = value; }
        }
    }

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

        public Dictionary<OrderItemViewModel,int> Items
        {
            get { return this.items; }
            set { this.items = value; }
        }

    }

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