using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using University.BL.DTOs;
using University.BL.Models;

namespace University.Web.Controllers
{
    public class DashboardController : Controller
    {
        private static readonly UniversityModel university = new UniversityModel();
       
        // GET: Dashboard
        public ActionResult Donut()
        {
            return View();
        }

        public async Task<ActionResult> DonutJson() 
        {
            var data = new List<DonutExampleDTO>();
            data.Add(new DonutExampleDTO { Value = 70, Label = "Java" });
            data.Add(new DonutExampleDTO { Value = 30, Label = "Angular" });

            var dataJson = JsonConvert.SerializeObject(data);

            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Bar()
        {
            return View();
        }

        public ActionResult DonutStudent()
        {
            return View();
        }

        public async Task<ActionResult> DonutStudents()
        {
            var student = from q in university.Course
                          join en in university.Enrollment on q.CourseID equals en.CourseID
                          group q by (q.Title) into query
                          select query;

            var data = new List<DonutExampleDTO>();
            foreach (var item in student)
            {
                data.Add(new DonutExampleDTO { Value = item.Count() , Label = item.Key });
                
            }
            

            var dataJson = JsonConvert.SerializeObject(data);

            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DonutInstructor()
        {
            return View();
        }

        public async Task<ActionResult> DonutInstructors()
        {
            var instructor = from q in university.Instructor
                             join en in university.CourseInstructor on q.ID equals en.InstructorID
                             group q by (q.FirstMidName + ", " + q.LastName) into query
                             select query;

            var data = new List<DonutExampleDTO>();
            foreach (var item in instructor)
            {
                data.Add(new DonutExampleDTO { Value = item.Count(), Label = item.Key });

            }


            var dataJson = JsonConvert.SerializeObject(data);

            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DonutCourse()
        {
            return View();
        }

        public async Task<ActionResult> DonutCourses()
        {
            var course = from q in university.Student
                         join en in university.Enrollment on q.ID equals en.StudentID
                         group q by (q.FirstMidName + ", " + q.LastName) into query
                         select query;
            

            var data = new List<DonutExampleDTO>();
            foreach (var item in course)
            {
                data.Add(new DonutExampleDTO { Value = item.Count(), Label = item.Key });

            }


            var dataJson = JsonConvert.SerializeObject(data);

            return Json(dataJson, JsonRequestBehavior.AllowGet);
        }
    }


}