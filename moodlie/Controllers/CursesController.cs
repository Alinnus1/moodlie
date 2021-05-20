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
        [Authorize(Roles = "Student,Profesor,Admin")]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            if(User.IsInRole("Student"))
            {
                var student = db.Students.Where(s => s.UserId == userId).FirstOrDefault();
                var curses = student.Curses;
                ViewBag.Curses = curses;
            }
            else if (User.IsInRole("Profesor"))
            {
                var profesor = db.Profesors.Where(p => p.UserId == userId).FirstOrDefault();
                var curses = profesor.Curses;
                ViewBag.Curses = curses;
            }
            else if (User.IsInRole("Admin"))
            {
                var curses = db.Curses;
                ViewBag.Curses = curses;

            }

            return View();
         

            
        }
        [Authorize(Roles = "Student,Profesor,Admin")]
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

        [Authorize(Roles = "Admin")]
        public ActionResult NewStudent(int id)
        {
            Curs curs = db.Curses.Find(id);
            var studentsInrolat = curs.Students;
            var students = db.Students.ToList();
            List<Student> studentsNotInrolat = new List<Student>();

            foreach(var st in students)
            {
                bool temp = studentsInrolat.Contains(st);
                if(!temp)
                {
                    studentsNotInrolat.Add(st);
                }
            }
            ViewBag.Students = studentsNotInrolat;
            return View(curs);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult NewStudentAct(int id,int id1)
        {
 
            Curs curs = db.Curses.Find(id);
            Student studentul = db.Students.Find(id1);
   
            studentul.Curses.Add(curs);
            curs.Students.Add(studentul);
            db.SaveChanges();
            return RedirectToAction("NewStudent/" + id.ToString());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            Curs curs = new Curs();
            curs.Profesors = GetAllProfesors();
            return View(curs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult New(Curs curs)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Curses.Add(curs);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    curs.Profesors = GetAllProfesors();
                    return View(curs);
                }
                
            }
            catch (Exception e)
            {
                curs.Profesors = GetAllProfesors();
                return View(curs);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Curs curs = db.Curses.Find(id);
            curs.Profesors = GetAllProfesors();
            return View(curs);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id, Curs requestCurs)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    Curs curs = db.Curses.Find(id);
                    if(TryUpdateModel(curs))
                    {
                        curs = requestCurs;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(requestCurs);
                }
                else
                {
                    requestCurs.Profesors = GetAllProfesors();
                    return View(requestCurs);
                }
                
            }
            catch(Exception e)
            {
                requestCurs.Profesors = GetAllProfesors();
                return View(requestCurs);
            }
        }
        [Authorize(Roles = "Admin")]
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

        [NonAction]
        public IEnumerable<SelectListItem> GetAllProfesors()
        {
            var selectList = new List<SelectListItem>();
            var profesors = from prof in db.Profesors
                             select prof;
            foreach (var prof in profesors)
            {
                selectList.Add(new SelectListItem
                {
                    Value = prof.ProfesorId.ToString(),
                    Text = prof.Prenume.ToString() + " " + prof.Nume.ToString()
                });
            }
            return selectList;
        }
    }
}