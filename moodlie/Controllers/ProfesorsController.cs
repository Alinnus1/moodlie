using moodlie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace moodlie.Controllers
{
    public class ProfesorsController : Controller
    {
        // GET: Profesors
        private ApplicationDbContext db = new ApplicationDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var profesors= from profesor in db.Profesors
                           orderby profesor.Nume
                           select profesor;
            ViewBag.Profesors= profesors;
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Show(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            ViewBag.Profesor= profesor;
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult New(Profesor profesor)
        {
            try
            {
                db.Profesors.Add(profesor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            ViewBag.Profesor= profesor;
            return View();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Profesor requestProfesor)
        {
            try
            {
                Profesor profesor = db.Profesors.Find(id);
                if (TryUpdateModel(profesor))
                {
                    profesor.Nume = requestProfesor.Nume;
                    profesor.Prenume = requestProfesor.Prenume;
                    profesor.GradDidactic= requestProfesor.GradDidactic;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            db.Profesors.Remove(profesor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
