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
    
    public partial class Vt_JobDrafting_Checker_Checklist
    {
        public int ID { get; set; }
        public int JobDraftingID { get; set; }
        public Nullable<int> CheckerID { get; set; }
        public Nullable<int> Drafting_Checker_QuestionID { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
    
        public virtual Vt_JobDrafting Vt_JobDrafting { get; set; }
    }
}
