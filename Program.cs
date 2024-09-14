using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ShopDB
{
    class Program
    {
        static void Main()
        {
            using (var context = new ShopContext())
            {

                context.Database.EnsureDeleted();  
                context.Database.EnsureCreated();  

                if (!context.Clients.Any()) 
                {
                    
                    var product1 = new Product { Name = "Asus TUF 17", Price = 36000 };
                    var product2 = new Product { Name = "Iphone 15 Pro", Price = 55000 };
                    var product3 = new Product { Name = "Acer Aspire 5", Price = 32000 };

                    
                    var client1 = new Client
                    {
                        Name = "Дмитрий Курман",
                        Email = "kurman@gmail.com",
                        Address = "ул. Пушкина, д. 1",
                        Orders = new List<Order>
                        {
                            new Order
                            {
                                OrderDate = DateTime.Now,
                                DeliveryAddress = "ул. Пушкина, д. 1",
                                OrderDetails = new List<OrderDetail>
                                {
                                    new OrderDetail { Product = product1, Quantity = 1 },
                                    new OrderDetail { Product = product2, Quantity = 2 }
                                }
                            }
                        }
                    };

                    var client2 = new Client
                    {
                        Name = "Анна Пазыныч",
                        Email = "pazynych@gmail.com",
                        Address = "ул. Попова, д. 45",
                        Orders = new List<Order>
                        {
                            new Order
                            {
                                OrderDate = DateTime.Now,
                                DeliveryAddress = "ул. Попова, д. 45",
                                OrderDetails = new List<OrderDetail>
                                {
                                    new OrderDetail { Product = product2, Quantity = 1 },
                                    new OrderDetail { Product = product3, Quantity = 1 }
                                }
                            }
                        }
                    };

                    
                    context.Clients.AddRange(client1, client2);

                   
                    context.SaveChanges();
                }

              
                var clientReports = context.Clients
                    .Select(c => new
                    {
                        ClientName = c.Name,              
                        Email = c.Email,                
                        Address = c.Address,            
                        TotalOrders = c.Orders.Count(),  
                        TotalSpent = c.Orders            
                            .SelectMany(o => o.OrderDetails)
                            .Sum(od => od.Product.Price * od.Quantity),
                        MostExpensiveProduct = c.Orders  
                            .SelectMany(o => o.OrderDetails)
                            .OrderByDescending(od => od.Product.Price)  
                            .Select(od => od.Product.Name)             
                            .FirstOrDefault()  
                    })
                    .ToList();

                
                foreach (var report in clientReports)
                {
                    Console.WriteLine($"Имя клиента: {report.ClientName}");
                    Console.WriteLine($"Email: {report.Email}");
                    Console.WriteLine($"Адрес: {report.Address}");
                    Console.WriteLine($"Общее количество заказов: {report.TotalOrders}");
                    Console.WriteLine($"Общая потраченная сумма: {report.TotalSpent} грн");
                    Console.WriteLine($"Самый дорогой товар: {report.MostExpensiveProduct}");
                    Console.WriteLine(new string('-', 30)); 
                }
            }
        }
    }
}

