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

        public ActionResult Chart()
        {
            string[] Months = { " ינואר", "פברואר", "מרץ", "אפריל", "מאי", "יוני", "יולי", "אוגוסט", "ספטמבר", "אוקטובר" };
            ViewBag.ChartTitle = "אקמול";//the name of the drug we will recieve
            List<int> list = new List<int>() { 9, 10, 20, 15, 20, 30, 5, 10, 20, 25 };


            //list of medecines, add field in the class which means the number of medicines had been added
            //update the amount with every reception
            //List<int> values = new List<int>();
            //int count = 0;
            //for (int i = 0; i < 10; i++)
            //{
            //count= bl.Count("אקמול", Months[i]);//the name of the drug we will recieve
            //values.add(count);
            //}
            //ניתן שם תרופה שרוצים להציג לבן אדם ואז עבור כל חודש הוא יפעיל את קאונט 
            //instance of bl model

            return View(list);
        }

        public ActionResult Test()
        {
            return View();
        }

    }
}
