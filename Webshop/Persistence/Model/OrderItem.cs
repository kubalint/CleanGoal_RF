using System;

namespace Persistence.Model
{
    public class OrderItem
    {
        public Guid ID { get; set; }

        public string ProductID { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public OrderStatus Status { get; set; }

    }
}