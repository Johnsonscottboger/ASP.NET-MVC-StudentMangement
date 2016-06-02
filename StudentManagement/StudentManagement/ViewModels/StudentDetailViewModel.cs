using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class StudentDetailViewModel
    {
        private int stuId = 0, courseId = 0;
        private Student student;
        public StudentDetailViewModel(Student student, int stuId,int courseId)
        {
            this.student = student;
            this.courseId = courseId;
            this.stuId = stuId;
        }
        [Display(Name = "学号")]
        public string Number { get { return this.student.Number; } set { value = this.student.Number; } }

        [Display(Name = "姓名")]
        public string Name { get { return this.student.Name; } set { value = this.student.Name; } }

        [Display(Name = "课程名")]
        public string CourseName { get { return this.student.Courses.FirstOrDefault(c => c.ID == courseId).Name; } set { value = this.student.Courses.FirstOrDefault(c => c.ID == courseId).Name; } }

        public IEnumerable<Exercise> ExerciseEveryScore()
        {
            IEnumerable<Exercise> _exes = student.Exercises.Where(p => p.Student.FirstOrDefault(s=>s.StudentID==stuId).StudentID == stuId && p.CourseId == courseId);
            return _exes;
        }
        public IEnumerable<Test> TestEveryScore()
        {
            IEnumerable<Test> _test = student.Tests.Where(p => p.Student.FirstOrDefault(s => s.StudentID == stuId).StudentID == stuId && p.CourseId == courseId);
            return _test;
        }
    }
}