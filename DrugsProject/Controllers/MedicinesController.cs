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
using DrugsProject.Models;
using DrugsProject.Models.Medicine;
using DrugsProject.Models.Patient;

namespace DrugsProject.Controllers
{
    public class MedicinesController : Controller
    {
        // GET: Medicines
        public ActionResult Index()
        {
            MedicineModel medicineModel = new MedicineModel();
            var medicines = medicineModel.getMedicineVms();
            return View(medicines);
        }

        // GET: Medicines/Details/5
        public ActionResult Details(int? id)
        {            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Medicine medicine = bL.GetMedicine(id);

            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(new MedicineVM(medicine));
        }

        // GET: Medicines/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MedicineVM medicine, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                IBL bL = new BlClass();
                bool IsAddOk = bL.AddMedicine(medicine.Current, img);
                if (IsAddOk)
                    return RedirectToAction("Index");
                else
                {
                    return View(medicine);
                }
            }

            return View(medicine);
        }

        // GET: Medicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Medicine medicine = bL.GetMedicine(id);

            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(new MedicineVM(medicine));
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MedicineVM medicine)
        {
            if (ModelState.IsValid)
            {
                IBL bL = new BlClass();
                bL.UpdateMedicine(medicine.Current);
                return RedirectToAction("Index");
            }
            return View(medicine);
        }

        // GET: Medicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IBL bL = new BlClass();
            Medicine medicine = bL.GetMedicine(id);

            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(new MedicineVM(medicine));
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IBL bL = new BlClass();
            bL.DeleteMedicine(id);
            return RedirectToAction("Index");
        }

        public ActionResult Chart(int? id)
        {
            string[] Months = { " ינואר", "פברואר", "מרץ", "אפריל", "מאי", "יוני", "יולי", "אוגוסט", "ספטמבר", "אוקטובר" };
            List<int> list = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            IBL bl = new BlClass();
            Medicine medicine = bl.GetMedicine(id);
            ViewBag.ChartTitle = medicine.commercialName;

            IEnumerable<Prescription> prescriptions = bl.GetPrescriptions(pre => pre.startDate > new DateTime(2020,01,01));
            var prescriptionsForMedicine = prescriptions.Where(pr => bl.GetMedicine(pr.MedicineId).commercialName == medicine.commercialName);
            foreach (var item in prescriptionsForMedicine)
            {
                list[item.startDate.Month-1]++;

            }

            return View(list);
        }

        protected override void Dispose(bool disposing)
        {
            IBL bL = new BlClass();
            bL.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
