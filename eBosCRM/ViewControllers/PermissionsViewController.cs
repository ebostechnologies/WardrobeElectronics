using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eBosCRM.Common;
using eBosCRM.Controllers;
using eBosCRM.Models;

namespace eBosCRM.ViewControllers
{
    public class PermissionsViewController : SessionPermissionController
    {
        // GET: PermissionsView
        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.Name);
            this.InitialiseViewInfos(userId);
            this.ApplyUserPermssions(this.loggedInUser, this.ownedPermssions.ToArray());
            return View(db.PermissionGroups.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPermissions()
        {
            var model = db.PermissionGroups;
            return PartialView("~/Views/PermissionsView/_GridViewPermissions.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPermissionsAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] eBosCRM.Models.PermissionGroup item)
        {
            var model = db.PermissionGroups;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Views/PermissionsView/_GridViewPermissions.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPermissionsUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] eBosCRM.Models.PermissionGroup item)
        {
            var model = db.PermissionGroups;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.PermissionGroupID == item.PermissionGroupID);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Views/PermissionsView/_GridViewPermissions.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPermissionsDelete(System.Guid PermissionGroupID)
        {
            var model = db.PermissionGroups;
            if (PermissionGroupID != null)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.PermissionGroupID == PermissionGroupID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("~/Views/PermissionsView/_GridViewPermissions.cshtml", model.ToList());
        }
    }
}
