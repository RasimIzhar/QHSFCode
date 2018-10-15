using BAL.Repository;
using DAL.DbEntities;
using System.Collections.Generic;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class UsersController : ApiController
    {
        private UsersRepository UserRepo;
        private TabsRepository TabsRepo;

        public UsersController()
        {
            UserRepo = new UsersRepository(new vt_QSFHEntities());
            TabsRepo = new TabsRepository(new vt_QSFHEntities());
        }

        [HttpGet]
        [Route("Api/Users/GetUsers")]
        public GetResponse GetUsers()
        {
            List<Vt_Users> User = UserRepo.GetUsers();
            GetResponse Response = new GetResponse();
            ApiResponseHeader Header = new ApiResponseHeader();
            Header.Status = true;
            Header.Message = "Success";

            Response.Header = Header;
            Response.ModelObject = User;
            Response.Status = true;
            return Response;
        }

        //If Email Exists
        [HttpPost]
        [Route("Api/Users/IsEmailExists")]
        public GetResponse IsEmailExists(string Email)
        {
            bool Result = UserRepo.IsEmailExists(Email);
            GetResponse Response = new GetResponse();
            ApiResponseHeader Header = new ApiResponseHeader();
            if (!Result)
            {
                Header.Status = false;
                Header.Message = "Email already exists";

                Response.Header = Header;
                Response.Status = false;
            }
            else
            {
                Header.Status = true;
                Header.Message = "Success";
                Response.Header = Header;
                Response.Status = true;
            }
            return Response;
        }

        [HttpGet]
        [Route("Api/Users/GettTabs")]
        public GetTabsResponse GettTabs()
        {
            List<vt_Tabs> tabs = TabsRepo.GetTabs();

            GetTabsResponse Response = new GetTabsResponse();
            ApiResponseHeader Header = new ApiResponseHeader();
            Header.Status = true;
            Header.Message = "Success";

            Response.Header = Header;
            Response.ModelObject = tabs;
            Response.Status = true;
            return Response;
        }

        [HttpPost]
        [Route("Api/Users/SaveUserRoles")]
        public GetTabsResponse SaveUserRoles(AddTabsDataNew Obj)
        {
            ApiResponseHeader Header = new ApiResponseHeader();
            GetTabsResponse Response = new GetTabsResponse();

            AddTabsDataNew TabsData = new AddTabsDataNew();

            TabsData.Tabs = Obj.Tabs;
            TabsData.TabUser = Obj.TabUser;

            bool Result = UserRepo.Create(TabsData);
            if (Result)
            {
                Header.Status = true;
                Header.Message = "Success";
                Response.Header = Header;
                //Response.ModelObject = TabsData;
                Response.Status = true;
                return Response;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("Api/Users/EditUserRoles")]
        public GetEditRecord EditUserRoles(int id)
        {
            GetEditRecord Response = new GetEditRecord();
            ApiResponseHeader Header = new ApiResponseHeader();
            var data = UserRepo.EditUser(id);
            if (data != null)
            {
                Header.Status = true;
                Header.Message = "Success";
                Response.Header = Header;
                Response.Status = true;
                Response.ModelObject = data;
                //Response.ModelObject = TabsData;

                return Response;
            }
            else
            {
            }

            return null;
        }

        [HttpPost]
        [Route("Api/Users/EditSaveUserRoles")]
        public GetTabsResponse EditSaveUserRoles(AddTabsDataNew Obj)
        {
            ApiResponseHeader Header = new ApiResponseHeader();
            GetTabsResponse Response = new GetTabsResponse();

            AddTabsDataNew TabsData = new AddTabsDataNew();

            TabsData.Tabs = Obj.Tabs;
            TabsData.TabUser = Obj.TabUser;

            bool Result = UserRepo.SaveUserRoles(TabsData);
            if (Result)
            {
                Header.Status = true;
                Header.Message = "Success";
                Response.Header = Header;
                //Response.ModelObject = TabsData;
                Response.Status = true;
                return Response;
            }
            else
            {
                return null;
            }
        }

        #region SALMAN_CODE

        [HttpGet]
        [Route("Api/Users/GetUserWrtRoles")]
        public object GetUserWrtRoles(int TabID, int RoleID)
        {
            var Data = UserRepo.UsersWrtRoles(TabID, RoleID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }
        [HttpGet]
        [Route("Api/Users/GetUserWrtRoles_Drafting")]
        public object GetUserWrtRoles_Drafting(int TabID,int Checker_RoleID, int Drafter_RoleID)
        {
            var Data = UserRepo.UsersWrtRoles_Drafting(TabID, Checker_RoleID, Drafter_RoleID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("Api/Users/GetUserDetails")]
        public Vt_Users GetUserDetails(int UserID)
        {
            //Get Single User Record
            var Data = UserRepo.GetUsers(UserID);
            if (Data != null)
            {
                return Data;
            }
            else
            {
                return null;
            }
        }

        #endregion SALMAN_CODE
    }
}