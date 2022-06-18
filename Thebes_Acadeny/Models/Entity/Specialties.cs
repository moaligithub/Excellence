using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Specialties
    {
        [Key]
        public int SpecialtiesId { get; set; }

        [Required]
        public string SpecialtiesName { get; set; }
        public ICollection<Plant> Plants { get; set; }
        public ICollection<Admin> Admins { get; set; }
    }
}
