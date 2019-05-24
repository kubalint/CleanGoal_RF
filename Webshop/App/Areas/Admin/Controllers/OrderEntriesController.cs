﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using App;
using App.Models;
using Persistence;
using Persistence.Model;

namespace App.Areas.Admin.Controllers
{
    public class OrderEntriesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Admin/OrderEntries
        public ActionResult Index()
        {
            var orders = db.Orders.ToList().OrderByDescending(x=>x.Date).ToList();
            
            return View(orders);

        }

        public ActionResult Filter(string id)
        {
            Enum.TryParse(id, out OrderStatus status);

            var toView = db.Orders.Where(x => x.Status == status).ToList();

            return View("Index", toView);
        }


        public ActionResult DecreaseQuantity(string id, string orderId)
        {
            Order order = db.Orders.Include(x => x.Items).Single(x => x.OrderId == orderId);
            var item = order.GetItemByProductId(id);
            if (item.Quantity > 1)
            {
                item.Quantity--;
                db.SaveChanges();
            }

            return RedirectToAction("Details", new { id = orderId });
        }

        public ActionResult IncreaseQuantity(string id, string orderId)
        {
            Order order = db.Orders.Include(x => x.Items).Single(x => x.OrderId == orderId);
            var item = order.GetItemByProductId(id);

            item.Quantity++;
            db.SaveChanges();


            return RedirectToAction("Details", new { id = orderId });
        }

        // GET: Admin/OrderEntries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<OrderItem> orderItems =
                db.Orders.Include(x => x.Items).SingleOrDefault(x => x.OrderId == id)?.Items.ToList();
            if (orderItems == null)
            {
                return HttpNotFound();
            }

            ViewBag.orderID = id;

            return View(orderItems);
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

        //// GET: Admin/OrderEntries/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Order orderEntry = db.Orders.Find(id);
        //    if (orderEntry == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(orderEntry);
        //}

        //// POST: Admin/OrderEntries/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "OrderId,UserID,ProductID,Quantity,Price,FirstName,LastName,Address,Email,Date,Status")] Order orderEntry)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(orderEntry).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(orderEntry);
        //}

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
