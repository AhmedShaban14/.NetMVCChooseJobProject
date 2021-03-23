using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateProject.Models;

namespace TemplateProject.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Category
        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {

            return PartialView("Create");
        }

        [HttpPost]
        public ActionResult Create(Category c)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(c);
                db.SaveChanges();
                return Json(new { result=1 });
            }
            return Json(new { result = 0 });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var catDb = db.Categories.SingleOrDefault(x => x.Id == id);

            return View(catDb);
        }

        [HttpPost]
        public ActionResult Edit(Category c)
        {
            var catDb = db.Categories.SingleOrDefault(x => x.Id == c.Id);
            if (catDb != null)
            {
                catDb.Name = c.Name;
                catDb.catDescription = c.catDescription;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(c);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var cat = db.Categories.SingleOrDefault(x => x.Id == id);
            return View(cat);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var cat = db.Categories.SingleOrDefault(x => x.Id == id);
            return View(cat);
        }
        [HttpPost]
        public ActionResult Delete(Category c)
        {
            var cat = db.Categories.SingleOrDefault(x => x.Id == c.Id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}