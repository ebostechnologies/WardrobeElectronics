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
using eBosCRM.Models;

namespace eBosCRM.Controllers
{
    public class CustomerCallsController : ApiController
    {
        private EbosEntities db = new EbosEntities();

        // GET: api/CustomerCalls
        public IQueryable<CustomerCall> GetCustomerCalls()
        {
            return db.CustomerCalls;
        }

        // GET: api/CustomerCalls/5
        [ResponseType(typeof(CustomerCall))]
        public IHttpActionResult GetCustomerCall(Guid id)
        {
            CustomerCall customerCall = db.CustomerCalls.Find(id);
            if (customerCall == null)
            {
                return NotFound();
            }

            return Ok(customerCall);
        }

        // PUT: api/CustomerCalls/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomerCall(Guid id, CustomerCall customerCall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customerCall.CustomerCallID)
            {
                return BadRequest();
            }

            db.Entry(customerCall).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerCallExists(id))
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

        // POST: api/CustomerCalls
        [ResponseType(typeof(CustomerCall))]
        public IHttpActionResult PostCustomerCall(CustomerCall customerCall)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomerCalls.Add(customerCall);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerCallExists(customerCall.CustomerCallID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customerCall.CustomerCallID }, customerCall);
        }

        // DELETE: api/CustomerCalls/5
        [ResponseType(typeof(CustomerCall))]
        public IHttpActionResult DeleteCustomerCall(Guid id)
        {
            CustomerCall customerCall = db.CustomerCalls.Find(id);
            if (customerCall == null)
            {
                return NotFound();
            }

            db.CustomerCalls.Remove(customerCall);
            db.SaveChanges();

            return Ok(customerCall);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerCallExists(Guid id)
        {
            return db.CustomerCalls.Count(e => e.CustomerCallID == id) > 0;
        }
    }
}