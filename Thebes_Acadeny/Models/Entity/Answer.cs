using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }
        public string AnswerTitle { get; set; }
        public bool boolAnswer { get; set; }
        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public Question Question { get; set; }

        //public int ExamId { get; set; }
        //[ForeignKey(nameof(ExamId))]
        public Exam Exam { get; set; }
    }
}
