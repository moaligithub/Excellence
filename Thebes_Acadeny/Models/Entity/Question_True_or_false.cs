using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Question_True_or_false
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string img { get; set; }
        public bool Answer { get; set; }
        public int ExamIdFk { get; set; }
        [ForeignKey(nameof(ExamIdFk))]
        public Exam Exam { get; set; }
    }
}
