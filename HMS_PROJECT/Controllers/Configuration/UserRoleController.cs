using HMS_PROJECT.App_Context;
using HMS_PROJECT.Models;
using HMS_PROJECT.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace HMS_PROJECT.Controllers.Configuration
{
    public class UserRoleController : Controller
    {
        //Context
        Application_Context db = new Application_Context();
        // GET: UserRole
        public ActionResult UserRoleIndex()
        {
            if (Session["Role"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (Session["Role"].ToString() == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("NF404", "NF404");
            }
           
        }

        public JsonResult GetUserRole()
        {
            //List<UserRole> UserRoleList = new List<UserRole>();
            //UserRoleList = db.UserRoles.Select(x => new UserRoleViewModel
            //{
            //    ID = x.ID,
            //    Role = x.Role,
            //    Active = x.Active
            var UserRoleList = db.UserRoles.Where(x => x.Action != "D").ToList();
            return Json(UserRoleList, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetRoleById(Guid id)
        {
            UserRole model = db.UserRoles.Where(x => x.Action !="D" && x.ID == id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }


        public JsonResult SaveDataInDatabase(UserRole model)
        {
            var result = false;
            try
            {
                if (model.ID == Guid.Empty)
                {
                    UserRole r = new UserRole();
                    r.ID = Guid.NewGuid();
                    r.Role = model.Role;
                    r.Active = model.Active;
                    r.Action = "A";
                    db.UserRoles.Add(r);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    UserRole r = db.UserRoles.SingleOrDefault(x => x.ID == model.ID);
                    r.Role = model.Role;
                    r.Active = model.Active;
                    r.Action = "E";
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

        public JsonResult DeleteRoleRecord(Guid id)
        {
            bool result = false;
            UserRole r = db.UserRoles.SingleOrDefault(x => x.Action != "D" && x.ID == id);
            if (r != null)
            {
                r.Action = "D";
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
    
}