using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab2.Models;

namespace Lab2.Data
{
    public class DbInitializer
    {
        public static void Initialize(OrderingContext context)
        {
            context.Database.EnsureCreated();

            if (context.Order.Any())
            {
                return;
            }

            var orders = new Order[]
            {
                //new Order{Date= new DateTime(1991, 12, 31, 22, 56, 59), Sum = 3000, UserId = 1, User=context.User.FirstOrDefault()},
               // new Order{Date= new DateTime(2000, 12, 31, 22, 56, 59),Sum = 10000, UserId = 2, User=context.User.FirstOrDefault()},
               // new Order{Url="http://orders.msdn.com/visualstudio"}

            };
            foreach (Order b in orders)
            {
                context.Order.Add(b);
            }
            context.SaveChanges();

            var products = new Product[]
            {
                new Product { Name="Спортивная куртка на молнии",Costs=3000  },
                new Product { Name="Спортивный зимний костюм",Costs=5000 }
            };
            foreach (Product p in products)
            {
                context.Product.Add(p);
            }
            var users = new User[]
            {
               // new User { Username="Ivanov",Email = "ivanov@mail.ru" },
               // new User { Username="Petrov",Email = "petrov@mail.ru"  }
            };
            foreach (User p in users)
            {
                context.User.Add(p);
            }
            context.SaveChanges();

            var os = new OrderString[]
            {
                new OrderString { OrderId = 1, ProductId = 1, Cost = 3000, Count = 1 },
                new OrderString { OrderId = 2, ProductId = 2, Cost = 10000, Count = 2 }
            };
            foreach (OrderString p in os)
            {
                context.OrderString.Add(p);
            }
            context.SaveChanges();
        }
    }
}
