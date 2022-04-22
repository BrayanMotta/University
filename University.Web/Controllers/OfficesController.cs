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
using University.BL.Controls;
using Newtonsoft.Json;

namespace University.Web.Controllers
{
    public class OfficesController : Controller
    {
        private readonly IMapper mapper = MvcApplication.MapperConfiguration.CreateMapper();
        private readonly IOfficeAssignmentRepository officeRepository = new OfficeAssignmentRepository(new UniversityModel());
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> IndexJson()
        {
            var officesModel = await officeRepository.GetAll();
            var officesDTO = officesModel.Select(x => mapper.Map<OfficeAssignmentDTO>(x));
            return Json(officesDTO, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetOffices()
        {
            var officesModel = await officeRepository.GetAll();
            var officesDTO = officesModel.Select(x => mapper.Map<OfficeAssignmentDTO>(x));
            var officesSelect = officesDTO.Select(x => new SelectControl
            {
                Id = x.InstructorID,
                Text = x.Location
            });

            return Json(JsonConvert.SerializeObject(officesSelect), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return PartialView(new OfficeAssignmentDTO());
        }

        [HttpPost]
        public async Task<ActionResult> Create(OfficeAssignmentDTO officeDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var officeModel = mapper.Map<OfficeAssignment>(officeDTO);
                    await officeRepository.Insert(officeModel);
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
            var officeModel = await officeRepository.GetById(id);
            var officeDTO = mapper.Map<OfficeAssignmentDTO>(officeModel);
            return PartialView(officeDTO);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(OfficeAssignmentDTO officeDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var officeModel = mapper.Map<OfficeAssignment>(officeDTO);
                    await officeRepository.Update(officeModel);
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
                await officeRepository.Delete(id);

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