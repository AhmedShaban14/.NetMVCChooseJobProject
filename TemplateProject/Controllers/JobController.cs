using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateProject.Models;

namespace TemplateProject.Controllers
{
    public class JobController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Job
        public ActionResult Index()
        {
            var jobs = db.Jobs.ToList();
            return View(jobs);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = db.Categories.ToList();
            ViewBag.cats = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Job j)
        {
            if (j != null)
            {
                db.Jobs.Add(j);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                var categories = db.Categories.ToList();
                ViewBag.cats = new SelectList(categories, "Id", "Name");
                return View(j);
            }
        }



    }
}