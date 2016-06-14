using PagedList;
using StudentManagement.DataContext;
using StudentManagement.Helpers;
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
    public class TeacherController : Controller
    {
        Repository<Student> student = new Repository<Student>(new Context());
        Repository<Teacher> teacher = new Repository<Teacher>(new Context());
        Repository<Course> course = new Repository<Course>(new Context());
        Repository<Exercise> exercise = new Repository<Exercise>(new Context());
        Repository<Exam> exam = new Repository<Exam>(new Context());
        Repository<Test> test = new Repository<Test>(new Context());
        Repository<Grade> grade = new Repository<Grade>(new Context());
        
        /// <summary>
        /// 浏览所教全部学生的信息
        /// </summary>
        /// <param name="id">TeacherId</param>
        /// <returns></returns>
        public ActionResult Index(int id, int? pageIndexNullable)
        {
            //所教课程
            Course _course = teacher.FindById(id).Course;
            //选择此课程的所有学生
            var _students = course.Find(c => c.ID == _course.ID).Students;


            //附加信息
            if(_students.Count()==0)
            {
                ViewBag.Count = "没有学生";
            }
            else
            {
                ViewBag.Count = _students.Count();
            }
            if (Request.QueryString.GetValues("pageIndex") != null)
            {
                pageIndexNullable = Int32.Parse(Request.QueryString.GetValues("pageIndex")[0]);
            }

            ViewBag.TeacherName = teacher.FindById(id).Name;
            ViewBag.TeacherId = id;
            ViewBag.Course = _course;
            ViewBag.PageIndex = pageIndexNullable;
            return View();
        }

        public PartialViewResult GetIndex(int id, int? pageIndexNullable)
        {
            if (Request.QueryString.GetValues("pageIndex") != null)
            {
                pageIndexNullable = Int32.Parse(Request.QueryString.GetValues("pageIndex")[0]);
            }

            //所教课程
            Course _course = teacher.FindById(id).Course;
            //选择此课程的所有学生
            var _students = course.Find(c => c.ID == _course.ID).Students;


            if (_students.Count() != 0)
            {
                IList<StudentViewModel> stuVMs = new List<StudentViewModel>();
                foreach (var stu_item in _students)
                {
                    StudentViewModel stuVM = new StudentViewModel(stu_item)
                    {
                        StudentID = stu_item.StudentID,
                        Number = stu_item.Number,
                        Name = stu_item.Name,
                        Password = stu_item.Password,
                        Courses = stu_item.Courses,


                        Exam = stu_item.Exam,
                        Exercises = stu_item.Exercises,
                        Tests = stu_item.Tests
                    };
                    stuVMs.Add(stuVM);
                }

                int pageSize = 5;
                int pageIndex = pageIndexNullable ?? 1;

                //排序
                List<StudentViewModel> resultList = stuVMs.OrderBy(p=>p.Number).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList<StudentViewModel>();
                //List<StudentViewModel> resultList = stuVMs.OrderByDescending(p=>p.GradeScore(_course)).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList<StudentViewModel>();




                //分页视图模型
                StudentPagingInfoViewModel model = new StudentPagingInfoViewModel
                {
                    StudentViewModel = resultList,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = pageIndex,
                        ItemsPerPage = pageSize,
                        TotalItems = _students.Count()
                    }
                };

                ViewBag.Course = _course;
                return PartialView("_PartialGetIndex", model);
            }
            else
            {
                ViewBag.Course = _course;
                return PartialView("_PartialGetIndex");
            }
        }

        public ActionResult Print(int id)
        {
            //所教课程
            Course _course = teacher.FindById(id).Course;
            //选择此课程的所有学生
            var _students = course.Find(c => c.ID == _course.ID).Students;


            if (_students.Count() != 0)
            {
                IList<StudentViewModel> stuVMs = new List<StudentViewModel>();
                foreach (var stu_item in _students)
                {
                    StudentViewModel stuVM = new StudentViewModel(stu_item)
                    {
                        StudentID = stu_item.StudentID,
                        Number = stu_item.Number,
                        Name = stu_item.Name,
                        Password = stu_item.Password,
                        Courses = stu_item.Courses,


                        Exam = stu_item.Exam,
                        Exercises = stu_item.Exercises,
                        Tests = stu_item.Tests
                    };
                    stuVMs.Add(stuVM);
                }


                //排序
                List<StudentViewModel> resultList = stuVMs.OrderBy(p => p.Number).ToList();
                //List<StudentViewModel> resultList = stuVMs.OrderByDescending(p=>p.GradeScore(_course)).Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList<StudentViewModel>();

                ViewBag.Course = _course;
                return View(resultList);
            }
            else
            {
                ViewBag.Course = _course;
                return View();
            }
        }

        /// <summary>
        /// 添加学生信息
        /// </summary>
        /// <param name="id">TeacherId</param>
        /// <returns></returns>
        public ActionResult AddStudent(int id)
        {
            ViewBag.TeacherId = id;
            ViewBag.TeacherName = teacher.FindById(id).Name;
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(AddStudentViewModel addStuVM,int id)
        {
            //教师Id
            int _teacherId = id;

            /****************************/
            /*注意，这里的_student和_course必须来自同一个Context
             * 来自同一个对象。SavaChanges也只能同时操作一个对象。
             * 
             * 具体来说就是都从course这个上下文对象来获取，当然其他对象也可以获取出来
             * 
             * 这里与AccountController/RegisterStudent中的学生注册方式略不同
             * 学生注册中的_stu是一个全新的对象，所以不局限于任何context上下文对象。可任意存取
             */
            

            if (ModelState.IsValid)
            {
                //操作的学生
                Student _student0 = student.Find(p => p.Number == addStuVM.Number);
                Student _student = course.FindAll().FirstOrDefault().Students.FirstOrDefault(s => s.Number == _student0.Number);

                if (_student != null && !student.Exist(p => p.Number == addStuVM.Number) && _student.Name != addStuVM.Name)
                {
                    ModelState.AddModelError("", "添加的学生不存在");
                    return View();
                }


                //操作的课程
                Course _course = course.Find(p => p.Teacher.ID == _teacherId);

                //在课程中添加学生
                _course.Students.Add(_student0);

                //再次查找操作的课程实体，保存更改
                var c = course.Find(p => p.Teacher.ID == _teacherId);
                course.Update(c);


                return RedirectToAction("Index", new { id = _teacherId });
            }
            return View();
        }
        
        /// <summary>
        /// 删除学生信息
        /// </summary>
        /// <param name="id">CourseId</param>
        /// <returns></returns>
        public ActionResult DeleteStudent(object id)
        {
            var r = Request.QueryString.GetValues("StudentID")[0];
            //studentId
            int studentId = Int32.Parse(r);
            //courseId
            int courseId = Int32.Parse(id.ToString());
            Student _student = student.FindById(studentId);
            Course _course = student.FindById(studentId).Courses.FirstOrDefault(c => c.ID == courseId);

            _course.Students.Remove(_student);

            student.Update(student.FindById(studentId));

            return RedirectToAction("Index",new { id=_course.Teacher.ID});
        }
        
        /// <summary>
        /// 修改学生信息
        /// </summary>
        /// <param name="id">CourseId</param>
        /// <returns></returns>
        public ActionResult UpdateStudent(int id)
        {
            var r = Request.QueryString.GetValues("StudentID")[0];
            //studentId
            int studentId = Int32.Parse(r);
            //courseId
            int courseId = Int32.Parse(id.ToString());

            //操作的学生和课程
            Student _student = student.FindById(studentId);
            Course _course = student.FindById(studentId).Courses.FirstOrDefault(c => c.ID == courseId);

            UpdateStudentViewModel updateStudentVM = new UpdateStudentViewModel();
            updateStudentVM.CourseId = _course.ID;
            updateStudentVM.StudentId = _student.StudentID;
            updateStudentVM.Name = _student.Name;
            updateStudentVM.Number = _student.Number;
            #region 初始化
            if (_student.Exercises.Count==0)
            {
                updateStudentVM.ExeScore1 = 0;
                updateStudentVM.ExeScore2 = 0;
                updateStudentVM.ExeScore3 = 0;
                updateStudentVM.ExeScore4 = 0;
                updateStudentVM.ExeScore5 = 0;
                updateStudentVM.ExeScore6 = 0;
                updateStudentVM.ExeScore7 = 0;
                updateStudentVM.ExeScore8 = 0;
                updateStudentVM.ExeScore9 = 0;
                updateStudentVM.ExeScore10 =0;
                updateStudentVM.ExeScore11 =0;
                updateStudentVM.ExeScore12 =0;
                updateStudentVM.ExeScore13 =0;
                updateStudentVM.ExeScore14 =0;
                updateStudentVM.ExeScore15 =0;
                updateStudentVM.ExeScore16 = 0;
            }
            if(_student.Tests.Count()==0)
            {
                updateStudentVM.TestScore1 =0;
                updateStudentVM.TestScore2 =0;
                updateStudentVM.TestScore3 =0;
            }
            if (_student.Exam.Count() == 0)
            {
                updateStudentVM.ExamScore = 0;
            }
            if(_student.Grades.Count()==0)
            {
                updateStudentVM.GradeScore = 0;
            }
            else
            {
                updateStudentVM.ExeScore1 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 1).Score.ToScore();
                updateStudentVM.ExeScore2 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 2).Score.ToScore();
                updateStudentVM.ExeScore3 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 3).Score.ToScore();
                updateStudentVM.ExeScore4 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 4).Score.ToScore();
                updateStudentVM.ExeScore5 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 5).Score.ToScore();
                updateStudentVM.ExeScore6 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 6).Score.ToScore();
                updateStudentVM.ExeScore7 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 7).Score.ToScore();
                updateStudentVM.ExeScore8 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 8).Score.ToScore();
                updateStudentVM.ExeScore9 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 9).Score.ToScore();
                updateStudentVM.ExeScore10 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 10).Score.ToScore();
                updateStudentVM.ExeScore11 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 11).Score.ToScore();
                updateStudentVM.ExeScore12 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 12).Score.ToScore();
                updateStudentVM.ExeScore13 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 13).Score.ToScore();
                updateStudentVM.ExeScore14 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 14).Score.ToScore();
                updateStudentVM.ExeScore15 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 15).Score.ToScore();
                updateStudentVM.ExeScore16 = _student.Exercises.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID && e.Num == 16).Score.ToScore();
                updateStudentVM.TestScore1 = _student.Tests.FirstOrDefault(t => t.StudentId == _student.StudentID && t.CourseId == _course.ID && t.Num == 1).Score.ToScore();
                updateStudentVM.TestScore2 = _student.Tests.FirstOrDefault(t => t.StudentId == _student.StudentID && t.CourseId == _course.ID && t.Num == 2).Score.ToScore();
                updateStudentVM.TestScore3 = _student.Tests.FirstOrDefault(t => t.StudentId == _student.StudentID && t.CourseId == _course.ID && t.Num == 3).Score.ToScore();
                updateStudentVM.ExamScore = _student.Exam.FirstOrDefault(e => e.StudentId == _student.StudentID && e.CourseId == _course.ID).Score;
                var exeSumScore = (updateStudentVM.ExeScore11 + updateStudentVM.ExeScore12 + updateStudentVM.ExeScore13 + updateStudentVM.ExeScore14 + updateStudentVM.ExeScore15 + updateStudentVM.ExeScore16 +
                    updateStudentVM.ExeScore1 + updateStudentVM.ExeScore2 + updateStudentVM.ExeScore3 + updateStudentVM.ExeScore4 + updateStudentVM.ExeScore5 + updateStudentVM.ExeScore6 + updateStudentVM.ExeScore7 +
                    updateStudentVM.ExeScore8 + updateStudentVM.ExeScore9 + updateStudentVM.ExeScore10) / 16;
                var testSumScore = (updateStudentVM.TestScore1 + updateStudentVM.TestScore2 + updateStudentVM.TestScore3) / 3;
                var examSumScore = updateStudentVM.ExamScore;
                updateStudentVM.GradeScore = (float)(exeSumScore.ToScore() * 0.1 + testSumScore.ToScore() * 0.2 + exeSumScore.ToScore() * 0.7);
            }
            #endregion
            ViewBag.Student = _student;
            ViewBag.Course = _course;
            ViewBag.GradeScore = updateStudentVM.GradeScore;
            ViewBag.TeacherId = _course.Teacher.ID;
            ViewBag.TeacherName = _course.Teacher.Name;
            return View(updateStudentVM);
        }
        [HttpPost]
        public ActionResult UpdateStudent(UpdateStudentViewModel updateStuVM)
        {
            if (student.Find(s => s.Number == updateStuVM.Number).StudentID != updateStuVM.StudentId)
            {
                ModelState.AddModelError("Number", "此学号已存在");
            }
            if (ModelState.IsValid)
            {
                ICollection<Exercise> _execrises = new List<Exercise>()
                {
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=1, Score=updateStuVM.ExeScore1.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=2, Score=updateStuVM.ExeScore2.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=3, Score=updateStuVM.ExeScore3.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=4, Score=updateStuVM.ExeScore4.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=5, Score=updateStuVM.ExeScore5.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=6, Score=updateStuVM.ExeScore6.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=7, Score=updateStuVM.ExeScore7.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=8, Score=updateStuVM.ExeScore8.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=9, Score=updateStuVM.ExeScore9.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=10, Score=updateStuVM.ExeScore10.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=11, Score=updateStuVM.ExeScore11.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=12, Score=updateStuVM.ExeScore12.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=13, Score=updateStuVM.ExeScore13.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=14, Score=updateStuVM.ExeScore14.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=15, Score=updateStuVM.ExeScore15.ToScore() },
                    new Exercise { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=16, Score=updateStuVM.ExeScore16.ToScore() }
                };
                student.FindById(updateStuVM.StudentId).Exercises.Clear();
                foreach (var exe_item in _execrises)
                {
                    student.FindById(updateStuVM.StudentId).Exercises.Add(exe_item);
                }

                ICollection<Test> _tests = new List<Test>()
                {
                    new Test { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=1, Score=updateStuVM.TestScore1.ToScore() },
                    new Test { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=2, Score=updateStuVM.TestScore2.ToScore() },
                    new Test { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Num=3, Score=updateStuVM.TestScore3.ToScore() }
                };
                student.FindById(updateStuVM.StudentId).Tests.Clear();
                foreach (var test_item in _tests)
                {
                    student.FindById(updateStuVM.StudentId).Tests.Add(test_item);
                }
                ICollection<Exam> _exams = new List<Exam>()
                {
                    new Exam { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Score=updateStuVM.ExamScore.ToScore() }
                };
                student.FindById(updateStuVM.StudentId).Exam.Clear();
                foreach (var exam_item in _exams)
                {
                    student.FindById(updateStuVM.StudentId).Exam.Add(exam_item);
                }
                ICollection<Grade> _grades = new List<Grade>()
                {
                    new Grade { CourseId=updateStuVM.CourseId, StudentId=updateStuVM.StudentId, Score=updateStuVM.GradeScore.ToScore() }
                };
                student.FindById(updateStuVM.StudentId).Grades.Clear();
                foreach (var grade_item in _grades)
                {
                    student.FindById(updateStuVM.StudentId).Grades.Add(grade_item);
                }
                student.FindById(updateStuVM.StudentId).Name = updateStuVM.Name;
                student.FindById(updateStuVM.StudentId).Number = updateStuVM.Number;

                student.Update();


                int teacherId = course.FindById(updateStuVM.CourseId).Teacher.ID;
                return RedirectToAction("Index", new { id = teacherId });
            }
            else
            {
                ViewBag.Student = student.FindById(updateStuVM.StudentId);
                ViewBag.Course = course.FindById(updateStuVM.CourseId);
                ViewBag.GradeScore = updateStuVM.GradeScore;
                ViewBag.TeacherId = course.FindById(updateStuVM.CourseId).Teacher.ID;
                ViewBag.TeacherName = course.FindById(updateStuVM.CourseId).Teacher.Name;

                return View(updateStuVM);
            }
        }
    }
}