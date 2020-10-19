using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DrugsProject.Models.Doctor
{
    public class DoctorVM
    {
        public BE.Doctor Current { get; set; }
        public DoctorVM(BE.Doctor doctor)
        {
            Current = doctor;
        }
        public DoctorVM()
        {
            Current = new BE.Doctor();
        }

        [Key]
        public int Id
        {
            get { return Current.Id; }
            set { Current.Id = value; }
        }

        [DisplayName("מספר תעודת זהות")]
        [Required(ErrorMessage = "חובה להזין מספר תעודת זהות")]
        [StringLength(9, MinimumLength = 7, ErrorMessage = "{0} חייב להיות בין {2} ל-{1} ספרות")]
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

        [DisplayName("מספר רשיון")]
        [Required(ErrorMessage = "חובה להזין מספר רשיון רופא")]
        public int licenseNumber
        {
            get { return Current.licenseNumber; }
            set { Current.licenseNumber = value; }
        }

        [DisplayName("התמחות")]
        [Required(ErrorMessage = "חובה להזין תחום התמחות")]
        public string specialization
        {
            get { return Current.specialization; }
            set { Current.specialization = value; }
        }

        [DisplayName("סיסמה")]
        [DataType(DataType.Password)]
        public string password
        {
            get { return Current.password; }
            set { Current.password = value; }
        }
    }
}