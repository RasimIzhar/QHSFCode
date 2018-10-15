using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QHSFApp.Models
{
    public class Authorization : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            if(httpContext.Session["validUser"] != null)
            {
                return true;
            }


              return false;
        }

    }
}