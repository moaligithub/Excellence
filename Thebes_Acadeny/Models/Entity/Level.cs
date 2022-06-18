using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Level
    {
        [Key]
        public int LevelId { get; set; }

        [Required]
        public string LevelName { get; set; }
        public ICollection<Admin> Admins { get; set; }
        public ICollection<Plant> Plants { get; set; }
    }
}
