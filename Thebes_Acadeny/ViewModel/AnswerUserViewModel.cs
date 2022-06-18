using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.ViewModel
{
    public class AnswerUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ExamName { get; set; }
        public string PlantName { get; set; }
        public string Level { get; set; }
        public string Spe { get; set; }
        public int Result { get; set; }
        public string m { get; set; }
        public string AdminName { get; set; }
        public int ExamId { get; set; }
        public IList<string> AnswerId { get; set; }
        public IList<int> AnId { get; set; }
        public IList<int> QuestionId { get; set; }
        public IList<int> Question_trueOrFalseId { get; set; }
    }
}
