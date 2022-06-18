using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;
using Thebes_Acadeny.Models.Entity;

namespace Thebes_Acadeny
{
    public class LevelIndexViewModel
    {
        public IList<Level> Levels { get; set; }
        public IList<Specialties> Specialties { get; set; }
        public IList<Tearm> Tearms { get; set; }
        public int LevId { get; set; }
        public int SpeId { get; set; }
        public int Teid { get; set; }
        public int AdmId { get; set; }
        
    }
}
