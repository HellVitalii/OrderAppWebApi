using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OrderApp.Models;

namespace OrderApp.Controllers
{
    public class OrderItemsController : ApiController
    {
        private OrderContext db = new OrderContext();

        public IEnumerable<OrderItem> GetOrderItem()
        {
            return db.OrderItems;
        }

        public OrderItem GetOrderItem(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            return orderItem;
        }

        // PUT: api/OrderItems/5
        [HttpPut]
        public void EditOrderItem(int id, [FromBody]OrderItem orderItem)
        {
            if (id == orderItem.Id)
            {
                
                var oldAmount = db.OrderItems.Find(id).Amount;
                if (oldAmount != orderItem.Amount)
                {
                    Order order = db.Orders.Find(orderItem.OrderId);
                    order.Amount +=  orderItem.Amount - oldAmount ;
                    db.Entry(order).State = EntityState.Modified;
                }
                OrderItem orderItemdb = db.OrderItems.Find(orderItem.Id);
                orderItemdb.Title = orderItem.Title;
                orderItemdb.Count = orderItem.Count;
                orderItemdb.Amount = orderItem.Amount;
                orderItemdb.OrderId = orderItem.OrderId;
                //db.Entry(orderItem).State = EntityState.Modified;
                //db.OrderItems.Add(orderItem);
                db.SaveChanges();
            }
        }

        // POST: api/OrderItems
        [HttpPost]
        public void CreateOrderItem([FromBody]OrderItem orderItem)
        {
            Order order = db.Orders.Find(orderItem.OrderId);
            if (order == null)
            {
                db.Orders.Add(new Order { Id = (int)orderItem.OrderId, Amount = orderItem.Amount });
            }
            else
            {
                order.Amount += orderItem.Amount;
                db.Entry(order).State = EntityState.Modified;
            }
            db.OrderItems.Add(orderItem);
            db.SaveChanges();
        }

        // DELETE: api/OrderItems/5
        public void DeleteOrderItem(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem != null)
            {
                Order order = db.Orders.Find(orderItem.OrderId);
                order.Amount -= orderItem.Amount;
                db.Entry(order).State = EntityState.Modified;

                db.OrderItems.Remove(orderItem);
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

        private bool OrderItemExists(int id)
        {
            return db.OrderItems.Count(e => e.Id == id) > 0;
        }
    }
}
