using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        public string Title { get; set; }
        public string img { get; set; }
        public int ExamIdFk { get; set; }
        [ForeignKey(nameof(ExamIdFk))]
        public Exam Exam { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
