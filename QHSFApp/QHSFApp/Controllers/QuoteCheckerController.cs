using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QHSFApp.Controllers
{
    public class QuoteCheckerController : Controller
    {
        [Authorize(Roles = "SuperAdmin,Admin,Contributor,ContributorChecker,ContributorDrafter")]
        // GET: QuoteChecker
        public ActionResult Index()
        {
            return View();
        }
    }
}