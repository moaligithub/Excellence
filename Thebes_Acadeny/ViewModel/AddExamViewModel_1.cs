using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Thebes_Acadeny
{
    public class AddExamViewModel_1
    {
        public string ExamName { get; set; }
        public string Qusetion { get; set; }
        public string QusetionTrueOrFalse { get; set; }
        public int AnswerTrueOrFalse { get; set; }
        public IList<string> Answer { get; set; }
        public int AdminId { get; set; }
        public int PlantId { get; set; }
        public int ExamId { get; set; }
        public IFormFile file { get; set; }
    }
}
