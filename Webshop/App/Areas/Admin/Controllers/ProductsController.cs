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
    public class ProductsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.Photo);
            return View(products.ToList());
        }

        // GET: Admin/Products/Details/5
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CategoryID,Name,Description,Price,Available,PhotoID")] Product product, HttpPostedFileBase image)
        {
            Photo photo = new Photo();

            if (ModelState.IsValid)
            {

                product.ID = Guid.NewGuid();
                photo.ID = Guid.NewGuid();

                photo.MimeType = image.ContentType;
                photo.PhotoFile = new byte[image.ContentLength];
                image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                db.Photos.Add(photo);
                product.PhotoID = photo.ID;
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CategoryID,Name,Description,Price,UrlFriendlyName,Available,PhotoID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.ProductCategories, "CategoryId", "CategoryName", product.CategoryID);
            ViewBag.PhotoID = new SelectList(db.Photos, "ID", "MimeType", product.PhotoID);
            return View(product);
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
