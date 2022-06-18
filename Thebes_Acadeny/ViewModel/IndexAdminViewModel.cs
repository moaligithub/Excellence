using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class IndexAdminViewModel
    {
        public IList<Admin> Admins { get; set; }
        public IList<Specialties> Specialties { get; set; }
        public IList<Level> Levels { get; set; }
    }
}
