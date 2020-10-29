using BE;
using BL;
using DrugsProject.Models.Patient;
using DrugsProject.Models.Prescription;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace DrugsProject.Controllers
{
    public class PrescriptionsController : Controller
    {

        // GET: Prescriptions
        public ActionResult Index(int? id)
        {
            IBL bl = new BlClass();
            var prescriptions = bl.GetPrescriptions(pre => pre.PatientId == id).Select(pr => new PrescriptionVM(pr));
            ViewBag.Patient = id;
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
        public ActionResult Create(int? id)
        {
            Prescription pre = new Prescription();
            pre.PatientId = (int)id;
            return View(new PrescriptionVM(pre));
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PrescriptionVM prescription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IBL bL = new BlClass();
                    prescription.DoctorId = RouteConfig.doctor != null ? RouteConfig.doctor.Id : -1;
                    bL.AddPrescription(prescription.Current);
                    ViewBag.TitlePopUp = "עבר בהצלחה";
                    ViewBag.Message = "המרשם נוסף בהצלחה";
                    var prescriptions = bL.GetPrescriptions(pre => pre.PatientId == prescription.PatientId).Select(pr => new PrescriptionVM(pr));
                    ViewBag.Patient = prescription.PatientId;
                    return View("Index", prescriptions);
                }
                return View(prescription);
            }
            catch (System.Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View(prescription);
            }

        }

        protected override void Dispose(bool disposing)
        {
            IBL bL = new BlClass();
            bL.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
