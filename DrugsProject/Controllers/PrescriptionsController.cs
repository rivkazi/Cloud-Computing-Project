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

namespace DrugsProject.Controllers
{
    public class PrescriptionsController : Controller
    {

        // GET: Prescriptions
        public ActionResult Index()
        {
            IBL bl = new BlClass();
            var prescriptions = bl.GetPrescriptions();
            return View(prescriptions);
        }

        // GET: Prescriptions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Prescription prescription = bL.GetPrescription(id);

            if (prescription == null)
            {
                return HttpNotFound();
            }
            return View(prescription);
        }

        // GET: Prescriptions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                IBL bL = new BlClass();
                bool IsAddOk = bL.AddPrescription(prescription);
                if (IsAddOk)
                    return RedirectToAction("Index");
                else
                    ViewBag.Error = "שגיאה מצערת בהוספת התרופה";
            }
            return View(prescription);
        }

        protected override void Dispose(bool disposing)
        {
            IBL bL = new BlClass();
            bL.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
