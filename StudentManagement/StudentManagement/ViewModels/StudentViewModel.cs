using StudentManagement.Helpers;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class StudentViewModel
    {
        Student student;
        public StudentViewModel(Student student)
        {
            this.student = student;
        }

        public int StudentID { get { return this.student.StudentID; } set { value = this.student.StudentID; } }

        [Display(Name="学号")]
        public string Number { get { return this.student.Number; } set { value = this.student.Number; } }

        [Display(Name="姓名")]
        public string Name { get { return this.student.Name; } set { value = this.student.Name; } }

        public string Password { get; set; }

        [Display(Name="考试分数")]
        public virtual ICollection<Exam> Exam { get { return this.student.Exam; } set { value = this.student.Exam; } }

        [Display(Name="练习分数")]
        public virtual ICollection<Exercise> Exercises { get { return this.student.Exercises; } set { value = this.student.Exercises; } }

        [Display(Name="测试分数")]
        public virtual ICollection<Test> Tests { get { return this.student.Tests; }set { value = this.student.Tests; } }

        [Display(Name ="课程名")]
        public virtual ICollection<Course> Courses { get { return this.student.Courses; } set { value = this.student.Courses; } }

        [Display(Name ="总成绩")]
        public virtual ICollection<Grade> Grades { get { return this.student.Grades; }set { value = this.student.Grades; } }

        public float ExerciseSumScore(Course course)
        {
            float sum = 0;
            foreach (var exe_item in student.Exercises.Where(p=>p.CourseId==course.ID&&p.StudentId==student.StudentID))
            {
                sum += exe_item.Score.ToScore();
            }
            return (sum/16).ToScore();
        }

        public float TestSumScore(Course course)
        {
            float sum = 0;
            foreach(var test_item in student.Tests.Where(p=>p.CourseId==course.ID && p.StudentId == student.StudentID))
            {
                sum += test_item.Score.ToScore();
            }
            return (sum/3).ToScore();
        }

        public float ExamScore(Course course)
        {
            float sum = 0;
            foreach(var examScore in student.Exam.Where(p => p.CourseId == course.ID && p.StudentId == student.StudentID))
            {
                sum += examScore.Score.ToScore();
            }
            return (sum).ToScore();
        }

        public float GradeScore(Course course)
        {
            return (float)(ExerciseSumScore(course) * 0.1 + TestSumScore(course) * 0.2 + ExamScore(course) * 0.7).ToScore();
        }
    }
}