using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WMCbizWeb.Controllers
{
    public class StartABusinessController : Controller
    {
        // GET: StartUpBusiness
        public ActionResult StartABusiness()
        {
            return View();
        }
        public ActionResult Sole_Proprietorship()
        {
            return View();
        }
        public ActionResult Partnership_Firm()
        {
            return View();
        }
        public ActionResult Limited_Liability()
        {
            return View();
        }
        public ActionResult Partnership()
        {
            return View();
        }

        public ActionResult Private_Limited_Company()
        {
            return View();
        }
        public ActionResult Public_Limited_Company()
        {
            return View();
        }
        public ActionResult Non_Profit_Organization()
        {
            return View();
        }
        public ActionResult Society()
        {
            return View();
        }
        public ActionResult Trust()
        {
            return View();
        }

        public ActionResult Section8_Company_with_Nonprofit_objects()
        {
            return View();
        }

        #region For foreign entity
        
        public ActionResult Wholly_Owned_Foreign_Subsidiary()
        {
            return View();
        }
        
        public ActionResult Liaison_Office()
        {
            return View();
        }

        public ActionResult Branch_Office()
        {
            return View();
        }
        public ActionResult Project_Office()
        {
            return View();
        }
        public ActionResult Nidhi_Company()
        {
            return View();
        }
        #endregion


    }
}