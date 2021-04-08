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
        public ActionResult Index()
        {
            var profesors= from profesor in db.Profesors
                           orderby profesor.Nume
                           select profesor;
            ViewBag.Profesors= profesors;
            return View();
        }
        public ActionResult Show(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            ViewBag.Profesor= profesor;
            return View();
        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
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

        public ActionResult Edit(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            ViewBag.Profesor= profesor;
            return View();
        }

        [HttpPut]
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
        public ActionResult Delete(int id)
        {
            Profesor profesor = db.Profesors.Find(id);
            db.Profesors.Remove(profesor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
