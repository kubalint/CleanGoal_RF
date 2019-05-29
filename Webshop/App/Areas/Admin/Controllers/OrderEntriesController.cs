using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App;
using App.Mappers;
using App.Models;
using App.Models.ViewModels.Order;
using Persistence;
using Persistence.Model;

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderEntriesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Admin/OrderEntries
        public ActionResult Index()
        {
            var orders = db.Orders.ToList().OrderByDescending(x=>x.Date).ToList();

            OrdersViewModel ovm = CustomerOrderMappers.OrdersToViewModel(orders);
            
            return View(ovm);

        }

        public ActionResult Filter(string id)
        {
            Enum.TryParse(id, out OrderStatus status);
            List<Order> toView = new List<Order>();

            var allOrders = db.Orders.ToList();

            foreach (var order in allOrders)
            {
                if (order.Status == status)
                {
                    toView.Add(order);
                } 
            }
            
            OrdersViewModel ovm = CustomerOrderMappers.OrdersToViewModel(toView);


            return View("Index", ovm);
        }


        public ActionResult DecreaseQuantity(string id, string orderId)
        {
            Order order = db.Orders.Where(x => x.OrderId == orderId)?.FirstOrDefault();

            OrderItem item = order.Items.SingleOrDefault(x => x.ProductID == id);

            if (item.Quantity > 1)
            {
                item.Quantity--;
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = orderId });
        }

        public ActionResult IncreaseQuantity(string id, string orderId)
        {
            Order order = db.Orders.FirstOrDefault(x => x.OrderId == orderId);

            OrderItem item = order?.Items.SingleOrDefault(x => x.ProductID == id);

            if (item != null)
            {
                item.Quantity++;
                db.SaveChanges();
            }
            
            return RedirectToAction("Details", new { id = orderId });
        }

        // GET: Admin/OrderEntries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.FirstOrDefault(x => x.OrderId == id);
            OrderViewModel ovm = CustomerOrderMappers.OrderToViewModel(order);

           
            if (ovm.Items == null)
            {
                return HttpNotFound();
            }

            ViewBag.OrderId = id;
            
            return View(ovm);
        }

        public ActionResult DeleteEntry(string id, string orderId)
        {
            Order order = db.Orders.Where(x => x.OrderId == orderId)?.FirstOrDefault();

            if (order == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            OrderItem item = order.Items.SingleOrDefault(x => x.ProductID == id);

            if (item == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            item.Status = OrderStatus.Deleted;
            db.SaveChanges();

            if (order.Status == OrderStatus.Active)
            {
                return RedirectToAction("Details", new { id = orderId });
            }
            else
            {
                return RedirectToAction("Index");
            }

        }
        
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.SingleOrDefault(x => x.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            foreach (OrderItem item in order.Items)
            {
                item.Status = OrderStatus.Deleted;
            }

            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult Deliver(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.SingleOrDefault(x => x.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            foreach (OrderItem item in order.Items)
            {
                item.Status = OrderStatus.Delivered;
            }

            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult Activate(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.SingleOrDefault(x => x.OrderId == id);
            if (order == null)
            {
                return HttpNotFound();
            }

            foreach (OrderItem item in order.Items)
            {
                item.Status = OrderStatus.Active;
            }

            db.SaveChanges();


            return RedirectToAction("Index");
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
