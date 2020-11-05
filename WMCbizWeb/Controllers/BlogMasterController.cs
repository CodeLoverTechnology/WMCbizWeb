using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WMCbizWeb.Models;
using WMCbizWeb.App_Code;

namespace WMCbizWeb.Controllers
{
    public class BlogMasterController : Controller
    {
        private WMCEntities1 db = new WMCEntities1();

        // GET: BlogMaster
        public async Task<ActionResult> Index()
        {
            return View(await db.T_BlogMaster.OrderByDescending(x=>x.BlogID).Take(20).ToListAsync());
        }

        // GET: BlogMaster/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_BlogMaster t_BlogMaster = await db.T_BlogMaster.FindAsync(id);
            if (t_BlogMaster == null)
            {
                return HttpNotFound();
            }
            return View(t_BlogMaster);
        }

        // GET: BlogMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BlogMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BlogID,BlogHeadLine,ShortDescription,Keywords,FilePath,CreatedDate")] T_BlogMaster t_BlogMaster)
        {
            if (!CommonFunction.CheckUserAuthentication())
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                string FolderPath = Server.MapPath(Resources.WMCBizResources.InsertBlogFilePath);// + "\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek;

                if (!string.IsNullOrEmpty(Request.Files["FilePath"].FileName))
                {
                    string FullPathWithFileName = FolderPath + "\\" + Request.Files["FilePath"].FileName;
                    string FolderPathForImage = Request.Files["FilePath"].FileName;  //"\\" + DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.DayOfWeek + "\\" + Request.Files["StdProfilePicPath"].FileName;
                    if (CommonFunction.IsFolderExist(FolderPath))
                    {
                        Request.Files["FilePath"].SaveAs(FullPathWithFileName);
                        t_BlogMaster.FilePath = FolderPathForImage;
                    }
                }
                t_BlogMaster.CreatedDate = DateTime.Now;               
                db.T_BlogMaster.Add(t_BlogMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(t_BlogMaster);
        }

        // GET: BlogMaster/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_BlogMaster t_BlogMaster = await db.T_BlogMaster.FindAsync(id);
            if (t_BlogMaster == null)
            {
                return HttpNotFound();
            }
            return View(t_BlogMaster);
        }

        // POST: BlogMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "BlogID,BlogHeadLine,ShortDescription,Keywords,FilePath,CreatedDate")] T_BlogMaster t_BlogMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(t_BlogMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(t_BlogMaster);
        }

        // GET: BlogMaster/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_BlogMaster t_BlogMaster = await db.T_BlogMaster.FindAsync(id);
            if (t_BlogMaster == null)
            {
                return HttpNotFound();
            }
            return View(t_BlogMaster);
        }

        // POST: BlogMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            T_BlogMaster t_BlogMaster = await db.T_BlogMaster.FindAsync(id);
            db.T_BlogMaster.Remove(t_BlogMaster);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
