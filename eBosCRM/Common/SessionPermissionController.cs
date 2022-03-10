using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eBosCRM.Models;

namespace eBosCRM.Common
{
    public abstract class SessionPermissionController : Controller
    {
        protected ApplicationUser loggedInUser { get; set; }

        protected IEnumerable<PermissionGroup> ownedPermssions { get; set; }

        protected EbosEntities db;

        public SessionPermissionController()
        {
            db = new EbosEntities();
        }

        protected void InitialiseViewInfos(Guid userId)
        {
            this.loggedInUser = db.ApplicationUsers.First(u => u.ApplicationUserID == userId);

            ViewData["Username"] = this.loggedInUser.Username;

            this.ownedPermssions = db.ApplicationUserPermissionGroups
                .Where(g => g.ApplicationUserID == loggedInUser.ApplicationUserID).Include(p => p.PermissionGroup)
                .Select(p => p.PermissionGroup).ToList();
        }
        protected void ApplyUserPermssions(ApplicationUser user, params PermissionGroup[] ownedPermissions)
        {
            ViewBag.IsDirector = PermissionManager.UserHasPermission(PermissionGroupTypes.Director, ownedPermissions);
            ViewBag.IsEmployee = PermissionManager.UserHasPermission(PermissionGroupTypes.Employee, ownedPermissions);
            ViewBag.IsAdmin = PermissionManager.UserHasPermission(PermissionGroupTypes.Administrator, ownedPermissions);
            ViewBag.IsManager = PermissionManager.UserHasPermission(PermissionGroupTypes.Manager, ownedPermissions);
        }
    }

}