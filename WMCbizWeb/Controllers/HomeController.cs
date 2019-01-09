using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WMCbizWeb.Models;
//using System.Net;
//using System.Net.Mail;
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
            string Current_Url = Request.UrlReferrer.PathAndQuery;
            string [] Path = Current_Url.Split('/');
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
            if(SendUserInfo(ObjNew))
            {
                TempData["qUERYmESSAGE"] = "Thank You for Enquiry on WMC Web Portal. Our Representative get back to you soon.";
            }
            return RedirectToAction(Path[2], Path[1]);
        }


        public bool SendUserInfo(WMCUser ObjNew)
        {
            if (string.IsNullOrEmpty(ObjNew.Name))
            {
                ViewBag.Status = "Please Enter User Name.";
            }
            if (string.IsNullOrEmpty(ObjNew.Emailid))
            {
                ViewBag.Status = "Please Enter Contact No.";
            }
            if (string.IsNullOrEmpty(ObjNew.MobileNo))
            {
                ViewBag.Status = "Please Enter Email ID.";
            }

            try
            {
                MailMessage message = new MailMessage();
                message.To.Add(ObjNew.Emailid);
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                message.From = new System.Net.Mail.MailAddress("info@nja.org.in", "National Journlist Association Member Details");
                message.Bcc.Add("Indrajeetsingh9758@gmail.com");
                message.Subject = "National Journlist Association Membership Details : " + ObjNew.Name;
                message.Body = "Hi " + ObjNew.Name + ", " + System.Environment.NewLine + System.Environment.NewLine
                    + "======================================================================================== "
                    + System.Environment.NewLine + "  Member Name : " + ObjNew.Name
                    + System.Environment.NewLine + "  Contact No : " + ObjNew.MobileNo
                    + System.Environment.NewLine + "  Email ID : " + ObjNew.Emailid
                     + System.Environment.NewLine + " Dear Member, Your Membership Application has approved by Admin."
                     + System.Environment.NewLine + " Feel free to Visit National Journlist Association Portal. To Download your Membership Card."
                    + " ======================================================================================== "
                    + System.Environment.NewLine + System.Environment.NewLine
                    + System.Environment.NewLine
                    + System.Environment.NewLine
                    + "Thanks & Regards," + System.Environment.NewLine
                   + "National Journlist Association," + System.Environment.NewLine
                   + "बिष्णु कुमार अग्रहरि," + System.Environment.NewLine
                   + " पिता-प्रह्लाद प्रसाद गुप्ता," + System.Environment.NewLine
                   + "आगा हुसैन का चैराहा, हजीगंज," + System.Environment.NewLine
                   + "पटना सिटी,नगला,पटना," + System.Environment.NewLine
                   + "Email ID : nationaljournlistassociation@gmail.com/info@nja.org.in" + System.Environment.NewLine
                   + "Contact No : +91-8279556824" + System.Environment.NewLine;
                //var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                message.IsBodyHtml = false;
                SmtpClient client = new SmtpClient();
                client.Host = "mail.nja.org.in";
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential("info@nja.org.in", "India$12345");
                client.Send(message);
                ViewBag.Status = "National Journlist Association Membership Details ";
                //ModelState.Clear();
                return true;
            }
            catch (Exception ex)
            {
                ViewBag.Status = "Problem while sending email, Please check details." + ex.ToString();
            }
            return false;
        }



    }
}
