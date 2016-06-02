using StudentManagement.Models;
using StudentManagement.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.ViewModels
{
    public class RegisterStudentViewModel
    {
        [Required]
        [Display(Name ="姓名")]
        [Range(2, 10, ErrorMessage = "长度最短为2，最长为10")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="学号")]
        public string Number { get; set; }

        [Required]
        [Display(Name ="密码")]
        public string Password { get; set; }

        [Required]
        [Display(Name ="确认密码")]
        public string ConfirmPassword { get; set; }

        [Display(Name ="课程")]
        public ICollection<Course> CourseCourse { get; set; }

        [Display(Name ="选择课程")]
        public IEnumerable<SelectListItem> Courses { get; set; }
    }
}