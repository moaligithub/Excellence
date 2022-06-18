using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }

        [Required]
        public string Title { get; set; }
        public bool Assent { get; set; }
        public int AdminIdFk { get; set; }
        [ForeignKey(nameof(AdminIdFk))]
        public Admin Admin { get; set; }
        //public int PlantIdFk { get; set; }
        //[ForeignKey(nameof(PlantIdFk))]
        public Plant Plant { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Question_True_or_false> question_True_Or_Falses { get; set; }
        public ICollection<Answer> answers { get; set; }
    }
}
 