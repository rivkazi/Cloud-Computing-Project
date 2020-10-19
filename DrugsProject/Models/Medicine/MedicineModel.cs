using BL;
using System.Collections.Generic;

namespace DrugsProject.Models.Medicine
{
    public class MedicineModel
    {
        public IEnumerable<BE.Medicine> medicines { get; set; }

        public MedicineModel()
        {
            BlClass bL = new BlClass();
            medicines = bL.GetMedicines();
        }
        public List<MedicineVM> getMedicineVms()
        {
            List<MedicineVM> medicineVMs = new List<MedicineVM>();
            foreach (var item in medicines)
            {
                medicineVMs.Add(new MedicineVM(item));
            }
            return medicineVMs;
        }
    }
}