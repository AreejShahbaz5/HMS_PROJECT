using HMS_PROJECT.App_Context;
using HMS_PROJECT.Models;
using HMS_PROJECT.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS_PROJECT.Controllers.Configuration
{
    public class AppUserController : Controller
    {
        Application_Context db = new Application_Context();
        // GET: AppUser
        public ActionResult AppUserIndex()
        {
           
            if (Session["Role"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Role"].ToString() == "Admin")
            {
                ApplicationUser ur = new ApplicationUser();
                ur.UserRolesCollection = db.UserRoles.Where(x => x.Active == true && x.Action != "D").ToList();
                ur.UserRolesCollection.OrderBy(x => x.Role);
                return View(ur);
            }
            else
            {
                return RedirectToAction("NF404", "NF404");
            }
        }

        
        public JsonResult GetUsers()
        {
            var UserList = db.ApplicationUsers.Include("UserRoles").Where(x => x.Action != "D").ToList();
            return Json(UserList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetUserById(Guid id)
        {
            ApplicationUser model = db.ApplicationUsers.Where(x => x.Action != "D"  && x.Id == id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveDataInDatabase(ApplicationUser model)
        {
            var result = false;
            try
            {
                if (model.Id == Guid.Empty)
                {
                    ApplicationUser r = new ApplicationUser();
                    r.Id = Guid.NewGuid();
                    r.FirstName = model.FirstName;
                    r.LastName = model.LastName;
                    r.UserName = model.FirstName + " " + model.LastName;
                    r.Email = model.Email;
                    r.Password = model.Password;
                    r.UserRoleId = model.UserRoleId;
                    r.PhoneNumber = model.PhoneNumber;
                    r.LockEnable = model.LockEnable;
                    r.LockEnableCount = 0;
                    r.Active = model.Active;
                    r.Action = "A";
                    r.UserInsert = model.UserInsert;
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    r.InsertDate = Convert.ToDateTime(date);
                    db.ApplicationUsers.Add(r);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    ApplicationUser r = db.ApplicationUsers.SingleOrDefault(x => x.Id == model.Id);
                    r.FirstName = model.FirstName;
                    r.LastName = model.LastName;
                    r.UserName = model.FirstName + " " + model.LastName;
                    r.Email = model.Email;
                    r.Password = model.Password;
                    r.UserRoleId = model.UserRoleId;
                    r.PhoneNumber = model.PhoneNumber;
                    r.LockEnable = model.LockEnable;
                    r.LockEnableCount = 0;
                    r.Active = model.Active;
                    r.Action = "E";
                    r.UserUpdate = model.UserUpdate;
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    r.UpdateDate = Convert.ToDateTime(date);
                    //r.UpdateDate = DateTime.Now;
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
        public JsonResult DeleteUserRecord(Guid id)
        {
            bool result = false;
            ApplicationUser r = db.ApplicationUsers.SingleOrDefault(x => x.Action != "D" && x.Id == id);
            if (r != null)
            {
                r.Action = "D";
                string date = DateTime.Now.ToString("yyyy/MM/dd");
                r.DeleteDate = Convert.ToDateTime(date);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}