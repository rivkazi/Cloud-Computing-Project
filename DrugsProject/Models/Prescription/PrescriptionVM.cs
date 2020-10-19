using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DrugsProject.Models.Prescription
{
    public class PrescriptionVM
    {
        public BE.Prescription Current { get; set; }
        public PrescriptionVM(BE.Prescription pre)
        {
            Current = pre;
        }
        public PrescriptionVM()
        {
            Current = new BE.Prescription();
        }

        [Key]
        public int Id 
        { 
            get { return Current.Id; }
            set { Current.Id = value; }
        }
        public int PatientId 
        {
            get { return Current.PatientId; }
            set { Current.PatientId = value; }
        }
        public int DoctorId
        {
            get { return Current.DoctorId; }
            set { Current.DoctorId = value; }
        }
        public int MedicineId
        {
            get { return Current.MedicineId; }
            set { Current.MedicineId = value; }
        }

        [DisplayName("תאריך התחלה")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "חובה להזין תאריך התחלה")]
        public DateTime startDate
        {
            get { return Current.startDate; }
            set { Current.startDate = value; }
        }
        
        [DisplayName("תאריך סיום")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "חובה להזין תאריך סיום")]
        public DateTime endDate
        {
            get { return Current.endDate; }
            set { Current.endDate = value; }
        }

        [DisplayName("סיבה")]
        [Required(ErrorMessage = "חובה להזין סיבה")]
        public string cause 
        {
            get { return Current.cause; }
            set { Current.cause = value; } 
        }
    }
}