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
    public class AccountController : Controller
    {
        Repository<Student> student = new Repository<Student>(new DataContext.Context());
        Repository<Teacher> teacher = new Repository<Teacher>(new DataContext.Context());
        Repository<Course> course = new Repository<Course>(new DataContext.Context());
        Repository<Exercise> exercise = new Repository<Exercise>(new DataContext.Context());
        Repository<Exam> exam = new Repository<Exam>(new DataContext.Context());
        Repository<Test> test = new Repository<Test>(new DataContext.Context());

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if(ModelState.IsValid)
            {
                Student _student = student.Find(p => p.Number == login.Number);
                if (_student!=null)
                {
                    if(_student.Password==login.Password)
                    {
                        return RedirectToAction("Index", "Student",new {id=_student.StudentID});
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "密码错误");
                    }
                }
                else
                {
                    Teacher _teacher = teacher.Find(p => p.Number == login.Number);
                    if(_teacher!=null)
                    {
                        if(_teacher.Password==login.Password)
                        {
                            return RedirectToAction("Index", "Teacher",new { id=_teacher.ID});
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "密码错误");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "用户不存在");
                    }
                }
            }
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel register)
        {
            if (register.IsTeacher)
            {
                return RedirectToAction("RegisterTeacher");
            }
            else
            {
                return RedirectToAction("RegisterStudent");
            }
        }

        /// <summary>
        /// 教师注册
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterTeacher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterTeacher(RegisterTeacherViewModel regTeaVM)
        {
            if (teacher.Exist(p => p.Name == regTeaVM.Number))
            {
                ModelState.AddModelError("Number", "此账户已存在");
                return View();
            }

            
            Teacher _ter = new Teacher
            {
                Name = regTeaVM.Name,
                Number = regTeaVM.Number,
                Password = regTeaVM.Password,
                Course = regTeaVM.Course
            };
            regTeaVM.Course.Teacher = _ter;
            
            teacher.Add(_ter);

            return RedirectToAction("Index", "Teacher", new { id=teacher.Find(p=>p.Number==_ter.Number).ID});
        }

        /// <summary>
        /// 学生注册
        /// </summary>
        /// <returns></returns>
        public ActionResult RegisterStudent()
        {
            List<SelectListItem> selectItems = new List<SelectListItem>();
                       
            foreach(var cour_item in course.FindAll())
            {
                selectItems.Add(new SelectListItem { Value = cour_item.Name, Text = cour_item.Name, Selected = cour_item.RequiredCouse, Disabled = cour_item.RequiredCouse });
            }
            
            
            RegisterStudentViewModel regStuVM = new RegisterStudentViewModel();
            
            regStuVM.Courses = selectItems;
            
            return View(regStuVM);
        }
        [HttpPost]
        public ActionResult RegisterStudent(RegisterStudentViewModel regStuVM,string[] Courses)
        {
            if (student.Exist(p => p.Name == regStuVM.Number))
            {
                ModelState.AddModelError("Number", "此账户已存在");
                return View();
            }
            if(Courses==null||Courses.Count()==0)
            {
                ModelState.AddModelError("", "至少选择一门科目");
                return View();
            }
            //从视图收集数据
            ICollection<Course> coursesFromView = new List<Course>();
            foreach(var course_item in Courses)
            {
                coursesFromView.Add(course.Find(p=>p.Name==course_item));
            }
            //填充到视图模型中
            regStuVM.CourseCourse = coursesFromView;

            Student _stu = new Student
            {
                Name = regStuVM.Name,
                Number = regStuVM.Number,
                Password = regStuVM.Password,
                Courses=regStuVM.CourseCourse
            };

            //在course中添加stu
            foreach (var cour_item in regStuVM.CourseCourse)
            {
                cour_item.Students.Add(_stu);
            }
            //更新course
            foreach (var course_item in Courses)
            {
                var c = course.Find(p => p.Name == course_item);
                course.Update(c);
            }

            return RedirectToAction("Index", "Student", new { id = student.Find(p => p.Name == _stu.Name).StudentID });
        }
    }
}