using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Smarts.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enroll()
        {
            return View();
        }

        public ActionResult HowItWorks()
        {
            return View();
        }
    }
}
