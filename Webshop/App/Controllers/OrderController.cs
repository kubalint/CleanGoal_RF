using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Models;
using Persistence;
using Persistence.Model;
using System.Data.Entity;


namespace App.Controllers
{
    public class OrderController : Controller
    {

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "FirstName,LastName,Address,Email,DateOfOrder,OrderID")] AnonymShippingInfos dto)
        {
            StoreContext db = new StoreContext();
            string userID = HelperMethods.GetUserID(User, Session);
            List<OrderItem> itemList = new List<OrderItem>();
            Dictionary<Product, int> productList = HelperMethods.GetBasketEntriesToId(userID);

            foreach (var entry in productList)
            {
                OrderItem item = new OrderItem();
                item.Quantity = entry.Value;
                item.Status = OrderStatus.Active;
                item.ProductID = entry.Key.ID.ToString();
                item.Price = (int)entry.Key.Price;
                item.ID = Guid.NewGuid();

                itemList.Add(item);
            }


            Order order = new Order()
            {
                OrderId = dto.OrderID,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Address = dto.Address,
                Date = dto.DateOfOrder,
                UserID = userID,
                Items = itemList

            };

            db.Orders.Add(order);
            db.SaveChanges();




            List<BasketEntry> basketEntries = db.BasketEntries.Where(x => x.UserID == userID).ToList();

            foreach (BasketEntry entry in basketEntries)
            {
                db.BasketEntries.Remove(entry);
            }

            db.SaveChanges();

            //return RedirectToAction("Index");



            return View(dto);
        }

        public ActionResult GetOrders(string id)
        {
            StoreContext db = new StoreContext();

            var orderList = db.Orders.Where(x => x.UserID == id).ToList();

            return View(orderList);
        }

        public ActionResult Details(string id)
        {
            StoreContext db = new StoreContext();

            var order = db.Orders.SingleOrDefault(x => x.OrderId == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

    }
}