using App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Persistence;
using Persistence.Model;

namespace App.Controllers
{
    public class BasketController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Basket
        public ActionResult Index(string id)
        {
            
            List<BasketEntry> basketEntries =  db.BasketEntries.Where(x => x.UserID == id).ToList();
            Dictionary<Product,int> productList = new Dictionary<Product, int>();

            foreach (BasketEntry entry in basketEntries)
            {
                Product product = db.Products.Where(x => x.ID.ToString() == entry.ProductID).FirstOrDefault();
                productList.Add(product,entry.Quantity);
            }

            return View(productList);
        }

        //return RedirectToAction("Index", "Basket", new { id = Session.SessionID });
        // GET: Basket
        public ActionResult AddOne(string id)
        {
            string userID = HelperMethods.GetUserID(User, Session);

            BasketEntry entry = db.BasketEntries.Where(x => x.UserID == userID && x.ProductID == id.ToString()).SingleOrDefault();
            BasketEntry newEntry = new BasketEntry() { ProductID = entry.ProductID, UserID = userID, Quantity = entry.Quantity + 1 };
            db.BasketEntries.Remove(entry);
            db.BasketEntries.Add(newEntry);

            db.SaveChanges();

            return RedirectToAction("Index", new { id = userID });
        }

        // GET: Basket
        public ActionResult RemoveOne(string id)
        {
            string userID = HelperMethods.GetUserID(User, Session);

            BasketEntry entry = db.BasketEntries.Where(x => x.UserID == userID && x.ProductID == id.ToString()).SingleOrDefault();

            BasketEntry newEntry = new BasketEntry() { ProductID = entry.ProductID, UserID = userID, Quantity = entry.Quantity };

            if (newEntry.Quantity>1)
            {
                newEntry.Quantity--;
            }
            else
            {
                return RedirectToAction("Delete", new { id = newEntry.ProductID });
            }

            db.BasketEntries.Remove(entry);
            db.BasketEntries.Add(newEntry);

            db.SaveChanges();

            return RedirectToAction("Index", new { id = userID });
        }

        // GET: Basket
        public ActionResult Delete(string id)
        {
            string userID = HelperMethods.GetUserID(User, Session);

            BasketEntry entry = db.BasketEntries.Where(x => x.UserID == userID && x.ProductID == id.ToString()).SingleOrDefault();
            
            db.BasketEntries.Remove(entry);
            
            db.SaveChanges();

            return RedirectToAction("Index", new { id = userID });
        }

        public ActionResult EmptyBasket()
        {
            string userID = HelperMethods.GetUserID(User, Session);

            List<BasketEntry> entries = db.BasketEntries.Where(x => x.UserID == userID).ToList();

            foreach (BasketEntry entry in entries)
            {
                db.BasketEntries.Remove(entry);
            }
            
            db.SaveChanges();

            return RedirectToAction("Index", new { id = userID });
        }

        public ActionResult OrderProducts(string id)
        {
           
            Dictionary<Product, int> productList = HelperMethods.GetBasketEntriesToId(id);

            if (productList.Count == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DateTime date = DateTime.Now;
            Guid orderId = Guid.NewGuid();
            AnonymShippingInfos dto = new AnonymShippingInfos();
            dto.DateOfOrder = date;
            dto.OrderID = orderId.ToString();
            ViewBag.ProductList = productList;

            return View(dto);
        }


    }
}