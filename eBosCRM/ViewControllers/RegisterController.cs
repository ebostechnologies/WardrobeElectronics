using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eBosCRM.Controllers;
using eBosCRM.Models;

namespace eBosCRM.ViewControllers
{
    public class RegisterController : Controller
    {
        private ApplicationUsersController usersClientService;
        private EmployeesController employeesClentService;
        private PermissionGroupsController permissionsClientService;

        public ActionResult Index()
        {
            this.permissionsClientService = new PermissionGroupsController();
            ViewBag.Permissions = this.permissionsClientService.GetPermissionGroups().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (!employee.Password.Equals(employee.ConfirmPassord))
                {
                    ViewBag.errorMessage = "Validation Failed! Passwords do not match...";
                    this.permissionsClientService = new PermissionGroupsController();
                    ViewBag.Permissions = this.permissionsClientService.GetPermissionGroups().ToList();

                    return View(employee);
                }

                var user = employee.ToApplicationUser();
                this.usersClientService = new ApplicationUsersController();
                this.usersClientService.PostApplicationUser(user);

                FormsAuthentication.SetAuthCookie(user.ApplicationUserID.ToString(), false);

                this.employeesClentService = new EmployeesController();
                employee.EmployeeID = Guid.NewGuid();
                employee.ApplicationUserID = user.ApplicationUserID;
                this.employeesClentService.PostEmployee(employee);

                //Assign the permission to the user
                var applicationUserPermission = new ApplicationUserPermissionGroup
                {
                    ApplicationUserPermissionGroupID = Guid.NewGuid(),
                    ApplicationUserID = user.ApplicationUserID,
                    PermissionGroupID = employee.PermissionGroupID
                };

                new ApplicationUserPermissionGroupsController().PostApplicationUserPermissionGroup(
                    applicationUserPermission);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                this.permissionsClientService = new PermissionGroupsController();
                ViewBag.Permissions = this.permissionsClientService.GetPermissionGroups().ToList();

                return View(employee);
            }
        }
    }
}