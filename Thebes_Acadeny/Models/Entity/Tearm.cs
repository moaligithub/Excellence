using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models.Entity
{
    public class Tearm
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Plant> Plants { get; set; }
    }
}
