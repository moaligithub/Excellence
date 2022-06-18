using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class IndexViewModel
    {
        public Admin owner { get; set; }
        public IList<Admin> admins { get; set; }
        public IList<Admin> adminShimaas { get; set; }
        public IList<Specialties> specialties { get; set; }
        public IList<Level> levels { get; set; }
    }
}
