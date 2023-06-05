using HMS_PROJECT.App_Context;
using HMS_PROJECT.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS_PROJECT.Controllers.Configuration
{
    
    public class BackUPController : Controller
    {
        SqlCommand cmd;
        SqlConnection con;
        Application_Context db = new Application_Context();
        string dbName = "HMS_DB";
        // GET: BackUP
        public ActionResult BackUPIndex()
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

        public JsonResult GetBackupData()
        {
            var backuplist = db.Back_UP.Include("ApplicationUsers").ToList();
            return Json(backuplist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(BackUP model)
        {
            var result = false;
            var role = Session["Role"].ToString();
                try
            {
                if (BACKUP() == true)
                {
                    BackUP _backup = new BackUP();
                    _backup.ID = Guid.NewGuid();
                    _backup.Name = dbName;
                    string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                    _backup.Date = Convert.ToDateTime(date);
                    string sessioid = Session["UserId"].ToString();
                    _backup.UserId = Guid.Parse(sessioid);
                    db.Back_UP.Add(_backup);
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public bool BACKUP() {
            try
            {
                DateTime _date = DateTime.Now;
                string date = _date.Date + "-" + _date.Month + "-" + _date.Year;
                string Directory = Server.MapPath("/DBBackup/");
                con = new SqlConnection(@"Data Source=ADEEL\ADEEL;Initial Catalog=HMS_DB;Integrated Security=True");
                string usestr = "Backup database " + dbName + " to disk='" + Directory + dbName + ".bak' WITH FORMAT, MEDIANAME = 'SQLServerBackups', NAME ='" + dbName + "'";
                query(usestr);
                return true;
                
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void query(string que)
        {
            // ERROR: Not supported in C#: OnErrorStatement
            con.Open();
            cmd = new SqlCommand(que, con);
            cmd.ExecuteNonQuery();
            con.Close();

        }
    }
}