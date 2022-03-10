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
using Bogus;
using eBosCRM.Models;

namespace eBosCRM.Controllers
{
    public class PermissionGroupsController : ApiController
    {
        private EbosEntities db = new EbosEntities();

        // GET: api/PermissionGroups
        public IQueryable<PermissionGroup> GetPermissionGroups()
        {
            return db.PermissionGroups;
        }

        // GET: api/PermissionGroups/5
        [ResponseType(typeof(PermissionGroup))]
        public IHttpActionResult GetPermissionGroup(Guid id)
        {
            PermissionGroup permissionGroup = db.PermissionGroups.Find(id);
            if (permissionGroup == null)
            {
                return NotFound();
            }

            return Ok(permissionGroup);
        }

        // PUT: api/PermissionGroups/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermissionGroup(Guid id, PermissionGroup permissionGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permissionGroup.PermissionGroupID)
            {
                return BadRequest();
            }

            db.Entry(permissionGroup).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionGroupExists(id))
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

        // POST: api/PermissionGroups
        [ResponseType(typeof(PermissionGroup))]
        public IHttpActionResult PostPermissionGroup(PermissionGroup permissionGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PermissionGroups.Add(permissionGroup);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PermissionGroupExists(permissionGroup.PermissionGroupID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = permissionGroup.PermissionGroupID }, permissionGroup);
        }

        // DELETE: api/PermissionGroups/5
        [ResponseType(typeof(PermissionGroup))]
        public IHttpActionResult DeletePermissionGroup(Guid id)
        {
            PermissionGroup permissionGroup = db.PermissionGroups.Find(id);
            if (permissionGroup == null)
            {
                return NotFound();
            }

            db.PermissionGroups.Remove(permissionGroup);
            db.SaveChanges();

            return Ok(permissionGroup);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionGroupExists(Guid id)
        {
            return db.PermissionGroups.Count(e => e.PermissionGroupID == id) > 0;
        }
    }
}