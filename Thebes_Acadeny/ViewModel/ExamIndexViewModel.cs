using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class ExamIndexViewModel
    {
        public IList<Exam> Exams { get; set; }
        public int IdAdmin { get; set; }
        public int PlantId { get; set; }
    }
}
