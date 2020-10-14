using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string idNumber { get; set; }
        public string privateName { get; set; }
        public string familyName { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
    }
}
