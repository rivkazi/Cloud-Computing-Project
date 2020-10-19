using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public interface IDAL
    {
        //ADD
        void AddDoctor(Doctor doctor);
        void AddMedicine(Medicine medicine, HttpPostedFileBase httpPostedFile);
        void AddPatient(Patient patient);
        void AddPrescription(Prescription prescription);
        void AddCronicalDisease(CronicalDisease cronicalDisease);

        //UPDATE
        void UpdateDoctor(Doctor doctor);
        void UpdateMedicine(Medicine medicine, HttpPostedFileBase httpPostedFile);
        void UpdatePatient(Patient patient);

        //DELETE
        bool DeleteDoctor(int? id);
        bool DeleteMedicine(int? id);
        bool DeletePatient(int? id);

        //GET
        Doctor GetDoctor(int? id);
        Medicine GetMedicine(int? id);
        Patient GetPatient(int? id);
        Prescription GetPrescription(int? id);
        string GetNDCForMedicine(string genericName); 
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
