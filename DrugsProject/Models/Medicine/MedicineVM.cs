using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DrugsProject.Models
{
    public class MedicineVM
    {
        public BE.Medicine Current { get; set; }
        public MedicineVM(BE.Medicine medicine)
        {
            Current = medicine;
        }
        public MedicineVM()
        {
            Current = new BE.Medicine();
        }

        [Key]
        public int Id
        {
            get { return Current.Id; }
            set { Current.Id = value; }
        }

        [ReadOnlyAttribute(true)]
        public string NDC
        {
            get { return Current.NDC; }
            set { Current.NDC = value; }
        }

        [DisplayName("שם גנרי")]
        [Required(ErrorMessage = "חובה להזין שם גנרי")]
        public string genericaName
        {
            get { return Current.genericaName; }
            set { Current.genericaName = value; }
        }

        [DisplayName("גודל")]
        [Required(ErrorMessage = "חובה להזין כמות")]
        public int size
        {
            get { return Current.size; }
            set { Current.size = value; }
        }

        [DisplayName("יצרן")]
        public string manufacturer
        {
            get { return Current.manufacturer; }
            set { Current.manufacturer = value; }
        }

        [DisplayName("שם מסחרי")]
        [Required(ErrorMessage = "חובה להזין שם מסחרי")]
        public string commercialName
        {
            get { return Current.commercialName; }
            set { Current.commercialName = value; }
        }

        [DisplayName("מאפיינים")]
        [Required(ErrorMessage = "חובה להזין מאפיינים")]
        public string properties
        {
            get { return Current.properties; }
            set { Current.properties = value; }
        }

        [Required(ErrorMessage = "חובה לבחור תמונה")]
        public HttpPostedFileBase img
        {
            get; set;
        }


        [DisplayName("תמונה")]
        public string imagePath
        {
            get { return Current.imagePath; }
            set { Current.imagePath = value; }
        }

        [DisplayName("תמונה")]
        public string fullImagePath
        {
            get
            {
                return $"/img/{Current.imagePath}";
            }
        }
    }
}