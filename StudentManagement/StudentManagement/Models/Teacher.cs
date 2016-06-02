using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.Models
{
    public class Teacher
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name ="账号")]
        public string Number { get; set; }

        [Required]
        [Display(Name ="姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="密码")]
        public string Password { get; set; }

        [Display(Name ="课程")]
        public virtual Course Course { get; set; }
    }
}