using Microsoft.AspNet.Identity;
using moodlie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace moodlie.Controllers
{
    public class CursesController : Controller
    {
        // GET: Curses
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            var curses = db.Curses;
            ViewBag.Curses= curses;

            return View();
         

            
        }
        public ActionResult Show(int id)
        {
            try
            {
                Curs curs = db.Curses.Find(id);
                if (curs == null)
                {
                    return View();
                }
                var students = from st in db.Students
                               join crst in db.CursStudents
                                    on st.StudentId equals crst.StudentId
                               where crst.CursId == id
                               select st;
                ViewBag.Students = students;
                return View(curs);
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult NewStudent(int id)
        {
            ViewBag.IdCurs = id;
            var selectList = new List<SelectListItem>();
            var studentsbetter = from st in db.Students
                                 from crst in db.CursStudents
                                 where st.StudentId != crst.StudentId
                                 select st;
            var students = db.Students;
            foreach(var st in students)
            {
                selectList.Add(new SelectListItem
                {
                    Value = st.StudentId.ToString(),
                    Text = st.Nume.ToString() + st.Prenume.ToString()
                });
            }
            ViewBag.Students = selectList;
            return View();
        }
        [HttpPut]
        public ActionResult NewStudent(int id,string student)
        {
            if (student == null)
            {
                return View();
            }
            var idStudent = Int16.Parse(student);
            Curs curs = db.Curses.Find(id);
            if(curs == null)
            {
                return View();
            }

            CursStudent cursStudent = new CursStudent();
            cursStudent.CursId = id;
            cursStudent.StudentId = idStudent;
            db.CursStudents.Add(cursStudent);
            db.SaveChanges();
            return RedirectToAction("Show/" + id);
        }
        public ActionResult New()
        {
            var profesors = from pf in db.Profesors
                            select pf.Nume;
            ViewBag.Profesors = profesors;
            return View();
        }
        [HttpPost]
        public ActionResult New(Curs curs)
        {
            try
            {
                db.Curses.Add(curs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            try
            {
                Curs curs = db.Curses.Find(id);
                db.Curses.Remove(curs);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}