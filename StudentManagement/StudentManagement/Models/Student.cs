using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Student
    {
        [Key]
        public int StudentID { get; set; }

        [Required]
        [Display(Name = "学号")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name ="考试")]
        public virtual ICollection<Exam> Exam { get; set; }

        [Display(Name ="练习")]
        public virtual ICollection<Exercise> Exercises { get; set; }

        [Display(Name ="测试")]
        public virtual ICollection<Test> Tests { get; set; }

        [Display(Name ="课程")]
        public virtual ICollection<Course> Courses { get; set; }

        [Display(Name ="总成绩")]
        public virtual ICollection<Grade> Grades { get; set; }
    }
}