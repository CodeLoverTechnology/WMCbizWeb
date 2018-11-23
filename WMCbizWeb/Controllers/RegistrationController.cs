using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMCbizWeb.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult ESI()
        {
            return View();
        }
        public ActionResult FSSAI()
        {
            return View();
        }
        public ActionResult IEC()
        {
            return View();
        }
        public ActionResult MSME()
        {
            return View();
        }

    }
}