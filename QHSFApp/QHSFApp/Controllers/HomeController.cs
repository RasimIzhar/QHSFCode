using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QHSFApp.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin,Contributor,ContributorChecker,ContributorDrafter")]
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}