using BL;
using System.Collections.Generic;

namespace DrugsProject.Models.Patient
{
    public class PatientModel
    {
        public IEnumerable<BE.Patient> patients { get; set; }

        public PatientModel()
        {
            BlClass bL = new BlClass();
            patients = bL.GetPatients();
        }
        public List<PatientVM> getPatientVms()
        {
            List<PatientVM> PatientVms = new List<PatientVM>();
            foreach (var item in patients)
            {
                PatientVms.Add(new PatientVM(item));
            }
            return PatientVms;
        }
    }
}
