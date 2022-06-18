using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public bool Assent { get; set; }
        public int AdminIdFk { get; set; }
        [ForeignKey(nameof(AdminIdFk))]
        public Admin Admin { get; set; }
        //public int PlantIdFk { get; set; }
        //[ForeignKey(nameof(PlantIdFk))]
        public Plant Plant { get; set; }
    }
}
