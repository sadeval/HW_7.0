using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ShopDB
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int ClientId { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;

        public virtual Client? Client { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }

    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }

}
