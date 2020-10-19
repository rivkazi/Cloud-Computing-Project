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
        public string password { get; set; }

        public Doctor() : base()
        {
            licenseNumber = 0;
            specialization = "";
            password = "";
        }
        public Doctor(string first, string last, string id, string phone, string mail, int license, string speciali, string pass) 
        {
            privateName = first;
            familyName = last;
            idNumber = id;
            phoneNumber = phone;
            email = mail;
            licenseNumber = license;
            specialization = speciali;
            password = pass;
        }
    }
}
