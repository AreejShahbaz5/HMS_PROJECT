using HMS_PROJECT.App_Context;
using HMS_PROJECT.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS_PROJECT.Controllers
{
    public class PaymentController : Controller
    {
        Application_Context db = new Application_Context();

        // GET: Payment
        public ActionResult PaymentIndex()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Receptionist")
            {
                return View();
            }
            else
            {
                return RedirectToAction("NF404", "NF404");
            }
            
        }

        public JsonResult Getpayrecord()
        {
            var payrecord = db.Payments.Include("Reservations").ToList();
            return Json(payrecord, JsonRequestBehavior.AllowGet);
        }





    }
}