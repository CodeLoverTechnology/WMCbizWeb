using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMCbizWeb.Controllers
{
    public class Accounting_ServicesController : Controller
    {
        // GET: GST
        public ActionResult GST_Return()
        {
            return View();
        }
        public ActionResult GST_Registration()
        {
            return View();
        }
    }
}