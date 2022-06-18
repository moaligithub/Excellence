using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class ProfileViewModel<T> where T:class
    {
        public T Adm { get; set; }
        public Specialties Specialties { get; set; }
        public Level Level { get; set; }
    }
}
