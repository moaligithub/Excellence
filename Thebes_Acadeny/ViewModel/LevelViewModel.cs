using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class LevelViewModel
    {
        public IList<Level> Levels { get; set; }
        public int SpeId { get; set; }
    }
}
