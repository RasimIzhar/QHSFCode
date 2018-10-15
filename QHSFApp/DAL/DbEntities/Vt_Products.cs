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
    
    public partial class Vt_Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vt_Products()
        {
            this.Vt_PriceList = new HashSet<Vt_PriceList>();
            this.Vt_JobEstimationDetails = new HashSet<Vt_JobEstimationDetails>();
        }
    
        public int ID { get; set; }
        public Nullable<int> StageID { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public string Size { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public string Description { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiiedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vt_PriceList> Vt_PriceList { get; set; }
        public virtual Vt_ProductsStages Vt_ProductsStages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vt_JobEstimationDetails> Vt_JobEstimationDetails { get; set; }
    }
}
