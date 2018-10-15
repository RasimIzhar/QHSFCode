using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QHSFApp.Controllers
{
    public class FinanceController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Admin,Contributor,ContributorChecker,ContributorDrafter")]
        // GET: Finance
        public ActionResult Index()
        {
            return View();
        }
    }
}