using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using App.Models.ViewModels.Order;
using Persistence;
using Persistence.Model;

namespace App.Mappers
{
    public static class CustomerOrderMappers
    {
        public static OrdersViewModel OrdersToViewModel(List<Order> ordersList)
        {
            OrdersViewModel toReturn = new OrdersViewModel();

            foreach (var order in ordersList)
            {
                OrderViewModel ovm = OrderToViewModel(order);
                toReturn.OrdersList.Add(ovm);
            }

            return toReturn;
        }

        public static OrderViewModel OrderToViewModel(Order order)
        {
            OrderViewModel toReturn = new OrderViewModel()
            {
                Address = order.Address,
                Date = order.Date,
                Email = order.Email,
                FirstName = order.FirstName,
                LastName = order.LastName,
                OrderId = order.OrderId,
                Status = order.Status,
                UserID = order.UserID,
            };

            int quantity = order.Items.Count;

            int sum = 0;
            foreach (var item in order.Items)
            {
                toReturn.Items.Add(OrderItemToViewModel(item), quantity);
                sum += item.Price * item.Quantity;

            }

            toReturn.Sum = sum;

            return toReturn;
        }


        public static OrderItemViewModel OrderItemToViewModel(OrderItem item)
        {
            OrderItemViewModel toReturn = new OrderItemViewModel()
            {
                ID = item.ID,
                Price = item.Price,
                ProductID = item.ProductID,
                Quantity = item.Quantity,
                Status = item.Status
            };

            StoreContext db = new StoreContext();

            Product product = db.Products.SingleOrDefault(x => x.ID.ToString() == item.ProductID);

            string name = product.Name;

            toReturn.Name = name;

            return toReturn;
        }

        
            
        
    }
}