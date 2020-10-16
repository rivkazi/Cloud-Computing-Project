using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Medicine
    {
        public int Id { get; set; }
        public string NDC { get; set; }
        public int size { get; set; }
        public string genericaName { get; set; }
        public string manufacturer { get; set; }
        public string commercialName  { get; set; }
        public string properties { get; set; }
        public string imagePath { get; set; }
    }
}
