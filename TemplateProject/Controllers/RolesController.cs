using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateProject.Models;

namespace TemplateProject.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Roles
        [Authorize(Roles ="Admins")]
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }

        // GET: Roles/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.SingleOrDefault(x => x.Id == id);
            return View(role);
        }

        // GET: Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {

            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.SingleOrDefault(x => x.Id == id);

            return View(role);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(IdentityRole role,string id)
        {
           
            var roleDb = db.Roles.SingleOrDefault(x => x.Id == id);
            if(roleDb != null)
            {
                roleDb.Id = role.Id;
                roleDb.Name = role.Name;
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Roles/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.SingleOrDefault(x => x.Id == id);
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {
            var rolew = db.Roles.SingleOrDefault(x => x.Id == role.Id);
                db.Roles.Remove(rolew);
                db.SaveChanges();
                return RedirectToAction("Index");
        }
    }
}
