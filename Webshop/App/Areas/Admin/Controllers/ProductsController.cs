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
using App.Models.ViewModels.Product;
using App.Mappers;

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Photo).OrderBy(x=>x.CategoryID).ThenBy(x=>x.Name);
           
            ProductsViewModel productsViewModel = new ProductsViewModel();

            foreach (var product in products)
            {
                ProductViewModel pvm = CustomerProductMappers.ProductToViewModel(product);
                if (product.PhotoID != null)
                {
                    pvm.Photo = CustomerProductMappers.PhotoToViewModel(product.Photo);
                }
                productsViewModel.ProductList.Add(pvm);
            }

            return View(productsViewModel);
            
        }

        [AllowAnonymous]
        public FileContentResult GetImage(Guid id)
        {
            Photo photo = db.Photos.Find(id);

            if (photo != null)
            {
                return File(photo.PhotoFile, photo.MimeType);
            }
            else
            {
                return null;
            }
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "CategoryId", "CategoryName");
            ViewBag.PhotoID = new SelectList(db.Photos, "ID", "MimeType");
            return View();
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryID,Name,Description,Price,Available,PhotoID")] Product product, HttpPostedFileBase image)
        {
            Photo photo = new Photo();

            if (ModelState.IsValid)
            {

                product.ID = Guid.NewGuid();

                if (image != null)
                {
                    photo.ID = Guid.NewGuid();
                    photo.MimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                    db.Photos.Add(photo);
                    product.PhotoID = photo.ID;
                }

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "CategoryId", "CategoryName", product.CategoryID);
            ViewBag.PhotoID = new SelectList(db.Photos, "ID", "MimeType", product.PhotoID);
            
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(Guid? id)
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
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "CategoryId", "CategoryName", product.CategoryID);
            ViewBag.PhotoID = new SelectList(db.Photos, "ID", "MimeType", product.PhotoID);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryID,Name,Description,Price,UrlFriendlyName,Available,PhotoID")] Product product, HttpPostedFileBase image)
        {
            
            Photo photo = new Photo();

            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                
                if (image != null)
                {
                    photo.ID = Guid.NewGuid();
                    photo.MimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                    db.Photos.Add(photo);
                    product.PhotoID = photo.ID;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.ProductCategories, "CategoryId", "CategoryName", product.CategoryID);
            ViewBag.PhotoID = new SelectList(db.Photos, "ID", "MimeType", product.PhotoID);

            return View(product);
        }

        // GET: Admin/Products/DeletePhoto/
        public ActionResult DeletePhoto(string id)
        {
            Product product = db.Products.Find(Guid.Parse(id));
            Photo photoToRemove = db.Photos.FirstOrDefault(x => x.ID == product.PhotoID);

            if (product !=null && photoToRemove!= null)
            {
                product.PhotoID = null;

                db.Photos.Remove(photoToRemove);
                db.SaveChanges();
            }
            
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
