using BE;
using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Razor.Parser;

namespace DrugsProject.Models
{
    public class MedicineVM
    {
        public BE.Medicine Current { get; set; }
        public MedicineVM(BE.Medicine medicine)
        {
            Current = medicine;
        }
        public int Id 
        {
            get { return Current.Id; }
            set { Current.Id = value; }
        }

        [DisplayName("שם גנרי")]
        public string genericaName
        {
            get { return Current.genericaName; }
            set { Current.genericaName = value; }
        }

        [DisplayName("יצרן")]
        public string manufacturer
        {
            get { return Current.manufacturer; }
            set { Current.manufacturer = value; }
        }

        [DisplayName("שם מסחרי")]
        public string commercialName
        {
            get { return Current.commercialName; }
            set { Current.commercialName = value; }
        }

        [DisplayName("מאפיינים")]
        public string properties
        {
            get { return Current.properties; }
            set { Current.properties = value; }
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