//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _2110181055_MVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class lecturer
    {
        public int lecturer_id { get; set; }
        public int university_id { get; set; }
        public int faculty_id { get; set; }
        public string lecturer_name { get; set; }
        public string degree { get; set; }
        public string gender { get; set; }
    
        public virtual faculty faculty { get; set; }
        public virtual university university { get; set; }
    }
}
