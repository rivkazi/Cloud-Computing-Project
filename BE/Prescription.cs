using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int MedicineId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string cause { get; set; }
    }
}
