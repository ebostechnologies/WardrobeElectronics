using DevExpress.Web.Mvc;
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
    public class PermissionsController : ApiController
    {
        private EbosEntities db = new EbosEntities();

        // GET: api/Permissions
        public IQueryable<Permission> GetPermissions()
        {
            return db.Permissions;
        }

        // GET: api/Permissions/5
        [ResponseType(typeof(Permission))]
        public IHttpActionResult GetPermission(Guid id)
        {
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return NotFound();
            }

            return Ok(permission);
        }

        // PUT: api/Permissions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPermission(Guid id, Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != permission.PermissionID)
            {
                return BadRequest();
            }

            db.Entry(permission).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
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

        // POST: api/Permissions
        [ResponseType(typeof(Permission))]
        public IHttpActionResult PostPermission(Permission permission)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Permissions.Add(permission);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PermissionExists(permission.PermissionID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = permission.PermissionID }, permission);
        }

        // DELETE: api/Permissions/5
        [ResponseType(typeof(Permission))]
        public IHttpActionResult DeletePermission(Guid id)
        {
            Permission permission = db.Permissions.Find(id);
            if (permission == null)
            {
                return NotFound();
            }

            db.Permissions.Remove(permission);
            db.SaveChanges();

            return Ok(permission);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PermissionExists(Guid id)
        {
            return db.Permissions.Count(e => e.PermissionID == id) > 0;
        }
    }
}