using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using TemplateProject.Models;

namespace TemplateProject.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var categories = db.Categories.ToList();
            return View(categories);
        }

        public ActionResult Details(int id)
        {
            var job = db.Jobs.SingleOrDefault(x => x.Id == id);
            Session["jobId"] = id;
            return View(job);
        }

        [HttpGet]
        public ActionResult Apply()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Apply(ApplyJob apply)
        {
            if (ModelState.IsValid)
            {
                // var userName = User.Identity.Name;
                apply.UserId = User.Identity.GetUserId();
                apply.JobId = Convert.ToInt32(Session["jobId"]);
                var check = db.ApplyJobs.Where(x => x.JobId == apply.JobId && x.UserId == apply.UserId);
                if (check.Count() < 1)
                {
                    //Apply Done for first time : 
                    apply.Date = DateTime.Now;
                    db.ApplyJobs.Add(apply);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.message = "You Have Registered Before ! ";
                    return View(apply);
                }
            }
            return View(apply);

        }
       
        public ActionResult GetAllJobsForSpecificUser()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.ApplyJobs.Where(x => x.UserId == userId);
            return View(jobs);
        }
        public ActionResult GetAllJobsForSpecificPublisher()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.Jobs.Where(x => x.UserId == userId);
            return View(jobs);
        }

        [HttpGet]
        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string text)
            {
            if (ModelState.IsValid)
            {
                var te = db.Jobs.Where(x => x.JobTitle.Contains(text) || x.JobContent.Contains(text));
                if (te == null)
                {
                    ViewBag.Message = "Please ENter YOur Search Right Well ..!! ";
                }
                else
                    ViewBag.Message = "Please ENter YOur Search Right Well ..!! ";
                return View(te);
            }
            else
            {
                ViewBag.Message = "Wroooong !!!  ..!! ";
            }
                    return View();

        }
        
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyJobs.SingleOrDefault(x => x.Id == id);
            return View(job);
        }

        [HttpGet]
        public ActionResult EditOfJob(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var ApplyJobs = db.ApplyJobs.Find(id);
            if (ApplyJobs == null)
            {
                return HttpNotFound();
            }
            return View(ApplyJobs);
        }

        // POST: JobsCustom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOfJob(ApplyJob apply)
        {
            if (ModelState.IsValid)
            {
                var jobDb = db.ApplyJobs.Find(apply.Id);
                jobDb.Message = apply.Message;
                jobDb.Date = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(apply);
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ContactUs(Contact contact)
        {
            var mail = new MailMessage();
            var userName = User.Identity.Name;
            mail.From = new MailAddress(contact.Email);
            mail.To.Add (new MailAddress("mymailjusttry@gmail.com"));
            var subject = contact.Subject.Replace('\r', ' ').Replace('\n', ' ');
            mail.Subject = subject;

            mail.IsBodyHtml = true;
            var message = "<strong>User Name :</strong>" + userName + "<br/>" +
                          "<strong>Email : </strong>" + contact.Email + "<br/> " +
                          "<strong>Subject : </strong>" + contact.Subject + "<br/> " +
                          "<strong>Message :</strong>" + contact.Body;
            var newMessage = " <table class=table table-hover table-striped> <tr> <th>User Name</th> <th>Email</th> <th>Subject</th> <th>Message</th> </tr>" +
                "<tr> <td>"+userName+"</td> <td>"+contact.Email+ "</td><td>" + contact.Subject + "</td><td>" + contact.Body + "</td></tr>"
                ;
      
            mail.Body = message;
          //  var loginInfo = new NetworkCredential("mymailjusttry@gmail.com", "mymailjusttry14");

            //mail.Body = contact.Body;
            var loginInfo = new NetworkCredential("shabanahmed491@gmail.com", "anazamalkawy14");

            var smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = loginInfo;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtp.Send(mail);

            return RedirectToAction("Index");
        }

        [HttpPost]
        //public ActionResult ContactUs()
        //{
        //    return View();
        //}






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
    }
}