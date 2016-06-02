using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Course
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name="名称")]
        public string Name { get; set; }

        [Display(Name ="必修课")]
        public bool RequiredCouse { get; set; }

        [Required]
        [Display(Name ="教师")]
        public virtual Teacher Teacher { get; set; }

        //[Display(Name ="练习")]
        //public virtual ICollection<Exercise> Exercise { get; set; }

        //[Display(Name ="测试")]
        //public virtual ICollection<Test> Test { get; set; }

        //[Display(Name = "考试")]
        //public virtual ICollection<Exam> Exam { get; set; }

        //[Display(Name ="总成绩")]
        //public virtual ICollection<Grade> Grades { get; set; }

        [Display(Name ="学生")]
        public virtual ICollection<Student> Students { get; set; }
    }
}