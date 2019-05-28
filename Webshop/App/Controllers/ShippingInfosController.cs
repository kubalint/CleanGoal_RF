using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Persistence;
using Persistence.Model;

namespace App.Controllers
{
    public class ShippingInfosController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: ShippingInfos
        public ActionResult Index()
        {
            return View(db.Shippings.Where(x=>x.Email==User.Identity.Name).ToList());
        }
        
        // GET: ShippingInfos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShippingInfos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Address")] ShippingInfos shippingInfos)
        {
            if (ModelState.IsValid)
            {
                shippingInfos.ID = Guid.NewGuid();
                shippingInfos.Email = User.Identity.Name;
                db.Shippings.Add(shippingInfos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shippingInfos);
        }

        // GET: ShippingInfos/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingInfos shippingInfos = db.Shippings.Find(id);
            if (shippingInfos == null)
            {
                return HttpNotFound();
            }
            return View(shippingInfos);
        }

        // POST: ShippingInfos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Address")] ShippingInfos shippingInfos)
        {
            shippingInfos.Email = User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.Entry(shippingInfos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shippingInfos);
        }

        // GET: ShippingInfos/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShippingInfos shippingInfos = db.Shippings.Find(id);
            if (shippingInfos == null)
            {
                return HttpNotFound();
            }
            return View(shippingInfos);
        }

        // POST: ShippingInfos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ShippingInfos shippingInfos = db.Shippings.Find(id);
            db.Shippings.Remove(shippingInfos);
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
