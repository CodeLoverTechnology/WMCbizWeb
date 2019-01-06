using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WMCbizWeb.Models;

namespace WMCbizWeb.Controllers
{
    public class HomeController : Controller
    {

        private WMCEntities1 dbModel = new WMCEntities1();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Enquiry(FormCollection frm)
        {
            string name = frm["txtname"].ToString();
            string email = frm["txtemail"].ToString();
            string Mobilenumber = frm["txtnumber"].ToString();

            WMCUser ObjNew = new WMCUser();
            ObjNew.Emailid = email;
            ObjNew.MobileNo = Mobilenumber;
            ObjNew.Name = name;
            ObjNew.CreatedBy = email;
            ObjNew.Createdate = DateTime.Now;
            ObjNew.modifiedBy = email;
            ObjNew.modifieddate = DateTime.Now;
            ObjNew.Active = true;
            dbModel.WMCUsers.Add(ObjNew);
            dbModel.SaveChanges();

            ViewBag.Message = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", name, DateTime.Now.ToString());
            return View();
        }

    }
}