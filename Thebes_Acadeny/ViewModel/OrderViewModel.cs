using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class OrderViewModel
    {
        public IList<Books> Books { get; set; }
        public IList<Exam> Exams { get; set; }
        public IList<Posts> Posts { get; set; }
        public IList<Video> Videos { get; set; }
        public IList<Admin> Admins { get; set; }
        public IList<Plant> Plants { get; set; }
        public string WebSiteUrl { get; set; }
    }
}
