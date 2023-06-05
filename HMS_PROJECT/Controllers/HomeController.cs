using HMS_PROJECT.App_Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        Application_Context db = new Application_Context();
        public ActionResult Index()
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

        public JsonResult GetRoomsInfo()
        {
            var Room = db.Rooms.Include("RoomTypes").Where(x => x.Action != "D").ToList();
            return Json(Room, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }

    }
}