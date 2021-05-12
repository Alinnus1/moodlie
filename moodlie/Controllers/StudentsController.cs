using moodlie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace moodlie.App_Start
{
    [Authorize(Roles ="Admin")]
    public class StudentsController : Controller
    {
        // GET: Students
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            var students = from student in db.Students
                           orderby student.Nume
                           select student;
            ViewBag.Students = students;
            ViewBag.Curses = db.Curses;
            ViewBag.CursStudents = db.CursStudents;
            return View();
        }
        public ActionResult Show(int id)
        {
            Student student = db.Students.Find(id);
            ViewBag.Student = student;
            return View();
        }

        public ActionResult New()
        {
            return View();
        }
        [HttpPost]
        public ActionResult New(Student student)
        {
            try
            {
                db.Students.Add(student);
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
            Student student = db.Students.Find(id);
            ViewBag.Student = student;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(int id, Student requestStudent)
        {
            try
            {
                Student student = db.Students.Find(id);
                if (TryUpdateModel(student))
                {
                    student.Nume = requestStudent.Nume;
                    student.Prenume = requestStudent.Prenume;
                    student.AnStudiu = requestStudent.AnStudiu;
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
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}