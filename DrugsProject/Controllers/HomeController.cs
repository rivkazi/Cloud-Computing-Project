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

        public ActionResult Log(string usName, string pass)
        {
            IBL bl = new BlClass();
            //var usName = Request["usName"].ToString();
            //var pass = Request["pass"].ToString();

            bool succeeded = bl.SignIn(usName, pass);
            if (succeeded)
            {
                Doctor doctor = bl.GetDoctors(doc => doc.userName == usName).FirstOrDefault();
                RouteConfig.doctor = doctor;
                return View("~/Views/Home/Index.cshtml");
            }
            else
            {
                ViewBag.messageError = "שם משתמש או סיסמה שגויים";
                return View("LogIn");
            }
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
