using System;
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

namespace App.Controllers
{
    public class ProductController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Product
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).OrderBy(x=>x.Name).ToList();
            return View(products);
        }

        public ActionResult Name(string urlFriendlyName)
        {
            IEnumerable<Product> query = db.Products.Where(x => x.UrlFriendlyName == urlFriendlyName);

            if (query.ToList().Count>0)
            {
                return View("Index", query.ToList());
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
        }

        // GET: Product/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Product/AddToBasket
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpGet]        
        //public ActionResult AddToBasket([Bind(Include = "ID,CategoryID,Name,Description,Price,UrlFriendlyName,Available")] Product product)
        // /Product/AddToBasket/fbd5af80-a929-4e92-a2da-908e2bd8eb2e
        public ActionResult AddToBasket(Guid id)
        {
            string userID = HelperMethods.GetUserID(User, Session);


            if (ModelState.IsValid)
            {
                if (db.BasketEntries.Where(x => x.UserID == userID && x.ProductID == id.ToString()).Count() == 0)
                {
                    BasketEntry entry = new BasketEntry()
                    {
                        UserID = userID,
                        ProductID = id.ToString(),
                        Quantity=1
                    };
                    db.BasketEntries.Add(entry);
                }else 
                {
                    BasketEntry old = db.BasketEntries.Where(x => x.UserID == userID && x.ProductID == id.ToString()).FirstOrDefault();
                    BasketEntry newEntry = new BasketEntry { UserID=old.UserID, ProductID=old.ProductID, Quantity=old.Quantity};
                    newEntry.Quantity++;

                    db.BasketEntries.Remove(old);
                    db.BasketEntries.Add(newEntry);
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Basket", new { id = userID });
            }

            //ViewBag.CategoryID = new SelectList(db.ProductCategories, "CategoryId", "CategoryName", product.CategoryID);
            return View("Index");
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
