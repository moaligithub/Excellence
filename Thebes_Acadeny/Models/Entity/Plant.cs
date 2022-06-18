using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models.Entity;

namespace Thebes_Acadeny.Models
{
    public class Plant
    {
        [Key]
        public int PlantId { get; set; }

        [Required]
        public string PlantName { get; set; }
        public int LevelIdFk { get; set; }
        [ForeignKey(nameof(LevelIdFk))]
        public Level Level { get; set; }
        public int SpecialtiesIdFk { get; set; }
        [ForeignKey(nameof(SpecialtiesIdFk))]
        public Specialties Specialties { get; set; }
        public Tearm Tearm { get; set; }
        public ICollection<Books> Books { get; set; }     
        public ICollection<Exam> Exams { get; set; }
        public ICollection<Posts> Posts { get; set; }
        public ICollection<Video> VideoAdmins { get; set; }
        
    }
}
