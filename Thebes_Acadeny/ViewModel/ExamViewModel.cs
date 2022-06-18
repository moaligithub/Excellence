using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class ExamViewModel
    {
        public IList<Exam> Exams { get; set; } 
        public int PlantId { get; set; }
        public int AdminId { get; set; }
        public IList<Plant> Plants { get; set; }
    }
}
