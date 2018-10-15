using BAL.Repository;
using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using static DAL.Model.ViewModels;

namespace QHSFApi.Controllers
{
    public class AccessControlController : ApiController
    {
        AccessControlRepository AccessRepo;
        public AccessControlController()
        {
            AccessRepo = new AccessControlRepository(new vt_QSFHEntities());
        }
        [HttpPost]
        [Route("Api/AccessControl/VerifyUser")]
        public GetAccessControlResponse VerifyUser(Vt_Users User)
        {
            Vt_Users UserObj = AccessRepo.Verification(User);
            GetAccessControlResponse Response = new GetAccessControlResponse();
            ApiResponseHeader Header = new ApiResponseHeader();

            if (UserObj != null)
            {
                Header.Status = true;
                Header.Message = "Success";

                Response.Header = Header;
                Response.ModelObject = UserObj;
                Response.Status = true;
            }
            else
            {
                Header.Status = false;
                Header.Message = "Success";

                Response.Header = Header;
                Response.ModelObject = UserObj;
                Response.Status = false;
            }
            return Response;
        }
    }
}
