using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IBL
    {
        //ADD
        bool AddDoctor(Doctor doctor);
        bool AddMedicine(Medicine medicine);
        bool AddPatient(Patient patient);
        bool AddPrescription(Prescription prescription);
        bool AddCronicalDisease(CronicalDisease prescription);

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
        IEnumerable<Person> GetAllPerson(Func<Person, bool> predicat = null);
        IEnumerable<Doctor> GetDoctors(Func<Doctor, bool> predicat = null);
        IEnumerable<Medicine> GetMedicines(Func<Medicine, bool> predicat = null);
        IEnumerable<Patient> GetPatients(Func<Patient, bool> predicat = null);
        IEnumerable<Prescription> GetPrescriptions(Func<Prescription, bool> predicat = null);
        IEnumerable<CronicalDisease> GetCronicalDiseases(Func<CronicalDisease, bool> predicat = null);
        List<string> GetNDCForAllActiveMedicine(int patientID);

        //FILTER
        IEnumerable<Prescription> FilterPrescriptionsForPatient(int patientID);
        IEnumerable<Prescription> FilterActivePrescriptionsForPatient(int patientID);
        IEnumerable<Medicine> FilterActiveMedicinesForPatient(int patientID);
        IEnumerable<CronicalDisease> FilterCronicalDiseasesForPatient(int patientID);

        //DISPOSE
        void Dispose(bool disp);

        //SEND
        void SendMail(string mailAdress,string subject, string receiverName, string message);

        //CODE
        int GetRandomCode();
        bool CheckOneTimeCode(int randonCode, int codeEntered);

        //ACCOUNT
        bool SignIn(string userName, string password);
        void SignUp(Person person);
        void ForgotPassword(string mail);

        //Image Service
        bool ValidateImage(string path);
        string ConvertStringToUrl(string path);

        //CONFLICT DRUGS SERVICE 
        List<string> IsConflict(List<string> NDC);
    }
}
