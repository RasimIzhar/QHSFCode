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
    
    public partial class Vt_JobDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vt_JobDetails()
        {
            this.JobDetailsDynamicFields = new HashSet<JobDetailsDynamicField>();
            this.JobDetailsStatics = new HashSet<JobDetailsStatic>();
        }
    
        public int ID { get; set; }
        public Nullable<int> JobID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> TabID { get; set; }
        public Nullable<int> UserAssignedID { get; set; }
        public Nullable<bool> IsApproved { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobDetailsDynamicField> JobDetailsDynamicFields { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JobDetailsStatic> JobDetailsStatics { get; set; }
        public virtual Vt_Customers Vt_Customers { get; set; }
        public virtual Vt_Jobs Vt_Jobs { get; set; }
    }
}
