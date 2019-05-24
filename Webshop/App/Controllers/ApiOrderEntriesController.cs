using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Persistence;
using Persistence.Model;

namespace App.Controllers
{
    public class ApiOrderEntriesController : ApiController
    {
        private StoreContext db = new StoreContext();

        // GET: api/ApiOrderEntries
        public IQueryable<Order> GetOrderEntries()
        {
            return db.Orders;
        }

        // GET: api/ApiOrderEntries/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrderEntry(string id)
        {
            Order orderEntry = db.Orders.Find(id);
            if (orderEntry == null)
            {
                return NotFound();
            }

            return Ok(orderEntry);
        }

        // PUT: api/ApiOrderEntries/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutOrderEntry(string id, Order orderEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != orderEntry.OrderId)
            {
                return BadRequest();
            }

            db.Entry(orderEntry).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderEntryExists(id))
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

        // POST: api/ApiOrderEntries
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrderEntry(Order orderEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Orders.Add(orderEntry);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderEntryExists(orderEntry.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = orderEntry.OrderId }, orderEntry);
        }

        // DELETE: api/ApiOrderEntries/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrderEntry(string id)
        {
            Order orderEntry = db.Orders.Find(id);
            if (orderEntry == null)
            {
                return NotFound();
            }

            db.Orders.Remove(orderEntry);
            db.SaveChanges();

            return Ok(orderEntry);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OrderEntryExists(string id)
        {
            return db.Orders.Count(e => e.OrderId == id) > 0;
        }
    }
}