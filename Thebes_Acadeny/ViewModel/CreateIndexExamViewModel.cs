using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class CreateIndexExamViewModel
    {
        public Exam Examm { get; set; }
        public IList<Question> Questionss { get; set; }
        public string QuestionName { get; set; }
        public IList<Answer> Answerss { get; set; }
        public IList<Question_True_or_false> question_True_Or_Falses { get; set; }
        public IList<int> TrueAnswer { get; set; }
        public int FirstTrueAnswer { get; set; }
        public int ExamId { get; set; }
    }
}
