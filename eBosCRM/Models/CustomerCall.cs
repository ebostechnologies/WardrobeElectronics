//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace eBosCRM.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerCall
    {
        public System.Guid CustomerCallID { get; set; }
        public System.Guid CustomerID { get; set; }
        public int CustomerNo { get; set; }
        public System.DateTime DateOfCall { get; set; }
        public System.TimeSpan TimeOfCall { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    
        public virtual Customer Customer { get; set; }
    }
}
