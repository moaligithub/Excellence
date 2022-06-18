using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class PostStViewModel
    {
        public IList<Posts> PostsAdmins { get; set; }
        public int PlantId { get; set; }
        public Admin Admin { get; set; }
        public IList<Admin> Admins { get; set; }
    }
}
