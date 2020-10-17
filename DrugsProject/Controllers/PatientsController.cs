using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BE;
using BL;
using DrugsProject.Models.Patient;

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
            if (ModelState.IsValid)
            {
                IBL bL = new BlClass();
                Dictionary<string, string> errorMessege = bL.PersonValidation(patient.Current);
                if (errorMessege.Count == 0)
                {
                    bL.UpdatePatient(patient.Current);
                    return RedirectToAction("Index");
                }
             
                foreach (var item in errorMessege)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            return View(patient);
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
            if (ModelState.IsValid)
            {
                IBL bL = new BlClass();
                Dictionary<string, string> errorMessege = bL.PersonValidation(patient.Current);
                if (errorMessege.Count == 0)
                {
                    bL.UpdatePatient(patient.Current);
                    return RedirectToAction("Index");
                }

                foreach (var item in errorMessege)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            return View(patient);
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
            IBL bL = new BlClass();
            bL.DeletePatient(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            IBL bL = new BlClass();
            bL.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
