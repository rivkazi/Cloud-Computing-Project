using BE;
using BL;
using DrugsProject.Models.Doctor;
using DrugsProject.Models.Patient;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DrugsProject.Models
{
    public static class Extension
    {
        public static IEnumerable<Prescription> GetPrescriptionsForPatient(this PatientVM patVM)
        {
            IBL bl = new BlClass();
            return bl.FilterPrescriptionsForPatient(patVM.Current.Id);
        }
        public static BE.Medicine GetMedicineForPrescription(this Prescription pre)
        {
            IBL bl = new BlClass();
            return bl.GetMedicine(pre.MedicineId);
        }
        public static DoctorVM GetDoctorForPrescription(this Prescription pre)
        {
            IBL bl = new BlClass();
            return new DoctorVM(bl.GetDoctor(pre.DoctorId));
        }
        public static MvcHtmlString DisplayImage(this HtmlHelper html, string imgPath, int size = 150, string cla = "")
        {
            return new MvcHtmlString($"<img src='{imgPath}' class='{cla}' height='{size}' width='{size}'/>");
        }

        public static MvcHtmlString DisplayHeader(this HtmlHelper html, string textForHeader, int size)
        {
            return new MvcHtmlString($"<h{size} class='panel-heading bold'>{textForHeader}</h{size}>");
        }
        public static MvcHtmlString DisplayHeaderColorful(this HtmlHelper html, string textForBlackHeader, string textForColorHeader, int size, string icon = "", string link = "", string icon2 = "", string link2 = "")
        {
            if (icon == "")
                return new MvcHtmlString($"<h{size} class='panel-heading'>{ textForBlackHeader } <span> { textForColorHeader }</span></h{size}>");
            string Link1 = $"<a href='{link}'><i class='{icon}'></i></a><a href='{link2}'><i class='{icon2}'></i></a>";
            string header = $"{ textForBlackHeader } <span> { textForColorHeader }</span>";
            return new MvcHtmlString($"<h{size} class='panel-heading bold'>{header} {Link1}</h{size}>");
        }

        public static MvcHtmlString DisplayItemWithIcon(this HtmlHelper html, Object text, string icon)
        {
            if (text is DateTime)
                text = Convert.ToDateTime(text).ToString("dd/MM/yyyy");
            else if (text is BloodTypeEnum)
                text = text.ToString().Replace('_', ' ');
            return new MvcHtmlString($"<li> <i class='{icon}' aria-hidden='true'></i> {text}</li>");
        }

        public static MvcHtmlString DropDownListForMedicinesName(this HtmlHelper htmlHelper)
        {
            IBL bl = new BlClass();
            string options = $"<option disabled selected>בחר שם גנרי</option>";
            /*change the NDC list*/
            Dictionary<string, string> ndc = new Dictionary<string, string>() {
                { "advil","01123"},
                {"acamol","55678"},
                {"optalgin","98989"},
                {"med", "65177" } };
            foreach (var number in ndc)
            {
                options += $"<option value ='{number.Key}'>{number.Key}</option>";
            }
            return new MvcHtmlString($"<select class='form-control'>{options}</select>");
        }

        //public static MvcHtmlString ShowPrescription(this HtmlHelper htmlHelper, Prescription prescription)
        //{
        //    IBL bl = new BlClass();
        //    string result = "";
        //    result += @"< div class='{panel}'>
        //        <div class='{panel-heading}' role='{tab}' id='{headingOne}'>
        //            <h4 class='{panel-title}'>
        //                <div class='{icon-circle}'> <i class='{fa fa-heartbeat}'></i></div>
        //                <a role = '{button}' data-toggle='{collapse}' data-parent= '{#accordion}' href='{#collapseOne}' aria -expanded='{true}' aria -controls='{collapseOne}'>
        //                    How do I purchase your product?
        //                </a>
        //            </h4>
        //        </div>
        //        <div id = '{collapseOne}' class='{panel-collapse collapse}' role ='{tabpanel}' aria -labelledby= '{headingOne}'>
        //            < div class='{panel -body}'>
        //                Lorem ipsum dolor sit amet, sed in nostro latine, eu option appetere mediocritatem duo.Pro duis magna perpetua ea. Dicant epicurei gubergren eos ne, ad suas ornatus graecis nam, pri quot liber ignota no.Usu et erat propriae invenire, blandit voluptua
        //                vim at, iuvaret albucius cu ius. Te integre diceret praesent eos, impetus legimus te vim. Ne mollis veritus est.
        //            </div>
        //        </div>
        //    </div>";

        //    return new MvcHtmlString(result);
        //}

        //public static MvcHtmlString DropDownListForHositals(this HtmlHelper htmlHelper, string name)
        //{
        //    IBL bl = new BlClass();
        //    string options = "";
        //    foreach (var hospital in bl.GetHospitals())
        //    {
        //        options += $"<option value ='{hospital.HospitalId}'> {hospital.HospitalName} </option>";
        //    }
        //    return new MvcHtmlString($"<select class='form-control' name='{name}'>{options}</select>");
        //}


        //public static MvcHtmlString DisplayNameFotInSpan(this HtmlHelper htmlHelper)
        //{
        //    string 


        //    IBL bl = new BlClass();
        //    string options = "";
        //    foreach (var hospital in bl.GetHospitals())
        //    {
        //        options += $"<option value ='{hospital.HospitalId}'> {hospital.HospitalName} </option>";
        //    }
        //    return new MvcHtmlString($"<select class='form-control' name='{name}'>{options}</select>");
        //}
    }
}