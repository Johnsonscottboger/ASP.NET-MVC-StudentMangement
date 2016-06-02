using StudentManagement.DataContext;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentManagement.Repository
{
    public class StudentRepository
    {
        Repository<Student> stuRep = new Repository<Student>(new Context());     
    }
}