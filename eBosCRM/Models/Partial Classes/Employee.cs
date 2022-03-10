using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eBosCRM.Models
{
    public partial class Employee
    {
        public string Password { get; set; }

        public string ConfirmPassord { get; set; }

        public Guid PermissionGroupID { get; set; }

        public ApplicationUser ToApplicationUser()
        {
            return new ApplicationUser
            {
                ApplicationUserID = Guid.NewGuid(),
                Username = this.FirstName,
                Password = this.Password
            };
        }
    }
}