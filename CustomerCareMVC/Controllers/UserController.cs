using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerCareMVC.Models;
using System.Data.Entity;

namespace CustomerCareMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Users.ToList());
            }
            
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Users.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                }

                return RedirectToAction("Index","User");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Users.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, User user)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("Index","User");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Users.Where(x => x.Id == id).FirstOrDefault());
            }
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    User user = db.Users.Where(c => c.Id == id).FirstOrDefault();
                    db.Users.Remove(user);
                    db.SaveChanges();
                }

                return RedirectToAction("Index","User");
            }
            catch
            {
                return View();
            }
        }



        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(User user)
        {
            using (DbModel db = new DbModel())
            {
                var details = (from userlist in db.Users
                               where userlist.Email == user.Email && userlist.Password == user.Password
                               select new
                               {

                                   userlist.Id,
                                   userlist.Email
                               }).ToList();

                if (details.FirstOrDefault() != null)
                {
                    Session["Id"] = details.FirstOrDefault().Id;
                    Session["Email"] = details.FirstOrDefault().Email;
                    return RedirectToAction("LoggedIn","User");
                }

                else
                {
                    ModelState.AddModelError("", "Email or Password is incorrect");
                }
            }
            return View(user);
        }

        public ActionResult LoggedIn()
        {
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            // or Session["register"] = null;

            return RedirectToAction("Login","User");
        }
    }
}
