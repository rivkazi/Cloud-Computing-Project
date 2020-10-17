using BE;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Policy;
using System.Web;

namespace BL
{
    public class BlClass : IBL
    {
        #region ADD
        public bool AddCronicalDisease(CronicalDisease cronicalDisease)
        {
            IDAL dal = new DalClass();
            IEnumerable<CronicalDisease> diseases = dal.GetCronicalDiseases(cd => cd.description == cronicalDisease.description);
            if (diseases.Count() == 0)
                return false;
            return dal.AddCronicalDisease(cronicalDisease);
        }

        public bool AddDoctor(Doctor doctor)
        {
            //Dictionary<string, string> result = PersonValidation(doctor);
            //if (result.Count != 0)
            //    return result;
            IDAL dal = new DalClass();
            IEnumerable<Doctor> doctors = dal.GetDoctors(doc => doc.idNumber == doctor.idNumber);
            //if (doctors.Count() == 0)
            //חלון קופץ משתמש קיים או שגיאת מערכת אם חזר פולס מהדאל
            //הוספה
            return dal.AddDoctor(doctor);
        }


        public bool AddMedicine(Medicine medicine)
        {
            IDAL dal = new DalClass();
            medicine.NDC = GetNDCForMedicine(medicine.genericaName);
            bool IsOkImage = ValidateImage(medicine.imagePath);
            if (IsOkImage)
            {
                medicine.manufacturer = medicine.manufacturer == null ? "לא ידוע" : medicine.manufacturer;
                return dal.AddMedicine(medicine);
            }
            else
                return false;
        }

        public bool AddPatient(Patient patient)
        {
            //Dictionary<string, string> result = PersonValidation(patient);
            //if (result.Count != 0)
            //    return result;

            patient.medicalHistory = patient.medicalHistory == null ? "לא נמסר מידע" : patient.medicalHistory;
            IDAL dal = new DalClass();
            IEnumerable<Patient> patients = dal.GetPatients(p => p.idNumber == patient.idNumber);
            //if (doctors.Count() == 0)
            //חלון קופץ משתמש קיים או שגיאת מערכת אם חזר פולס מהדאל
            //הוספה
            return dal.AddPatient(patient);
        }

        public bool AddPrescription(Prescription prescription)
        {
            IDAL dal = new DalClass();
            if (prescription.endDate < prescription.startDate)
                return false;
            List<string> NDCforPatientMedicines = GetNDCForAllActiveMedicine(prescription.PatientId);
            List<string> Result = IsConflict(NDCforPatientMedicines);
            bool isConflict = Result[1] == "true" ? true : false;
            if (isConflict)
                return false;
            else
                return dal.AddPrescription(prescription);
        }

        #endregion

        #region VALIDATION
        public Dictionary<string, string> PersonValidation(Person person)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (!Validation.IsId(person.idNumber))
                result.Add("idNumber", "מספר תעודת הזהות אינו תקין");
            if (!Validation.IsEmail(person.email))
                result.Add("email", "כתובת המייל אינה תקינה");
            if (!Validation.IsPhone(person.phoneNumber))
                result.Add("phoneNumber", "מספר הטלפון אינו תקין");
            if (!Validation.IsName(person.familyName))
                result.Add("familyName", "שם זה אינו תקין");
            if (!Validation.IsName(person.privateName))
                result.Add("privateName", "שם זה אינו תקין");
            return result;
        }

