using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thebes_Acadeny
{
    public class AddPostViewModel
    {
        public int id { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile File { get; set; }
        public int pid { get; set; }
        public int AdminId { get; set; }
    }
}
