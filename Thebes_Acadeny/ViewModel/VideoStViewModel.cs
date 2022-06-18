using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thebes_Acadeny.Models;

namespace Thebes_Acadeny
{
    public class VideoStViewModel
    {
        public int PlantId { get; set; }
        public IList<Video> VideoAdmins { get; set; }
        public int AdminId { get; set; }
    }
}
