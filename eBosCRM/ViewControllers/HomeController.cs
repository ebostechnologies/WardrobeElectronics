using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using eBosCRM.Common;
using eBosCRM.Models;

namespace eBosCRM.Controllers
{
    public class HomeController : SessionPermissionController
    {
        [System.Web.Mvc.Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.Name);
            this.InitialiseViewInfos(userId);

            this.ApplyUserPermssions(this.loggedInUser, this.ownedPermssions.ToArray());

            var employee = db.Employees.First(e => e.ApplicationUserID == this.loggedInUser.ApplicationUserID);

            return View(employee);
        }
    }
}
