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
using DrugsProject.Models.Doctor;

namespace DrugsProject.Controllers
{
    public class DoctorsController : Controller
    {
        // GET: Doctors
        public ActionResult Index()
        {
            DoctorModel doctorModel = new DoctorModel();
            var doctors = doctorModel.getDoctorVms();
            return View(doctors);
        }

        // GET: Doctors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Doctor doctor = bL.GetDoctor(id);

            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(new DoctorVM(doctor));
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorVM doctor)
        {
            if (ModelState.IsValid)
            {
                IBL bL = new BlClass();
                Dictionary<string, string> errorMessege = bL.AddDoctor(doctor.Current);
                if (errorMessege.Count == 0)
                    return RedirectToAction("Index");
                foreach (var item in errorMessege)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Doctor doctor = bL.GetDoctor(id);

            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(new DoctorVM(doctor));
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DoctorVM doctor)
        {
            if (ModelState.IsValid)
            {
                IBL bL = new BlClass();
                Dictionary<string, string> errorMessege = bL.UpdateDoctor(doctor.Current);
                if (errorMessege.Count == 0)
                    return RedirectToAction("Index");
                foreach (var item in errorMessege)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Doctor doctor = bL.GetDoctor(id);

            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(new DoctorVM(doctor));
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IBL bL = new BlClass();
            bL.DeleteDoctor(id);
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
