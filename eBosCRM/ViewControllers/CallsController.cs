using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bogus;
using eBosCRM.Common;
using eBosCRM.Controllers;
using eBosCRM.Models;

namespace eBosCRM.ViewControllers
{
    public class CallsController : SessionPermissionController
    {
        // GET: Calls
        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.Name);
            this.InitialiseViewInfos(userId);

            this.ApplyUserPermssions(this.loggedInUser, this.ownedPermssions.ToArray());

            return View(db.CustomerCalls.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewCustomerCalls()
        {
            db = new EbosEntities();
            var model = db.CustomerCalls.ToList();
            return PartialView("~/Views/Calls/_GridViewCustomerCalls.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewCustomerCallsAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] eBosCRM.Models.CustomerCall item)
        {
            db = new EbosEntities();

            var model = db.CustomerCalls;
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
            return PartialView("~/Views/Calls/_GridViewCustomerCalls.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewCustomerCallsUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] eBosCRM.Models.CustomerCall item)
        {
            db = new EbosEntities();

            var model = db.CustomerCalls;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.CustomerCallID == item.CustomerCallID);
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
            return PartialView("~/Views/Calls/_GridViewCustomerCalls.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewCustomerCallsDelete(System.Guid CustomerCallID)
        {
            db = new EbosEntities();
            var model = db.CustomerCalls;
            if (CustomerCallID != null)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.CustomerCallID == CustomerCallID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("~/Views/Calls/_GridViewCustomerCalls.cshtml", model.ToList());
        }
    }
}