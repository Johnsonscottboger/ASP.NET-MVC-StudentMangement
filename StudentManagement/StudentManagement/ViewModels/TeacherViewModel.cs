using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class TeacherViewModel:Teacher
    {
        public TeacherViewModel():base()
        {

        }
        Teacher teacher;
        public TeacherViewModel(Teacher teacher)
        {
            this.teacher = teacher;
        }
    }
}