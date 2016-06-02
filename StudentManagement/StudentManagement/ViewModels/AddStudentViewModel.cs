using StudentManagement.DataContext;
using StudentManagement.Models;
using StudentManagement.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class AddStudentViewModel
    {
        private Course course;
        private Student student;
        public AddStudentViewModel()
        {
        }
        [Display(Name="学号")]
        [Required]
        public string Number { get; set; }

        [Display(Name="姓名")]
        public string Name { get; set; }
    }
}