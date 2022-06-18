using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models.Entity;

namespace Thebes_Acadeny
{
    public class AddPlantViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        public int Tearm { get; set; }
        public IFormFile File { get; set; }
        public IList<Tearm> Tearms { get; set; }
        public int LevelId { get; set; }
        public int speid { get; set; }
    }
}
