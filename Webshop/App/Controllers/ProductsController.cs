using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using App;
using App.Models;
using Persistence;
using Persistence.Model;

namespace App.Controllers
{
    public class ProductsController : ApiController
    {
        private StoreContext db = new StoreContext();

        // GET: api/Products
        public List<Product> GetProducts()
        {
            IQueryable<Product> products = db.Products;
            List<Product> toJson = new List<Product>();

            foreach (Product item in products)
            {
                Product prod = new Product()
                {
                    ID=item.ID,
                    Name = item.Name,
                    Price = item.Price,
                    Description = item.Description
                };

                toJson.Add(prod);
            }

            return toJson;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(string id)
        {

            Product product = db.Products.Where(x=>x.ID.ToString()==id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }

            Product prod = new Product()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description
            };

            return Ok(prod);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(Guid id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.ID)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = product.ID }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(Guid id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(Guid id)
        {
            return db.Products.Count(e => e.ID == id) > 0;
        }
    }
}