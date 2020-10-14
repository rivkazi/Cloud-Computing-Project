using BE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DrugsProject.Models.Patient
{
    public class PatientVM
    {
        public BE.Patient Current { get; set; }
        public PatientVM(BE.Patient patient)
        {           
            Current = patient;
        }
        public PatientVM()
        {
            Current = new BE.Patient();
        }

        [Key]
        public int Id
        {
            get { return Current.Id; }
            set { Current.Id = value; }
        }

        [DisplayName("מספר תעודת זהות")]
        [Required(ErrorMessage = "חובה להזין מספר תעודת זהות")]
        [StringLength(9, MinimumLength =7, ErrorMessage = "{0} חייב להיות בין {2} ל-{1} ספרות")]
        public string idNumber
        {
            get { return Current.idNumber; }
            set { Current.idNumber = value; }
        }

        [DisplayName("שם פרטי")]
        [Required(ErrorMessage = "חובה להזין שם פרטי")]
        [StringLength(35, MinimumLength = 2, ErrorMessage = "נא להזין שם פרטי תקין")]
        public string privateName
        {
            get { return Current.privateName; }
            set { Current.privateName = value; }
        }

        [DisplayName("שם משפחה")]
        [Required(ErrorMessage = "חובה להזין שם משפחה")]
        [StringLength(35, MinimumLength = 2, ErrorMessage = "נא להזין שם משפחה תקין")]
        public string familyName
        {
            get { return Current.familyName; }
            set { Current.familyName = value; }
        }

        [DisplayName("שם")]
        public string fullName
        {
            get { return privateName + " " + familyName; }
        }

        [DisplayName("מספר טלפון")]
        [DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "חובה להזין מספר טלפון")]
        public string phoneNumber
        {
            get { return Current.phoneNumber; }
            set { Current.phoneNumber = value; }
        }

        [DisplayName("מייל")]
        [Required(ErrorMessage = "חובה להזין כתובת מייל ליצירת קשר")]
        public string email
        {
            get { return Current.email; }
            set { Current.email = value; }
        }

        [DisplayName("תאריך לידה")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "חובה להזין תאריך לידה")]
        public DateTime birthDate
        {
            get { return Current.birthDate; }
            set { Current.birthDate = value; }
        }

        [DisplayName("גיל")]
        public int age 
        {
            get { return DateTime.Now.Year - Current.birthDate.Year; }
        }

        [DisplayName("סוג דם")]
        [Required(ErrorMessage = "חובה לבחור סוג דם. במידה ואינך יודע בחר: לא ידוע")]
        public BloodTypeEnum bloodType
        {
            get { return Current.bloodType; }
            set { Current.bloodType = value; }
        }

        [DisplayName("הסטוריה רפואית")]
        public string medicalHistory
        {
            get { return Current.medicalHistory; }
            set { Current.medicalHistory = value; }
        }
    }
}