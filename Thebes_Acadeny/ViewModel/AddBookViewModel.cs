using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class AddBookViewModel
    {
        public int BookAdminsId { get; set; }

        [Required]
        public string BookName { get; set; }
        
        [Required]
        public string PdfUrl { get; set; }

        [Required]
        public string NewPdfUrl { get; set; }

        public IList<Plant> Plants { get; set; }
        public int pid { get; set; }
        public int AdminId { get; set; }
    }
}
