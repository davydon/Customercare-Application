using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerCareMVC.Models;

namespace CustomerCareMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            using (DbModel db = new DbModel())
            {
                var details = (from adminList in db.Admins
                               where adminList.Username == admin.Username && adminList.Password == admin.Password
                               select new
                               {
                                   adminList.Id,
                                   adminList.Username

                               }).ToList();
                if(details.FirstOrDefault() != null)
                {
                    Session["Id"] = details.FirstOrDefault().Id;
                    Session["Username"] = details.FirstOrDefault().Username;
                    return RedirectToAction("Admin", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                }
                               
            }
            return View(admin);
        }



        public ActionResult SideMenu()
        {
            return PartialView("SideMenu");
        }


        public ActionResult UserMenu()
        {
            return PartialView("UserMenu");
        }

        public ActionResult Admin()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            // or Session["register"] = null;

            return RedirectToAction("Login", "Admin");
        }
    }
}