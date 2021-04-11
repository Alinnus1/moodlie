using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using moodlie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace moodlie.Controllers
{
    public class RegistrationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Registration
        public ActionResult NewS()
        {
            return View();
        }
        public ActionResult NewP()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewS (Student student)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var v = User.Identity.GetUserId();
            student.UserId = v;
           
            try
            {
                db.Students.Add(student);
                db.SaveChanges();
                UserManager.RemoveFromRole(v, "Waiting");
                UserManager.AddToRole(v, "Student");
                return Redirect("/Home/");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult NewP (Profesor profesor)
        
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var v = User.Identity.GetUserId();
            profesor.UserId = v;
            try
            {
                db.Profesors.Add(profesor);
                db.SaveChanges();
                UserManager.RemoveFromRole(v, "Waiting");
                UserManager.AddToRole(v, "Profesor");
                return Redirect("/Home/");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        
    }
}