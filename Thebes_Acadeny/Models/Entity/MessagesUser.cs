using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny.Models
{
    public class MessagesUser
    {
        [Key]
        public int MessageUserId { get; set; }
        [Required]
        public string MessageText { get; set; }
        public DateTime DateTime { get; set; }
        public bool Bol { get; set; }
    }
}
