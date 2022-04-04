using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using University.BL.Models;
using University.BL.Repositories;
using University.BL.Repositories.Implements;
using AutoMapper;
using University.BL.DTOs;

namespace University.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IMapper mapper = MvcApplication.MapperConfiguration.CreateMapper();
        private readonly IStudentRepository studentRepository = new StudentRepository(new UniversityModel());
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> IndexJson()
        {
            var studentsModel = await studentRepository.GetAll();
            var studentsDTO = studentsModel.Select(x => mapper.Map<StudentDTO>(x));
            return Json(studentsDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView(new StudentDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(StudentDTO studentDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var studentModel = mapper.Map<Student>(studentDTO);
                    await studentRepository.Insert(studentModel);
                }
                return Json(new ResponseDTO
                {
                    Message = "The process is successful",
                    IsSuccess = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    Message = ex.Message,
                    IsSuccess = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var studentModel = await studentRepository.GetById(id);
            var studentDTO = mapper.Map<StudentDTO>(studentModel);
            return PartialView(studentDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StudentDTO studentDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var studentModel = mapper.Map<Student>(studentDTO);
                    await studentRepository.Update(studentModel);
                }
                return Json(new ResponseDTO
                {
                    Message = "The process is successful",
                    IsSuccess = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    Message = ex.Message,
                    IsSuccess = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await studentRepository.Delete(id);

                return Json(new ResponseDTO
                {
                    Message = "The process is successful",
                    IsSuccess = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ResponseDTO
                {
                    Message = ex.Message,
                    IsSuccess = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}