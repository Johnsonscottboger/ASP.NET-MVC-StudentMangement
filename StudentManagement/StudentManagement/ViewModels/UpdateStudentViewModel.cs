using StudentManagement.Helpers;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentManagement.ViewModels
{
    public class UpdateStudentViewModel
    {
        private static int _courseId;
        public int CourseId { get { return _courseId; } set { _courseId = value; } }

        private static int _studentId;
        public int StudentId { get { return _studentId; } set { _studentId = value; } }

        [Display(Name="姓名")]
        [Required]
        [StringLength(10, ErrorMessage = "最长为10")]
        public string Name { get; set; }

        [Display(Name ="学号")]
        [Required]
        [StringLength(10,ErrorMessage ="最长为10")]
        public string Number { get; set; }

        [Display(Name ="第1次")]
        [Range(0.0,100.0,ErrorMessage ="在0到100之间")]
        [IsScore(ErrorMessage ="请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore1 { get; set; }

        [Display(Name = "第2次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore2 { get; set; }

        [Display(Name = "第3次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore3 { get; set; }

        [Display(Name = "第4次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore4 { get; set; }

        [Display(Name = "第5次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore5 { get; set; }

        [Display(Name = "第6次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore6 { get; set; }

        [Display(Name = "第7次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore7 { get; set; }

        [Display(Name = "第8次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore8 { get; set; }

        [Display(Name = "第9次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore9 { get; set; }

        [Display(Name = "第10次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore10 { get; set; }

        [Display(Name = "第11次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore11 { get; set; }

        [Display(Name = "第12次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore12 { get; set; }

        [Display(Name = "第13次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore13 { get; set; }

        [Display(Name = "第14次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore14 { get; set; }

        [Display(Name = "第15次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore15 { get; set; }

        [Display(Name = "第16次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExeScore16 { get; set; }

        [Display(Name = "第1次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float TestScore1 { get; set; }

        [Display(Name = "第2次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float TestScore2 { get; set; }

        [Display(Name = "第3次")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float TestScore3 { get; set; }

        [Display(Name = "考试分数")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float ExamScore { get; set; }

        [Display(Name = "总成绩")]
        [Range(0.0, 100.0, ErrorMessage = "在0到100之间")]
        [IsScore(ErrorMessage = "请输入正确的分数，确保分数为整数或小数部分为0.5")]
        public float GradeScore { get; set; }
    }
}