using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobApplicationPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult Registration()
        {
            ViewBag.Message = "Registration is only for recruiters and organization members.";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login is only for recruiters and organization members.";

            return View();
        }
    }
}
