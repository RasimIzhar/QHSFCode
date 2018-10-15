using System;
using System.Collections.Generic;
using DAL.DbEntities;

namespace DAL.Model
{
    public class VMClass 
    {
        public class AddTabsRole_ViewModel : ApiResponseHeader_ViewModel
        {
            public Users_ViewModel Users { get; set; }
            public List<TabsRole_ViewModel> TabsRoles { get; set; }
        }

        public class Users_ViewModel
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string Password { get; set; }
            public Nullable<DateTime> CreatedDate { get; set; }
            public Nullable<DateTime> ModifiedDate { get; set; }
            public Nullable<bool> IsDeleted { get; set; }
            public Nullable<bool> IsActive { get; set; }
        }

        public class TabsRole_ViewModel
        {
            public string TabID { get; set; }
            public string TabName { get; set; }
            public bool Admin { get; set; }
            public bool Contributor { get; set; }
            public bool ContributorDrafter { get; set; }
            public bool ContributorChecker { get; set; }
        }

        public class GetTabsResponse_ViewModel
        {
            public string UserName { get; set; }
            public string UserEmail { get; set; }
            public string Password { get; set; }
            public bool isavtive { get; set; }
            public ApiResponseHeader_ViewModel Header { get; set; }
            public List<vt_Tabs> ModelObject { get; set; }
            public bool Status { get; set; }
        }

        public class GetResponse_ViewModel
        {
            public ApiResponseHeader_ViewModel Header { get; set; }
            public IEnumerable<object> ModelObject { get; set; }
            public bool Status { get; set; }
        }

        public class ApiResponseHeader_ViewModel
        {
            public bool Status { get; set; }
            public string Message { get; set; }
        }
    }
}