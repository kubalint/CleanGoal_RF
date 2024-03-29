﻿
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
using App.Models.ViewModels.Product;
using Persistence;
using Persistence.Model;

namespace App.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductCategoriesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Admin/ProductCategories
        public ActionResult Index()
        {
            var prodCategories = db.ProductCategories.ToList();

            CategoriesViewModel cvm = new CategoriesViewModel();

            foreach (var category in prodCategories)
            {
                CategoryViewModel categoryViewModel = AdminCategoryMappers.CategoryToViewModel(category);

                cvm.CategoryList.Add(categoryViewModel);
            }

            return View(cvm);
        }

        // GET: Admin/ProductCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductCategories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryId,CategoryName")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.ProductCategories.Add(productCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductCategory productCategory = db.ProductCategories.Find(id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryId,CategoryName")] ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productCategory);
        }

        // GET: Admin/ProductCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductCategory productCategory = db.ProductCategories.Find(id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }

        // POST: Admin/ProductCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductCategory productCategory = db.ProductCategories.Find(id);
            ProductCategory otherCategory = db.ProductCategories.FirstOrDefault(x => x.CategoryName == "Other");

            List<Product> productsToMove = productCategory.Products;

            foreach (Product p in productsToMove)
            {
                p.CategoryID = otherCategory.CategoryId;
            }

            db.ProductCategories.Remove(productCategory);
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
