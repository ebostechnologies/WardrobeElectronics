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
    public class ApplicationUsersController : ApiController
    {
        private EbosEntities db = new EbosEntities();

        // GET: api/ApplicationUsers
        public IQueryable<ApplicationUser> GetApplicationUsers()
        {
            return db.ApplicationUsers;
        }

        // GET: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult GetApplicationUser(Guid id)
        {
            ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return Ok(applicationUser);
        }

        // PUT: api/ApplicationUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUser(Guid id, ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUser.ApplicationUserID)
            {
                return BadRequest();
            }

            db.Entry(applicationUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
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

        // POST: api/ApplicationUsers
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult PostApplicationUser(ApplicationUser applicationUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationUsers.Add(applicationUser);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserExists(applicationUser.ApplicationUserID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUser.ApplicationUserID }, applicationUser);
        }

        // DELETE: api/ApplicationUsers/5
        [ResponseType(typeof(ApplicationUser))]
        public IHttpActionResult DeleteApplicationUser(Guid id)
        {
            ApplicationUser applicationUser = db.ApplicationUsers.Find(id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            db.ApplicationUsers.Remove(applicationUser);
            db.SaveChanges();

            return Ok(applicationUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserExists(Guid id)
        {
            return db.ApplicationUsers.Count(e => e.ApplicationUserID == id) > 0;
        }
    }
}