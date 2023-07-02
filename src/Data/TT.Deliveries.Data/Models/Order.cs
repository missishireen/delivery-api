using System;

namespace TT.Deliveries.Data.Models
{
    public class Order
    {
        public Order(string orderNumber, string sender)
        {
            this.Id = Guid.NewGuid();
            this.OrderNumber = orderNumber;
            this.Sender = sender;
        }

        public Guid Id { get; set; }
        public string OrderNumber { get; set; }
        public string Sender { get; set; }
    }
}
