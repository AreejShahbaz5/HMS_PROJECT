using HMS_PROJECT.App_Context;
using HMS_PROJECT.Models.Setup;
using HMS_PROJECT.Models.Transaction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS_PROJECT.Controllers.Transaction
{
    public class ReservationController : Controller
    {

        Application_Context db = new Application_Context();
        Random ran1 = new Random();
        Random ran2 = new Random();
        Random ran3 = new Random();

        // GET: Reservation
        public ActionResult ReservationIndex()
        {

            if (Session["Role"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Receptionist")
            {
                Reservation r = new Reservation();
                r.RoomTypeCollection = db.RoomTypes.Where(x => x.Active == true && x.Action != "D").ToList();
                r.RoomTypeCollection.OrderBy(x => x.Name);

                r.CustomerCollection = db.Customers.Where(x => x.Active == true && x.Action != "D").ToList();
                r.CustomerCollection.OrderBy(x => x.FirstName);
                return View(r);
            }
            else
            {
                return RedirectToAction("NF404", "NF404");
            }
            
        }

        public JsonResult GetReservation()
        {
            var Reservation = db.Reservations.Include("Rooms").Include("Customers").Include("Rooms.RoomTypes").ToList();
            return Json(Reservation, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetReservationById(Guid id)
        {
            Reservation model = db.Reservations.Where(x => x.Id == id).SingleOrDefault();
            var payment = db.Payments.Where(x => x.ResrvationId == id).SingleOrDefault();
            model.TotalDays = payment.TotalDays;
            model.Total_Payment = payment.Total_Payment;
            model.UpFront_Amount = payment.UpFront_Amount;
            model.Remaining_Balance = payment.Remaining_Balance;
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }


       

        public JsonResult GetRoomsByRoomType(Guid id)
        {
            var Room = db.Rooms.Where(x => x.RoomTypeId == id && x.Active == true && x.Action!="D"&& x.Status == "Available").ToList();
            return Json(Room, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoomtypeByPerson(int Num)
        {
            var Roomtype = db.RoomTypes.Where(x => x.Capacity >= Num && x.Active == true && x.Action != "D").ToList();
            return Json(Roomtype, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GeneratePayment(Guid RoomtypeId)
        {
            var Roomtype = db.RoomTypes.Where(x => x.Id == RoomtypeId).ToList();
            return Json(Roomtype, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(Reservation model)
        {
            
            var result = false;
            try
            {
                if (model.Id == Guid.Empty)
                {
                    Payment pay = new Payment();
                    Room room = db.Rooms.SingleOrDefault(x => x.Id == model.RoomId);
                    Reservation r = new Reservation();
                    r.Id = Guid.NewGuid();
                    pay.ResrvationId = r.Id;
                    pay.Id= Guid.NewGuid();
                    r.Booking_No = "RIV-"+ran1.Next(1,99)+""+ran2.Next(100,1000) + ""+ran3.Next(50,500);
                    pay.Pay_No = "P-" + ran1.Next(44, 99) + "" + ran2.Next(1, 11) + "" + ran3.Next(11, 43);
                    pay.TotalDays = model.TotalDays;
                    pay.Total_Payment = model.Total_Payment;
                    pay.UpFront_Amount = model.UpFront_Amount;
                    pay.Remaining_Balance = model.Remaining_Balance;
                    r.CheckIn = model.CheckIn;
                    r.CheckOut = model.CheckOut;
                    r.CustomerId = model.CustomerId;
                    r.Roomtype_Id = model.Roomtype_Id;
                    r.RoomId = model.RoomId;
                    r.Status = "Booking";
                    pay.Action = "A";
                    r.TotalPerson = model.TotalPerson;
                    r.BookedBy = Guid.Parse(Session["UserId"].ToString());
                    pay.UserInsert = Guid.Parse(Session["UserId"].ToString());
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    r.BookingDate = Convert.ToDateTime(date);
                    pay.InsertDate = Convert.ToDateTime(date);
                    db.Reservations.Add(r);
                    db.Payments.Add(pay);
                    db.SaveChanges();
                    room.Status = "Reserved";
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Room room = db.Rooms.SingleOrDefault(x => x.Id == model.RoomId);
                    Payment pay = db.Payments.SingleOrDefault(x => x.ResrvationId == model.Id);
                    Reservation r = db.Reservations.SingleOrDefault(x => x.Id == model.Id);
                    r.CheckIn = model.CheckIn;
                    r.CheckOut = model.CheckOut;
                    r.Roomtype_Id = model.Roomtype_Id;
                    r.RoomId = model.RoomId;
                    room.Status = "Reserved";

                    pay.TotalDays = model.TotalDays;
                    pay.Total_Payment = model.Total_Payment;
                    pay.UpFront_Amount = model.UpFront_Amount;
                    pay.Remaining_Balance = model.Remaining_Balance;
                    pay.Action = "E";

                    r.UserUpdate = Guid.Parse(Session["UserId"].ToString());
                    pay.UserUpdate = Guid.Parse(Session["UserId"].ToString());
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    r.UpdateDate = Convert.ToDateTime(date);
                    pay.UpdateDate = Convert.ToDateTime(date);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelBooking(Guid id)
        {
            bool result = false;
            Reservation r = db.Reservations.SingleOrDefault(x => x.Status != "Booked" && x.Id == id);
            if (r != null)
            {
                Room room = db.Rooms.SingleOrDefault(x => x.Id == r.RoomId);
                r.Status = "Cancel";
                room.Status = "Available";
                r.BookingCancelBy = Guid.Parse(Session["UserId"].ToString());
                string _date = DateTime.Now.ToString("yyyy/MM/dd");
                r.BookingCancelDate = Convert.ToDateTime(_date);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}