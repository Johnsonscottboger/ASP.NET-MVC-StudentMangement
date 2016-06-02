using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class RegisterTeacherViewModel
    {
        [Required]
        [Display(Name="姓名")]
        [Range(2, 10, ErrorMessage = "长度最短为2，最长为10")]
        public string Name { get; set; }

        [Required]
        [Display(Name="账号")]
        [Range(2, 10, ErrorMessage = "长度最短为2，最长为10")]
        public string Number { get; set; }

        [Required]
        [Display(Name="密码")]
        [Range(2, 10, ErrorMessage = "长度最短为2，最长为10")]
        public string Password { get; set; }

        [Required]
        [Display(Name="确认密码")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name="所授课程名称")]
        public Course Course { get; set; }
    }
}