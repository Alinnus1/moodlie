using Microsoft.AspNet.Identity;
using moodlie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace moodlie.Controllers
{
    public class SectionsController : Controller
    {
        private ApplicationDbContext db = new moodlie.Models.ApplicationDbContext();
        // GET: Sections
        [Authorize(Roles = "Profesor,Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Profesor,Admin")]
        public ActionResult New(Section section)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var profesorId = db.Profesors.Where(p => p.UserId == userId).FirstOrDefault().ProfesorId;
                var cursId = section.CursId;
                var curs = db.Curses.Find(cursId);
                if (ModelState.IsValid && (profesorId == curs.ProfesorId || User.IsInRole("Admin"))) //
                {
                    db.Sections.Add(section);
                    db.SaveChanges();
                    return Redirect("/Curses/Show/" + section.CursId);
                }
                else
                {
                    return Redirect("/Curses/Show/" + section.CursId);
                }
            }

            catch (Exception)
            {
                return Redirect("/Curses/Show/" + section.CursId);
            }
        }


        [Authorize(Roles = "Profesor,Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                Section section = db.Sections.Find(id);
                var creatorId = section.Curs.ProfesorTitular.UserId;
                var cursId = section.CursId;
                if (User.Identity.GetUserId() == creatorId || User.IsInRole("Admin"))
                {
                    db.Sections.Remove(section);
                    db.SaveChanges();
                }

                return Redirect("/Curses/Show/" + cursId.ToString());
            }
            catch (Exception)
            {
                return Redirect("/Curses/Index");
            }
        }
    }
}