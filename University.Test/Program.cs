using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.BL.Models;
using University.BL.Repositories;
using University.BL.Repositories.Implements;
using University.BL.DTOs;

namespace University.Test
{
    class Program
    {
        private static readonly UniversityModel university = new UniversityModel();
        //private static readonly IEnrollmentRepository enrollmentRepository = new EnrollmentRepository(new UniversityModel());
        static void Main(string[] args)
        {
            //var courses = university.Enrollment.ToList();
            //var courses2 = enrollmentRepository.GetAll().Result;
            //foreach (var item in courses2) 
            //{
            //  Console.WriteLine($"{item.EnrollmentID} {item.CourseID}");
            //}
            //Console.WriteLine("\n");

            var student = from q in university.Enrollment
                          join en in university.Course on q.CourseID equals en.CourseID

                          group en by (en.Title) into query
                          orderby query.Count() descending
                          select query;

            var instructor = from q in university.Instructor
                          join en in university.CourseInstructor on q.ID equals en.InstructorID
                          group q by (q.FirstMidName +", "+ q.LastName) into query
                          select query;

            foreach (var item in instructor)
            {
              Console.WriteLine($"{item.Key} - {item.Count()}");
            }

            Console.WriteLine("\n");

            var suma = university.CourseInstructor.Select(x => x.CourseID).Count();

            Console.WriteLine(suma);

            Console.WriteLine("\n");

            //var books = Book.Books();
            //foreach (var item in books)
            //{
            //    Console.WriteLine($"{item.Title} - {item.PublicationDate}");
            //}
            //Console.WriteLine("\n");

            //var authors = Author.Authors();
            //foreach (var item in authors)
            //{
            //    Console.WriteLine($"{item.Name}");
            //}


            var books = Book.Books();
            var authors = Author.Authors();

            var ex1 = books.OrderByDescending(x => x.Sales).Take(3).ToList();
            var ex2 = books.OrderBy(x => x.Sales).Take(3).ToList();

           var ex3 = from b in books
                     join a in authors on b.AuthorId equals a.AuthorId
                     group a by (a.AuthorId, a.Name) into query
                     orderby query.Count() descending
                     select query;

            var ex4 = (from b in books
                       join a in authors
                       on b.AuthorId
                       equals a.AuthorId
                       group b by a.Name into c
                       select new
                       {
                           Author = c.Key,
                           Count = c.Count(),
                       }).ToList();

            var ex5 = books.Where(x => x.PublicationDate >= DateTime.Now.AddYears(-50).Year).ToList();
            var ex6 = books.OrderBy(x => x.PublicationDate).FirstOrDefault();
            //var ex7 = books.Where(x => x.Title.Contains("El")).ToList();
            var ex7 = (from b in books
                       join a in authors
                       on b.AuthorId
                       equals a.AuthorId
                       where b.Title.Contains("El")
                       select new
                       {
                           a.Name,
                           b.Title
                       }).ToList();

            
            
            foreach (var item in ex1)
            {
                Console.WriteLine($"{item.Title} - {item.Sales}");
            }

            Console.WriteLine("\n");

            foreach (var item in ex2)
            {
                Console.WriteLine($"{item.Title} - {item.Sales}");
            }

            Console.WriteLine("\n");

            var resultEx3 = ex3.FirstOrDefault();
            Console.WriteLine($"{resultEx3.Key.AuthorId} - {resultEx3.Key.Name} - {resultEx3.Count()}");
            Console.WriteLine();
            foreach(var item in ex3)
            {
                Console.WriteLine($"{item.Key.Name} - {item.Count()}");
            }

            Console.WriteLine("\n");

            foreach (var item in ex4)
            {
                Console.WriteLine($"{item.Author} - {item.Count}");
            }

            Console.WriteLine("\n");

            foreach (var item in ex5)
            {
                Console.WriteLine($"{item.Title} - {item.PublicationDate}");
            }

            Console.WriteLine("\n");

            
            Console.WriteLine($"{ex6.Title} - {ex6.PublicationDate}");
           

            Console.WriteLine("\n");

            foreach (var item in ex7)
            {
                Console.WriteLine($"{item.Title} - {item.Name}");
            }

            Console.WriteLine("\n");


            Console.ReadKey();
        }
    }
}
