using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TemplateProject.Models;

namespace TemplateProject.Controllers
{
    public class JobsCustomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobsCustom
        public ActionResult Index()

        {
            var userId = User.Identity.GetUserId();
            var jobs = db.Jobs.Where(x=>x.UserId==userId);
            return View(jobs.ToList());
        }

        // GET: JobsCustom/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: JobsCustom/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: JobsCustom/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job , HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                //Upload Image : 
                var guidImage = Guid.NewGuid()+upload.FileName;
                var path = Path.Combine(Server.MapPath("~/Uploads"), guidImage);
                upload.SaveAs(path);
                job.JobImage = guidImage;
                job.UserId = User.Identity.GetUserId();
                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", job.CategoryId);
            return View(job);
        }

        // GET: JobsCustom/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", job.CategoryId);
            return View(job);
        }

        // POST: JobsCustom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Job job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var jobDb = db.Jobs.SingleOrDefault(x => x.Id == job.Id);
                if (upload != null)
                {
                    var guidPhoto = Guid.NewGuid() +upload.FileName;
             //update new photo :::
                    var path = Path.Combine(Server.MapPath("~/Uploads"), guidPhoto);
                    var oldPath = Path.Combine(Server.MapPath("~/Uploads"), job.JobImage);
                    upload.SaveAs(path);
                    System.IO.File.Delete(oldPath);
                    jobDb.JobImage = guidPhoto;
                }else
                {
                    //not uploaded new photo ::: 
                }
               // jobDb.JobImage = job.JobImage;
                jobDb.JobTitle = job.JobTitle;
                jobDb.JobContent = job.JobContent;
                jobDb.CategoryId = job.CategoryId;


                //db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", job.CategoryId);
            return View(job);
        }

        // GET: JobsCustom/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: JobsCustom/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,HttpPostedFileBase upload)
        {
            Job job = db.Jobs.Find(id);
            var path = Path.Combine(Server.MapPath("~/Uploads"), job.JobImage);
            System.IO.File.Delete(path);
            db.Jobs.Remove(job);
            db.SaveChanges();
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
