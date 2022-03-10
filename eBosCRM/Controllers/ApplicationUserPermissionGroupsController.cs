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
    public class ApplicationUserPermissionGroupsController : ApiController
    {
        private EbosEntities db = new EbosEntities();

        // GET: api/ApplicationUserPermissionGroups
        public IQueryable<ApplicationUserPermissionGroup> GetApplicationUserPermissionGroups()
        {
            return db.ApplicationUserPermissionGroups;
        }

        // GET: api/ApplicationUserPermissionGroups/5
        [ResponseType(typeof(ApplicationUserPermissionGroup))]
        public IHttpActionResult GetApplicationUserPermissionGroup(Guid id)
        {
            ApplicationUserPermissionGroup applicationUserPermissionGroup = db.ApplicationUserPermissionGroups.Find(id);
            if (applicationUserPermissionGroup == null)
            {
                return NotFound();
            }

            return Ok(applicationUserPermissionGroup);
        }

        // PUT: api/ApplicationUserPermissionGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutApplicationUserPermissionGroup(Guid id, ApplicationUserPermissionGroup applicationUserPermissionGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != applicationUserPermissionGroup.ApplicationUserPermissionGroupID)
            {
                return BadRequest();
            }

            db.Entry(applicationUserPermissionGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserPermissionGroupExists(id))
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

        // POST: api/ApplicationUserPermissionGroups
        [ResponseType(typeof(ApplicationUserPermissionGroup))]
        public IHttpActionResult PostApplicationUserPermissionGroup(ApplicationUserPermissionGroup applicationUserPermissionGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ApplicationUserPermissionGroups.Add(applicationUserPermissionGroup);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ApplicationUserPermissionGroupExists(applicationUserPermissionGroup.ApplicationUserPermissionGroupID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = applicationUserPermissionGroup.ApplicationUserPermissionGroupID }, applicationUserPermissionGroup);
        }

        // DELETE: api/ApplicationUserPermissionGroups/5
        [ResponseType(typeof(ApplicationUserPermissionGroup))]
        public IHttpActionResult DeleteApplicationUserPermissionGroup(Guid id)
        {
            ApplicationUserPermissionGroup applicationUserPermissionGroup = db.ApplicationUserPermissionGroups.Find(id);
            if (applicationUserPermissionGroup == null)
            {
                return NotFound();
            }

            db.ApplicationUserPermissionGroups.Remove(applicationUserPermissionGroup);
            db.SaveChanges();

            return Ok(applicationUserPermissionGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ApplicationUserPermissionGroupExists(Guid id)
        {
            return db.ApplicationUserPermissionGroups.Count(e => e.ApplicationUserPermissionGroupID == id) > 0;
        }
    }
}