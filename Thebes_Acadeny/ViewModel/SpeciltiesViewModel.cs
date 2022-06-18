using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny
{
    public class SpeciltiesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }
    }
}
