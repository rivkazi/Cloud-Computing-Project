using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Doctor : Person
    {
        public int licenseNumber { get; set; }
        public string specialization { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
    }
}
