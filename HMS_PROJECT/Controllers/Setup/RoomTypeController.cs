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
    public class RoomTypeController : Controller
    {
        Application_Context db = new Application_Context();
        // GET: RoomType
        public ActionResult RoomTypeIndex()
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
        public JsonResult GetRoomType()
        {
            var RoomType = db.RoomTypes.Where(x => x.Action != "D").ToList();
            return Json(RoomType, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoomTypeById(Guid id)
        {
            RoomType model = db.RoomTypes.Where(x => x.Action != "D" && x.Id == id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveDataInDatabase(RoomType model)
        {
            var result = false;
            
            try
            {
                if (model.Id == Guid.Empty)
                {
                    RoomType r = new RoomType();
                    r.Id = Guid.NewGuid();
                    r.Name = model.Name;
                    r.Capacity = model.Capacity;
                    r.Price = model.Price;
                    r.Active = model.Active;
                    r.UserInsert = Guid.Parse(Session["UserId"].ToString()); ;
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    r.InsertDate = Convert.ToDateTime(date);
                    r.Action = "A";
                    db.RoomTypes.Add(r);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    RoomType r = db.RoomTypes.SingleOrDefault(x => x.Id == model.Id);
                    r.Name = model.Name;
                    r.Capacity = model.Capacity;
                    r.Price = model.Price;
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

        public JsonResult DeleteRoomType(Guid id)
        {
            bool result = false;
            RoomType r = db.RoomTypes.SingleOrDefault(x => x.Action != "D" && x.Id == id);
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