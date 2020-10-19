using BE;
using BL;
using DrugsProject.Models.Doctor;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

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
            try
            {
                if (ModelState.IsValid)
                {
                    IBL bL = new BlClass();
                    Dictionary<string, string> errorMessege = bL.PersonValidation(doctor.Current);
                    if (errorMessege.Count == 0)
                    {
                        bL.AddDoctor(doctor.Current);
                        ViewBag.TitlePopUp = "עבר בהצלחה";
                        ViewBag.Message = "הרופא.ה התווספ.ה בהצלחה למאגר הרופאים";
                        return View("Index", new DoctorModel().getDoctorVms());

                    }

                    foreach (var item in errorMessege)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                }
                return View(doctor);
            }
            catch (Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View("Index", new DoctorModel().getDoctorVms());
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    IBL bL = new BlClass();
                    Dictionary<string, string> errorMessege = bL.PersonValidation(doctor.Current);
                    if (errorMessege.Count == 0)
                    {
                        bL.UpdateDoctor(doctor.Current);
                        ViewBag.TitlePopUp = "עבר בהצלחה";
                        ViewBag.Message = "הרופא.ה עודכנ.ה בהצלחה במאגר הרופאים";
                        return View("Index", new DoctorModel().getDoctorVms());

                    }

                    foreach (var item in errorMessege)
                    {
                        ModelState.AddModelError(item.Key, item.Value);
                    }
                }
                return View(doctor);
            }
            catch (Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View("Index", new DoctorModel().getDoctorVms());
            }
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
            try
            {
                IBL bL = new BlClass();
                bL.DeleteDoctor(id);
                ViewBag.TitlePopUp = "עבר בהצלחה";
                ViewBag.Message = "הרופא.ה נמחק.ה בהצלחה ממאגר הרופאים";
                return View("Index", new DoctorModel().getDoctorVms());
            }
            catch (Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View("Index", new DoctorModel().getDoctorVms());
            }

        }
    }
}
