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
    
    public partial class Research
    {
        public int ResearchId { get; set; }
        public string JobTitle { get; set; }
        public System.DateTime JobDate { get; set; }
        public string ClassName { get; set; }
        public int Priority { get; set; }
    }
}
