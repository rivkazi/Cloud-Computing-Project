using BE;
using BL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace DrugsProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult Log(DoctorSign doctorSign)
        {
            try
            {
                IBL bl = new BlClass();
                Dictionary<string, string> errorMessege = bl.SignValidation(doctorSign);
                if (errorMessege.Count == 0)
                {
                    bl.SignIn(doctorSign);
                    Doctor doctor = bl.GetDoctors(doc => doc.email == doctorSign.email).FirstOrDefault();
                    RouteConfig.doctor = doctor;
                    return View("~/Views/Home/Index.cshtml");
                }

                foreach (var item in errorMessege)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                ViewBag.SigndivActive = "true";
                return View("LogIn");
            }
            catch (System.Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                ViewBag.SigndivActive = "true";
                return View("LogIn");
            }     
        }

        public ActionResult SignUp(DoctorSign doctorSign)
        {
            try
            {
                IBL bl = new BlClass();
                Dictionary<string, string> errorMessege = bl.SignValidation(doctorSign);
                if (errorMessege.Count == 0)
                {
                    bl.SignUp(doctorSign);
                    ViewBag.TitlePopUp = "עבר בהצלחה";
                    ViewBag.Message = "ההרשמה עברה בהצלחה, אישור נוסף תמצא בתיבת המייל האישית שלך.";
                    return View("~/Views/Home/Index.cshtml");
                }

                foreach (var item in errorMessege)
                {
                    ModelState.AddModelError(item.Key, item.Value);
                }
                return View("LogIn");
            }
            catch (System.Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public ActionResult ForgotPassword(string mail)
        {
            try
            {
                IBL Bl = new BlClass();
                Bl.ForgotPassword(mail);
                ViewBag.TitlePopUp = "איפוס סיסמה";
                ViewBag.Message = "ממתינה לך סיסמה חדשה בתיבת המייל איתו נרשמת למערכת";
                return View("~/Views/Home/Index.cshtml");
            }
            catch (System.Exception ex)
            {
                ViewBag.TitlePopUp = "שגיאה";
                ViewBag.Message = ex.Message;
                return View("~/Views/Home/Index.cshtml");
            }

        }

        public ActionResult LogOut()
        {
            RouteConfig.doctor = null;
            return View("~/Views/Home/Index.cshtml");
        }



        public ActionResult Test()
        {
          
            return View();
        }

    }
}
