using HMS_PROJECT.App_Context;
using HMS_PROJECT.Models.Setup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS_PROJECT.Controllers.Setup
{
    public class RoomController : Controller
    {
        Application_Context db = new Application_Context();
        // GET: Room
        public ActionResult RoomIndex()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Role"].ToString() == "Admin" || Session["Role"].ToString() == "Receptionist")
            {
                Room r = new Room();
                r.RoomTypeCollection = db.RoomTypes.Where(x => x.Active == true && x.Action != "D").ToList();
                r.RoomTypeCollection.OrderBy(x => x.Name);
                return View(r);
            }
            else
            {
                return RedirectToAction("NF404", "NF404");
            }
           
        }

        public JsonResult GetRooms()
        {
            var Room = db.Rooms.Include("RoomTypes").Where(x => x.Action != "D").ToList();
            return Json(Room, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoomById(Guid id)
        {
            Room model = db.Rooms.Where(x => x.Action != "D" && x.Id == id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveDataInDatabase(Room model)
        {
            var result = false;

            try
            {
                if (model.Id == Guid.Empty)
                {
                    Room r = new Room();
                    r.Id = Guid.NewGuid();
                    r.Room_No = model.Room_No;
                    r.RoomTypeId = model.RoomTypeId;
                    r.Status = model.Status;
                    r.Description = model.Description;
                    r.Floor = model.Floor;
                    r.Active = model.Active;
                    r.Clean = model.Clean;
                    r.UserInsert = Guid.Parse(Session["UserId"].ToString()); ;
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    r.InsertDate = Convert.ToDateTime(date);
                    r.Action = "A";
                    db.Rooms.Add(r);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Room r = db.Rooms.SingleOrDefault(x => x.Id == model.Id);
                    r.Room_No = model.Room_No;
                    r.RoomTypeId = model.RoomTypeId;
                    r.Status = model.Status;
                    r.Description = model.Description;
                    r.Floor = model.Floor;
                    r.Clean = model.Clean;
                    r.Active = model.Active;
                    r.Action = "E";
                    r.UserUpdate = Guid.Parse(Session["UserId"].ToString()); ;
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    r.UpdateDate = Convert.ToDateTime(date);
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

        public JsonResult DeleteRoom(Guid id)
        {
            bool result = false;
            Room r = db.Rooms.SingleOrDefault(x => x.Action != "D" && x.Id == id);
            if (r != null)
            {
                r.Action = "D";
                r.UserDelete = Guid.Parse(Session["UserId"].ToString());
                string date = DateTime.Now.ToString("yyyy/MM/dd");
                r.DeleteDate = Convert.ToDateTime(date);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}