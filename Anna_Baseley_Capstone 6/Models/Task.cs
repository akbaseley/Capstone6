//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Anna_Baseley_Capstone_6.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public string TaskName { get; set; }
        public string Description { get; set; }
        public System.DateTime DueDate { get; set; }
        public string Complete { get; set; }
        public string UserID { get; set; }
    
        public virtual User User { get; set; }
    }
}
