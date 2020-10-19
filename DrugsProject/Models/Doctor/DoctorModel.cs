using BL;
using System.Collections.Generic;

namespace DrugsProject.Models.Doctor
{
    public class DoctorModel
    {
        public IEnumerable<BE.Doctor> doctors { get; set; }

        public DoctorModel()
        {
            BlClass bL = new BlClass();
            doctors = bL.GetDoctors();
        }
        public List<DoctorVM> getDoctorVms()
        {
            List<DoctorVM> DoctorVms = new List<DoctorVM>();
            foreach (var item in doctors)
            {
                DoctorVms.Add(new DoctorVM(item));
            }
            return DoctorVms;
        }
    }
}
