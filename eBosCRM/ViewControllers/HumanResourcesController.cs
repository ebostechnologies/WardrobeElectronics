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
    public class HumanResourcesController : SessionPermissionController
    {
        // GET: HumanResources
        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.Name);
            this.InitialiseViewInfos(userId);
            this.ApplyUserPermssions(this.loggedInUser, this.ownedPermssions.ToArray());
            return View(this.db.Employees.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.Employees;
            return PartialView("~/Views/HumanResources/_GridViewPartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] eBosCRM.Models.Employee item)
        {
            var model = db.Employees;
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
            return PartialView("~/Views/HumanResources/_GridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] eBosCRM.Models.Employee item)
        {
            var model = db.Employees;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.EmployeeID == item.EmployeeID);
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
            return PartialView("~/Views/HumanResources/_GridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Guid EmployeeID)
        {
            var model = db.Employees;
            if (EmployeeID != null)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.EmployeeID == EmployeeID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("~/Views/HumanResources/_GridViewPartial.cshtml", model.ToList());
        }
    }
}