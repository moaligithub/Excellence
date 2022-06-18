using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class PlantViewModel
    {
        public IList<Plant> Plants { get; set; }
        public int AdminId { get; set; }
    }
}
