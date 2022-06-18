using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class AddAdminViewModel
    {
        public int AdId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public IFormFile File { get; set; }

        public string ImageUrl { get; set; }
        public IList<Level> Levels { get; set; }
        public IList<Categores> Categores { get; set; }
        public IList<Specialties> Specialties { get; set; }
       
        public int SpeId { get; set; }

        public int cateId { get; set; }

        public int LevId { get; set; }
    }
}
