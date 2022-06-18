using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class Categores
    {
        [Key]
        public int GategoryId { get; set; }
       
        public string CategoryName { get; set; }
        public ICollection<Admin> Admins { get; set; }
    }
}
