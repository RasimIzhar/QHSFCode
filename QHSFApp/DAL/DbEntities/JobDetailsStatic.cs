//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.DbEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobDetailsStatic
    {
        public int ID { get; set; }
        public Nullable<int> JobDetailsID { get; set; }
        public Nullable<bool> StaticEnggForm15 { get; set; }
        public Nullable<bool> StaticEnggForm16 { get; set; }
        public Nullable<bool> StaticFreightSite { get; set; }
        public Nullable<bool> Installation { get; set; }
    
        public virtual Vt_JobDetails Vt_JobDetails { get; set; }
    }
}
