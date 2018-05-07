using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderApp.Models;
using System.Data;

namespace OrderApp.Controllers
{
    public class OrdersController : ApiController
    {
        OrderContext db = new OrderContext();

        public IEnumerable<Order> GetOrders()
        {
            Console.WriteLine("getorders");
            return db.Orders;
        }

        public Order GetOrder(int id)
        {
            Order order = db.Orders.Find(id);
            return order;
        }



        [HttpPost]
        public void CreateOrder([FromBody]Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }



        [HttpPut]
        public void EditOrder(int id, [FromBody]Order Order)
        {
            if (id == Order.Id)
            {
                db.Entry(Order).State = EntityState.Modified;

                db.SaveChanges();
            }
        }



        public void DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                foreach (var item in db.OrderItems)
                {
                    if (item.OrderId == order.Id) db.OrderItems.Remove(item);
                }
                db.Orders.Remove(order);
                db.SaveChanges();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}