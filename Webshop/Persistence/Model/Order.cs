using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Persistence.Model
{
    public class Order
    {
        [Key]
        public string OrderId { get; set; }

        public string UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderItem> Items { get; set; }

        public OrderStatus Status => Items.Select(x => x.Status).Distinct().Count() == 1
            ? Items.First().Status
            : OrderStatus.Active;

        public OrderItem GetItemByProductId(string id)
        {
            return Items.SingleOrDefault(x => x.ProductID == id);
        }
    }
}