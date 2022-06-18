using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        
        [Required]
        public string FullName { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string UserName { get; set; }

        public string WhatsApp { get; set; }

        [Required]
        public string PhoneNumper { get; set; }
        public string ImageUrl { get; set; }
        public int LevelIdFk { get; set; }
        [ForeignKey(nameof(LevelIdFk))]
        public Level Level { get; set; }
        public int GategoryIdFk { get; set; }
        [ForeignKey(nameof(GategoryIdFk))]
        public Categores Categores { get; set; }
        public int SpecialtiesId { get; set; }
        [ForeignKey(nameof(SpecialtiesId))]
        public Specialties Specialties { get; set; }
        public ICollection<Books> Books { get; set; }
        public ICollection<Posts> Posts { get; set; }
        public ICollection<Exam> Exam { get; set; }
        public ICollection<Video> Video { get; set; }

    }
}
