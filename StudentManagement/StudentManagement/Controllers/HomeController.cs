using StudentManagement.Models;
using StudentManagement.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        Repository<Student> student = new Repository<Student>(new DataContext.Context());
        Repository<Teacher> teacher = new Repository<Teacher>(new DataContext.Context());
        Repository<Course> course = new Repository<Course>(new DataContext.Context());
        Repository<Exercise> exercise = new Repository<Exercise>(new DataContext.Context());
        Repository<Exam> exam = new Repository<Exam>(new DataContext.Context());
        Repository<Test> test = new Repository<Test>(new DataContext.Context());
        
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}