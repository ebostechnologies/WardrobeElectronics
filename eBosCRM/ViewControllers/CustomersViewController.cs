using DevExpress.Web.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;
using Bogus;
using eBosCRM.Common;
using eBosCRM.Controllers;
using eBosCRM.Models;

namespace eBosCRM.ViewControllers
{
    public class CustomersViewController : SessionPermissionController
    {
        // GET: CustomersView
        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.Name);
            this.InitialiseViewInfos(userId);

            this.ApplyUserPermssions(this.loggedInUser, this.ownedPermssions.ToArray());

            return View(db.Customers.ToList());
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.Customers;
            return PartialView("~/Views/CustomersView/_GridViewPartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] eBosCRM.Models.Customer item)
        {
            db = new EbosEntities();
            var model = db.Customers;
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
            return PartialView("~/Views/CustomersView/_GridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] eBosCRM.Models.Customer item)
        {
            db = new EbosEntities();
            var model = db.Customers;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.CustomerID == item.CustomerID);
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
            return PartialView("~/Views/CustomersView/_GridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Guid CustomerID)
        {
            db = new EbosEntities();
            var model = db.Customers;
            if (CustomerID != null)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.CustomerID == CustomerID);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("~/Views/CustomersView/_GridViewPartial.cshtml", model.ToList());
        }
    }
}
