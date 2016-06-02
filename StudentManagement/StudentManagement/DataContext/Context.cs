using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StudentManagement.DataContext
{
    public class Context:DbContext
    {
        public Context()
            :base("StudentManagement")
        {

        }
        public DbSet<Student> StudentTable { get; set; }
        public DbSet<Teacher> TeacherTable { get; set; }
        public DbSet<Course> CourseTable { get; set; }

        public DbSet<Exercise> ExerciseTable { get; set; }
        public DbSet<Exam> ExamTable { get; set; }
        public DbSet<Test> TestTable { get; set; }
        public DbSet<Grade> GradeTable { get; set; }
    }
}