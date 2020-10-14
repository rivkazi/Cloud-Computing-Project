using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Patient : Person
    {
        public DateTime birthDate { get; set; }

        public BloodTypeEnum bloodType { get; set; }

        public string medicalHistory { get; set; }
    }
}
