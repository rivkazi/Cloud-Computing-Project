using BE;
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public ActionResult Log(string mail, string pass)
        {
            IBL bl = new BlClass();

            bool succeeded = bl.SignIn(mail, pass);
            if (succeeded)
            {
                Doctor doctor = bl.GetDoctors(doc => doc.email == mail).FirstOrDefault();
                RouteConfig.doctor = doctor;
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.messageError = "שם משתמש או סיסמה שגויים";
                return View("LogIn");
            }
        }

        public ActionResult SignUp(DoctorSign doctorSign)
        {
            IBL bl = new BlClass();
            Dictionary<string, string> errorMessege = bl.SignValidation(doctorSign);
            if (errorMessege.Count == 0)
            {
                bl.SignUp(doctorSign);
                return RedirectToAction("Index");
            }

            foreach (var item in errorMessege)
            {
                ModelState.AddModelError(item.Key, item.Value);
            }          
            return View("LogIn");
        }

        public ActionResult ForgotPassword(string mail)
        {
            IBL Bl = new BlClass();
            Bl.ForgotPassword(mail);
            return View("~/Views/Home/Index.cshtml");
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
