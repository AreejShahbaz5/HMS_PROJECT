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
    public class CustomerController : Controller
    {
        Application_Context db = new Application_Context();
        // GET: Customer
        public ActionResult CustomerIndex()
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

        public JsonResult GetCustomer()
        {
            var customer = db.Customers.Where(x => x.Action != "D").ToList();
            return Json(customer, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCustomerById(Guid id)
        {
            Customer model = db.Customers.Where(x => x.Action != "D" && x.Id == id).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(Customer model)
        {
            var result = false;

            try
            {
                if (model.Id == Guid.Empty)
                {
                    Customer cus = new Customer();
                    cus.Id = Guid.NewGuid();
                    cus.FirstName = model.FirstName;
                    cus.LastName = model.LastName;
                    cus.FullName = model.FirstName +" "+ model.LastName;
                    cus.Address = model.Address;
                    cus.CNIC = model.CNIC;
                    cus.Email = model.Email;
                    cus.Contact = model.Contact;
                    cus.AnotherContact = model.AnotherContact;
                    cus.Active = model.Active;
                    cus.UserInsert = Guid.Parse(Session["UserId"].ToString()); 
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    cus.InsertDate = Convert.ToDateTime(date);
                    cus.Action = "A";
                    db.Customers.Add(cus);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Customer cus = db.Customers.SingleOrDefault(x => x.Id == model.Id);
                    cus.FirstName = model.FirstName;
                    cus.LastName = model.LastName;
                    cus.FullName = model.FirstName + " " + model.LastName;
                    cus.Address = model.Address;
                    cus.CNIC = model.CNIC;
                    cus.Email = model.Email;
                    cus.Contact = model.Contact;
                    cus.AnotherContact = model.AnotherContact;
                    cus.Active = model.Active;
                    cus.Action = "E";
                    cus.UserUpdate = Guid.Parse(Session["UserId"].ToString());
                    string date = DateTime.Now.ToString("yyyy/MM/dd");
                    cus.UpdateDate = Convert.ToDateTime(date);
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

        public JsonResult DeleteCustomer(Guid id)
        {
            bool result = false;
            Customer cus = db.Customers.SingleOrDefault(x => x.Action != "D" && x.Id == id);
            if (cus != null)
            {
                cus.Action = "D";
                cus.UserDelete = Guid.Parse(Session["UserId"].ToString());
                string date = DateTime.Now.ToString("yyyy/MM/dd");
                cus.DeleteDate = Convert.ToDateTime(date);
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}