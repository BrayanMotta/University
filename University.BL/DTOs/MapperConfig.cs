using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using University.BL.Models;

namespace University.BL.DTOs
{
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(x =>
            {
                x.CreateMap<Course, CourseDTO>().ReverseMap();
                x.CreateMap<Student, StudentDTO>().ReverseMap();
                x.CreateMap<Instructor, InstructorDTO>().ReverseMap();
                x.CreateMap<Department, DepartmentDTO>().ReverseMap();
                x.CreateMap<OfficeAssignment, OfficeAssignmentDTO>().ReverseMap();
            });
        }
    }
}
