using DAL.DbEntities;
using BAL.Repository;
using DAL.Model;
using System.Web.Mvc;
using System.Web.Security;
using static DAL.Model.HttpApi;
using static DAL.Model.ViewModels;
using System.Configuration;
using Newtonsoft.Json;

namespace QHSFApp.Controllers
{
    public class LoginController : Controller
    {
        private DBContext Db = new DBContext();
        AccessControlRepository AccessControlRepo;
        public LoginController()
        {
            AccessControlRepo = new AccessControlRepository(new vt_QSFHEntities());
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Vt_Users User)
        {
            
            //UserSession sess = new UserSession();
            //Session.Add("UserSession", null);
            //Session.Add("TabsSession", null);

            if (!string.IsNullOrEmpty(User.Email) && !string.IsNullOrEmpty(User.Password))
            {
                if (User.Email.ToLower() == "superadmin" && User.Password.ToLower() == "admin123")
                {
                    FormsAuthentication.SetAuthCookie("SuperAdmin", false);
                    Session["LogedInuser"] = "Super Admin";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string strResponse = CreateRequest(ConfigurationManager.AppSettings["APIHostDomain"] + "/Api/AccessControl/VerifyUser", User);
                    GetAccessControlResponse Response = JsonConvert.DeserializeObject<GetAccessControlResponse>(strResponse);
                    var UserObj = AccessControlRepo.Verification(User);
                    if (Response.Header.Status && Response.ModelObject.FirstName != null) 
                    {
                        Session["LogedInuser"] = UserObj.FirstName;
                        Session["LogedInUserID"] = UserObj.ID;
                        //sess.vt_user = UserObj;
                        //Session.Add("UserSession", sess);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        TempData["Message"] = "Incorrect Credentials";
                        return View();
                    }
                }
            }
            return RedirectToAction("Index");
        }
     
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index");
        }
    }
}