        public Dictionary<string, string> SignValidation(DoctorSign doctorSign)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            if (doctorSign.idNumber == null)
                result.Add("idNumber", "חובה להזין מספר ת.ז.");
            else if (!Validation.IsId(doctorSign.idNumber))
                result.Add("idNumber", "מספר תעודת הזהות אינו תקין");
            if (doctorSign.email == null)
                result.Add("email", "חובה להזין כתובת מייל");
            else if (!Validation.IsEmail(doctorSign.email))
                result.Add("email", "כתובת המייל אינה תקינה");
            if (doctorSign.password == null)
                result.Add("password", "חובה לבחור סיסמה");
            else if (!Validation.IsPassword(doctorSign.password))
                result.Add("password", "סיסמה אינה תקינה");
            return result;
        }
        #endregion

        #region UPDATE
        public bool UpdateDoctor(Doctor doctor)
        {
            //Dictionary<string, string> result = PersonValidation(doctor);
            //if (result.Count != 0)
            //    return result;
            IDAL dal = new DalClass();
            IEnumerable<Doctor> doctors = dal.GetDoctors(doc => doc.idNumber == doctor.idNumber);
            //if (doctors.Count() == 0)
            //חלון קופץ משתמש קיים או שגיאת מערכת אם חזר פולס מהדאל
            //עדכון
            return dal.UpdateDoctor(doctor);
        }

        public bool UpdateMedicine(Medicine medicine)
        {
            IDAL dal = new DalClass();
            bool IsOkImage = ValidateImage(medicine.imagePath);
            if (IsOkImage)
            {
                medicine.manufacturer = medicine.manufacturer == null ? "לא ידוע" : medicine.manufacturer;
                return dal.UpdateMedicine(medicine);
            }
            else
                return false;
        }

        public bool UpdatePatient(Patient patient)
        {
            //Dictionary<string, string> result = PersonValidation(patient);
            //if (result.Count != 0)
            //    return result;

            patient.medicalHistory = patient.medicalHistory == null ? "לא נמסר מידע" : patient.medicalHistory;
            IDAL dal = new DalClass();
            IEnumerable<Patient> patients = dal.GetPatients(p => p.idNumber == patient.idNumber);
            //if (doctors.Count() != 0)
            //חלון קופץ משתמש קיים או שגיאת מערכת אם חזר פולס מהדאל
            //עדכן
            return dal.UpdatePatient(patient);
        }
        #endregion

        #region DELETE
        public bool DeleteDoctor(int? id)
        {
            IDAL dal = new DalClass();
            Doctor doctor = dal.GetDoctor(id);
            if (doctor == null)
                return false;
            return dal.DeleteDoctor(id);
        }

        public bool DeleteMedicine(int? id)
        {
            IDAL dal = new DalClass();
            Medicine medicine = dal.GetMedicine(id);
            if (medicine == null)
                return false;
            return dal.DeleteMedicine(id);
        }

        public bool DeletePatient(int? id)
        {
            IDAL dal = new DalClass();
            Patient patient = dal.GetPatient(id);
            if (patient == null)
                return false;
            return dal.DeletePatient(id);
        }
        #endregion

        #region GET LISTS     
        public IEnumerable<Person> GetAllPerson(Func<Person, bool> predicat = null)
        {
            IDAL dal = new DalClass();
            IEnumerable<Person> doctors = dal.GetDoctors();
            IEnumerable<Person> patients = dal.GetPatients();

            IEnumerable<Person> all = doctors.Union(patients);
            return all;
        }

        public IEnumerable<Doctor> GetDoctors(Func<Doctor, bool> predicat = null)
        {
            IDAL dal = new DalClass();
            return dal.GetDoctors(predicat);
        }
        public IEnumerable<Medicine> GetMedicines(Func<Medicine, bool> predicat = null)
        {
            IDAL dal = new DalClass();
            return dal.GetMedicines(predicat);
        }

        public List<string> GetNDCForAllActiveMedicine(int id)
        {
            IEnumerable<Medicine> medicinePatient = FilterActiveMedicinesForPatient(id);
            List<string> NDC = medicinePatient.Select(med => med.NDC).ToList();
            return NDC;
        }

        public IEnumerable<Patient> GetPatients(Func<Patient, bool> predicat = null)
        {
            IDAL dal = new DalClass();
            return dal.GetPatients(predicat);
        }
        public IEnumerable<Prescription> GetPrescriptions(Func<Prescription, bool> predicat = null)
        {
            IDAL dal = new DalClass();
            return dal.GetPrescriptions(predicat);
        }
        public IEnumerable<CronicalDisease> GetCronicalDiseases(Func<CronicalDisease, bool> predicat = null)
        {
            IDAL dal = new DalClass();
            return dal.GetCronicalDiseases(predicat);
        }

        #endregion

        #region GET
        public Doctor GetDoctor(int? id)
        {
            IDAL dal = new DalClass();
            return dal.GetDoctor(id);
        }

        public Medicine GetMedicine(int? id)
        {
            IDAL dal = new DalClass();
            return dal.GetMedicine(id);
        }

        public Patient GetPatient(int? id)
        {
            IDAL dal = new DalClass();
            return dal.GetPatient(id);
        }

        public Prescription GetPrescription(int? id)
        {
            IDAL dal = new DalClass();
            return dal.GetPrescription(id);
        }
        public string GetNDCForMedicine(string genericName)
        {
            IDAL dal = new DalClass();
            return dal.GetNDCForMedicine(genericName);
        }

        #endregion

        #region FILTER
        public IEnumerable<Prescription> FilterPrescriptionsForPatient(int patientID)
        {
            IDAL dal = new DalClass();
            return dal.GetPrescriptions(pre => pre.PatientId == patientID);
        }

        public IEnumerable<Prescription> FilterActivePrescriptionsForPatient(int patientID)
        {
            IDAL dal = new DalClass();
            return dal.GetPrescriptions(pre => pre.PatientId == patientID && pre.endDate >= DateTime.Now);
        }
        public IEnumerable<Medicine> FilterActiveMedicinesForPatient(int patientID)
        {
            IEnumerable<Prescription> prescriptions = FilterActivePrescriptionsForPatient(patientID);
            var medicinesId = prescriptions.Select(pre => pre.MedicineId);
            IEnumerable<Medicine> medicines = medicinesId.Select(medId => GetMedicine(medId));

            return medicines;
        }

        public IEnumerable<CronicalDisease> FilterCronicalDiseasesForPatient(int patientID)
        {
            IDAL dal = new DalClass();
            return dal.GetCronicalDiseases(pre => pre.PatientId == patientID);
        }
        #endregion

        #region ACCOUNT
        public bool SignIn(string mail, string password)
        {
            IDAL dal = new DalClass();
            Doctor doctor = dal.GetDoctors(doc => doc.email == mail).FirstOrDefault();
            if (doctor != null && doctor.password == password)
                return true;
            return false;
        }
        public void SignUp(DoctorSign doctorSign)
        {
            IDAL dal = new DalClass();
            Doctor doctor= dal.GetDoctors(doc => doc.idNumber == doctorSign.idNumber).FirstOrDefault();
            if (doctor != null && doctor.password == null)
            {
                doctor.password = doctorSign.password;
                dal.UpdateDoctor(doctor);
                SendMail(doctor.email, doctor.privateName + " " + doctor.familyName, "ההרשמה עברה בהצלחה", "ברוכים הבאים לאתר שלנו, שמחים שהצטרפת." + "<br/>"
                         + "נשמח לעמוד לעזרתך בכל פניה ובקשה ומקווים שתהיה לך חוויה נעימה." + "<br/>" + "תודה, צוות WiseCare");
                return;
            }
            else if(doctor == null)
            {
                SendMail(doctorSign.email, "ההרשמה נכשלה", "", "לצערנו, נסיון ההרשמה שלך לאתרנו נכשל." + "<br/>"
                         + "אנא נסה שוב בעוד חצי שנה ונשמח לעמוד לעזרתך." + "<br/>" + "תודה, צוות WiseCare");
                return;
            }
            else
            {
                throw new NotImplementedException();
                //הינך כבר רשום למערכת, אנא התחבר
            }
        }
        public void ForgotPassword(string mail)
        {
            IDAL dal = new DalClass();
            //Person person = GetAllPerson(per=> per.email == mail).FirstOrDefault();
            Doctor doctor = GetDoctors(doc => doc.email == mail).FirstOrDefault();
            if (doctor != null)
            {
                Random random = new Random();
                int newPassword = random.Next(10000, 1000000000);
                SendMail(doctor.email, doctor.privateName + " " + doctor.familyName, "איפוס סיסמה", "הסיסמה שלך אופסה, הסיסמה החדשה היא:" + "<br/>" + newPassword + "<br/>" + "אנא שנה סיסמה בהקדם האפשרי");
               
                //Edit Password
                doctor.password = newPassword.ToString();
                UpdateDoctor(doctor);
            }
        }
        #endregion

        #region SEND
        public void SendMail(string mailAdress, string subject, string receiverName, string message)
        {
            MailMessage mail;
            SmtpClient smtp;
            mail = new MailMessage();
            mail.To.Add(mailAdress);
            mail.From = new MailAddress("MyProject4Ever@gmail.com");
            mail.Subject = subject;
            mail.Body = $"שלום {receiverName}, <br>" + message;
            mail.IsBodyHtml = true;
            smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("MyProject4Ever@gmail.com", "bla/*123*/bla");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }


        #endregion

        #region IMAGE SERVICE
        public bool ValidateImage(string _path)
        {
            List<string> testImages = new List<string>()
            {"pill", "pill bottle", "pills", "medicine", "bottle", "syrup", "medical", "drug", "drugs", "cure", "prescription drug", "vitamin", "cream", "ointment"};

            List<string> Result = new List<string>(); //returning the list of results order by certain confidence
            bool flag = false;

            string path = ConvertStringToUrl(_path);

            ImageDetails DrugImage = new ImageDetails(path);

            IDAL dal = new DalClass();

            dal.GetTags(DrugImage);

            var Threshold = 60.0; //testing the result with confidence above 60%

            foreach (var item in DrugImage.Details)
            {
                if (item.Value > Threshold)
                {
                    foreach (var option in testImages) //the words we can accept
                    {
                        if (item.Key == option)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }

            return flag; /*Result;*/
        }
        public string ConvertStringToUrl(string path)
        {
            string imgPath = HttpContext.Current.Server.MapPath($"~/img/{path}");
            return imgPath;
        }
        #endregion

        #region CONFLICT DRUGS SERVICE 
        public List<string> IsConflict(List<string> NDC)
        {
            IDAL dal = new DalClass();

            List<string> strings = dal.IsConflict(NDC);
            return strings;
        }
        #endregion

        public void Dispose(bool disp)
        {
            if (disp)
            {
                IDAL dal = new DalClass();
                dal.Dispose();
            }
        }

    }
}
