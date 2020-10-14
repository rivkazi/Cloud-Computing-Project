using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IDAL
    {
        //ADD
        bool AddDoctor(Doctor doctor);
        bool AddMedicine(Medicine medicine);
        bool AddPatient(Patient patient);
        bool AddPrescription(Prescription prescription);
        bool AddCronicalDisease(CronicalDisease cronicalDisease);

        //UPDATE
        bool UpdateDoctor(Doctor doctor);
        bool UpdateMedicine(Medicine medicine);
        bool UpdatePatient(Patient patient);

        //DELETE
        bool DeleteDoctor(int? id);
        bool DeleteMedicine(int? id);
        bool DeletePatient(int? id);

        //GET
        Doctor GetDoctor(int? id);
        Medicine GetMedicine(int? id);
        Patient GetPatient(int? id);
        Prescription GetPrescription(int? id);

        IEnumerable<Doctor> GetDoctors(Func<Doctor, bool> predicat = null);
        IEnumerable<Medicine> GetMedicines(Func<Medicine, bool> predicat = null);
        IEnumerable<Patient> GetPatients(Func<Patient, bool> predicat = null);
        IEnumerable<Prescription> GetPrescriptions(Func<Prescription, bool> predicat = null);
        IEnumerable<CronicalDisease> GetCronicalDiseases(Func<CronicalDisease, bool> predicat = null);

        //IMAGE SERVICE
        void GetTags(ImageDetails CurrentImage);

        //CONFLICT DRUGS SERVICE 
        List<string> IsConflict(List<string> NDC);

        void Dispose();
    }
}
