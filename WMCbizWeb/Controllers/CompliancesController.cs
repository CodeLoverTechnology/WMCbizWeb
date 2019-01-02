using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMCbizWeb.Controllers
{
    public class CompliancesController : Controller
    {
        // GET: Compliances
        public ActionResult Corporatelaw_Compliance()
        {
            return View();
        }
        public ActionResult MERGERS_ACQUISITIONS()
        {
            return View();
        }
        public ActionResult FEMARBI_SERVICES()
        {
            return View();
        }
        public ActionResult CERTIFICATIONATTESTATIONSERVICES()
        {
            return View();
        }
    }
}