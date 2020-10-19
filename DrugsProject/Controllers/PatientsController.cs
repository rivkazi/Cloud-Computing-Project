using BE;
using BL;
using DrugsProject.Models.Patient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DrugsProject.Controllers
{
    public class PatientsController : Controller
    {

        // GET: Patients
        public ActionResult Index()
        {
            PatientModel patientModel = new PatientModel();
            var patients = patientModel.getPatientVms();
            return View(patients);
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Patient patient = bL.GetPatient(id);

            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(new PatientVM(patient));
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PatientVM patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IBL bL = new BlClass();
                    Dictionary<string, string> errorMessege = bL.PersonValidation(patient.Current);
                    if (errorMessege.Count == 0)
                    {
                        bL.AddPatient(patient.Current);
                        ViewBag.TitlePopUp = "עבר בהצלחה";
                        ViewBag.Message = "המטופל.ת התווספ.ה בהצלחה למאגר המטופלים";
                        return View("Index", new PatientModel().getPatientVms());

                    }

                    foreach (var item in errorMessege)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                }
                return View(patient);
            }
            catch (Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View("Index", new PatientModel().getPatientVms());
            }
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Patient patient = bL.GetPatient(id);

            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(new PatientVM(patient));
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PatientVM patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IBL bL = new BlClass();
                    Dictionary<string, string> errorMessege = bL.PersonValidation(patient.Current);
                    if (errorMessege.Count == 0)
                    {
                        bL.UpdatePatient(patient.Current);
                        ViewBag.TitlePopUp = "עבר בהצלחה";
                        ViewBag.Message = "המטופל.ת עודכנ.ה בהצלחה במאגר המטופלים";
                        return View("Index", new PatientModel().getPatientVms());

                    }

                    foreach (var item in errorMessege)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                }
                return View(patient);
            }
            catch (Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View("Index", new PatientModel().getPatientVms());
            }
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Patient patient = bL.GetPatient(id);

            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(new PatientVM(patient));
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                IBL bL = new BlClass();
                bL.DeletePatient(id);
                ViewBag.TitlePopUp = "עבר בהצלחה";
                ViewBag.Message = "המטופלץת נמחק.ה בהצלחה ממאגר המטופלים";
                return View("Index", new PatientModel().getPatientVms());
            }
            catch (Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View("Index", new PatientModel().getPatientVms());
            }
        }
    }
}
