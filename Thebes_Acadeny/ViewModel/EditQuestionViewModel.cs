using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny.ViewModel
{
    public class EditQuestionViewModel
    {
        public string QusetionName { get; set; }
        public int QuestionId { get; set; }
        public string question_True_Or_False_Name { get; set; }
        public int question_true_false_Id { get; set; }
        public int AnswerTrue { get; set; }
        public IList<string> Answer { get; set; }
        public int AdminId { get; set; }
        public int PlantId { get; set; }
        public int ExamId { get; set; }
        public string imgurl { get; set; }
        public IFormFile file { get; set; }
    }
}
