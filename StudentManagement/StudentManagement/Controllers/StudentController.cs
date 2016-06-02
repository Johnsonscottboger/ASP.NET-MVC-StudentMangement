using StudentManagement.Models;
using StudentManagement.Repository;
using StudentManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class StudentController : Controller
    {
        Repository<Student> student = new Repository<Student>(new DataContext.Context());
        Repository<Teacher> teacher = new Repository<Teacher>(new DataContext.Context());
        Repository<Course> course = new Repository<Course>(new DataContext.Context());
        Repository<Exercise> exercise = new Repository<Exercise>(new DataContext.Context());
        Repository<Exam> exam = new Repository<Exam>(new DataContext.Context());
        Repository<Test> test = new Repository<Test>(new DataContext.Context());

        // GET: Student
        public ActionResult Index(int id)
        {
            var _stu = student.FindById(id);
            ViewBag.StudentId = _stu.StudentID;
            ViewBag.StudentName = _stu.Name;
            StudentViewModel stuVM = new StudentViewModel(_stu);
            return View(stuVM);
        }

        /// <summary>
        /// 查看个人详细分数信息
        /// </summary>
        /// <param name="id">courseId</param>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            var r = Request.QueryString.GetValues("StudentID")[0];
            //studentId
            int secondid = Int32.Parse(r);

            Student _stu = student.FindById(secondid);
            ViewBag.StudentId = _stu.StudentID;
            ViewBag.StudentName = _stu.Name;
            StudentDetailViewModel stuDetailVM = new StudentDetailViewModel(_stu, secondid, id);
            return View(stuDetailVM);
        }
    }
}