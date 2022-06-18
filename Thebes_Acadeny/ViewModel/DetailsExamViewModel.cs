using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class DetailsExamViewModel
    {
        public Exam Exam { get; set; }
        public int AnswerId { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<Answer> Answers { get; set; }
        
        public IList<Question_True_or_false> Question_True_Or_Falses { get; set; }
        public Admin Admin { get; set; }
      
        [MaxLength(20 , ErrorMessage ="لايمكن ادخال الاسم الاول باكثر من 20 حرف") , Required(ErrorMessage ="يجب ادخال الاسم الاول")]
        public string FirstName { get; set; }
      
        [MaxLength(20, ErrorMessage = "لايمكن ادخال الاسم الثاني باكثر من 20 حرف") , Required(ErrorMessage ="يجب ادخال الاسم الثاني")]
        public string LastName { get; set; }
        public IList<int> True_Answer { get; set; }
        public IList<int> True_Answer_2 { get; set; }
        public int Exam_Id { get; set; }
        public int PlantId { get; set; }
        public IList<int> False_1 { get; set; }
        public string AdminName { get; set; }
    }
}
