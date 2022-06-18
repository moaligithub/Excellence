using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models.Entity
{
    public class UrlWebSite
    {
        [Key]
        public int UrlId { get; set; }
        public string UrlText { get; set; }
    }
}
