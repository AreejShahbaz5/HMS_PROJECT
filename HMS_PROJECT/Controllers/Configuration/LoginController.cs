using HMS_PROJECT.App_Context;
using HMS_PROJECT.Models;
using HMS_PROJECT.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS_PROJECT.Controllers.Configuration
{
    public class LoginController : Controller
    {
        Application_Context db = new Application_Context();
        ApplicationUser au = new ApplicationUser();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        //CheckUser
        public JsonResult CheckUser(LoginViewModel model)
        {
            var result = false;
            try
            {
                var ValidUser = db.ApplicationUsers.Include("UserRoles").Where(x => x.Email == model.Email && x.Password == model.Password && x.Active == true && x.LockEnable == false).SingleOrDefault();
                if (ValidUser != null)
                {
                    Session["UserId"] = ValidUser.Id;
                    Session["Role"] = ValidUser.UserRoles.Role;
                    Session["UserName"] = ValidUser.UserName;
                    Session.Timeout = 20;
                    result = true;
                }   
            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}