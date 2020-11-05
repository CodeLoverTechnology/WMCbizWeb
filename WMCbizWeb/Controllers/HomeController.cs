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

            if(string.IsNullOrEmpty(name))
            {
                TempData["qUERYmESSAGE"] = "please fill the name";
                return RedirectToAction(Path[2], Path[1]);
            }
            if (string.IsNullOrEmpty(email))
            {
                TempData["qUERYmESSAGE"] = "please fill the email";
                return RedirectToAction(Path[2], Path[1]);
            }
            if (string.IsNullOrEmpty(Mobilenumber))
            {
                TempData["qUERYmESSAGE"] = "please fill the Mobile number";
                return RedirectToAction(Path[2], Path[1]);
            }


            WMCUser ObjNew = new WMCUser();
            ObjNew.Emailid = email;
            ObjNew.MobileNo = Mobilenumber;
            ObjNew.Name = name;
            ObjNew.RequestedURL = Current_Url;
            ObjNew.CreatedBy = email;
            ObjNew.Createdate = DateTime.Now;
            ObjNew.modifiedBy = email;
            ObjNew.modifieddate = DateTime.Now;
            ObjNew.Active = true;
            dbModel.WMCUsers.Add(ObjNew);
            dbModel.SaveChanges();
            if(SendUserInfo(ObjNew))
            {
                TempData["qUERYmESSAGE"] = " Dear " + ObjNew.Name + ", Your Enquiry Application has been Submitted. Thank you for WISDOM MANAGEMENT CONSULTANCY Enquiry. We will get back to you soon!!!";
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
                message.From = new System.Net.Mail.MailAddress("info@wmcbiz.com", "WISDOM MANAGEMENT CONSULTANCY Enquiry Details");
                message.Bcc.Add("csyogendrayadav@gmail.com");
                message.Bcc.Add("wmckpo@gmail.com");
                message.Bcc.Add("codelovertechnology@gmail.com");
                message.Subject = "WISDOM MANAGEMENT CONSULTANCY Enquiry Details : " + ObjNew.Name;
                message.Body = "Hi " + ObjNew.Name + ", " + System.Environment.NewLine + System.Environment.NewLine
                    + "======================================================================================== "
                    + System.Environment.NewLine + "  User Name : " + ObjNew.Name
                    + System.Environment.NewLine + "  Contact No : " + ObjNew.MobileNo
                    + System.Environment.NewLine + "  Email ID : " + ObjNew.Emailid
                     + System.Environment.NewLine + " Dear " + ObjNew.Name + ", Your Enquiry Application has been Submitted. Thank you for WISDOM MANAGEMENT CONSULTANCY Enquiry. We will get back to you soon!!!"
                     + System.Environment.NewLine + " Feel free to Visit WISDOM MANAGEMENT CONSULTANCY Web Portal."
                     + System.Environment.NewLine + " Enquiry Requested From : "+ ObjNew.RequestedURL + System.Environment.NewLine
                    + " ======================================================================================== "
                    + System.Environment.NewLine + System.Environment.NewLine
                    + System.Environment.NewLine
                    + System.Environment.NewLine
                    + "Thanks & Regards," + System.Environment.NewLine
                   + "WISDOM MANAGEMENT CONSULTANCY," + System.Environment.NewLine
                   + "CORPORATE OFFICE :-" + System.Environment.NewLine
                   + "Office No-36, S-513," + System.Environment.NewLine
                   + "Shakarpur, Delhi-110092," + System.Environment.NewLine
                   + "Phone :- 011-42785910" + System.Environment.NewLine
                   + "Email ID :- info@wmcbiz.com/wmckpo@gmail.com /csyogendrayadav@gmail.com" + System.Environment.NewLine                   
                   + "Contact No : +91-7275278701" + System.Environment.NewLine
                + System.Environment.NewLine
                   + System.Environment.NewLine
                   + "REGISTERED OFFICE :-" + System.Environment.NewLine
                   + "B-116 Joshi Colony," + System.Environment.NewLine
                   + "I.P.Extension New Delhi- 110092," + System.Environment.NewLine;
            
                //var contentType = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Pdf);
                message.IsBodyHtml = false;
                SmtpClient client = new SmtpClient();
                client.Host = "mail.wmcbiz.com";
                client.Port = 25;
                client.Credentials = new System.Net.NetworkCredential("info@wmcbiz.com", "tZvi6%85");
                client.Send(message);
               // ViewBag.Status = "Thank you for WISDOM MANAGEMENT CONSULTANCY Enquiry. We will get back to you soon!!!";
                //ModelState.Clear();
                return true;
            }
            catch (Exception ex)
            {
                ViewBag.Status = "Problem while sending email, Please check details." + ex.ToString();
            }
            return false;
        }


        public ActionResult Login()
        {
            return View();
        }
       
        [ActionName("Login")]
        [HttpPost]
        public ActionResult Login_post(FormCollection frm)
        {
            string userName = frm["txtusername"].ToString();
            string userPwd = frm["txtpassword"].ToString();

            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPwd))
            {
                if (userName == Resources.WMCBizResources.BlogerUser && userPwd == Resources.WMCBizResources.BlogPassword)
                {
                    Session["userName"] = userName;
                    Session["userPwd"] = userPwd;
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }

        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
