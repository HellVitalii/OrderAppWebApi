using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OrderApp.Models
{
    public class OrderDbInitializer : DropCreateDatabaseAlways<OrderContext>
    {
        protected override void Seed(OrderContext db)
        {
            Order order1 = new Order {Amount = 3000};
            Order order2 = new Order {Amount = 4000};

           
            db.Orders.Add(order1);
            db.Orders.Add(order2);
            db.SaveChanges();

            db.OrderItems.Add(new OrderItem { Title = "Oranges", Count = 300, Amount = 2000, OrderId = order1.Id });
            db.OrderItems.Add(new OrderItem { Title = "Apples", Count = 200, Amount = 1000, OrderId = order1.Id });
            db.OrderItems.Add(new OrderItem { Title = "Potatos", Count = 500, Amount = 4000, OrderId = order2.Id });

            base.Seed(db);
        }
    }
}