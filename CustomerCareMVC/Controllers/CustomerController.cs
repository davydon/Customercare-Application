using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerCareMVC.Models;
using System.Data.Entity;

namespace CustomerCareMVC.Controllers
{
    public class CustomerController : Controller
    {
        private DbModel db = new DbModel();
        // GET: Customer
        public ActionResult Index(string name)
        {
            var item = from n in db.Customers
                       select n;
            if (!String.IsNullOrEmpty(name))
            {
                item = item.Where(c => c.Name.Contains(name));
            }
            return View(item);
        }


        public ActionResult ListCustomer(string name)
        {
            var item = from n in db.Customers
                       select n;
            if (!String.IsNullOrEmpty(name))
            {
                item = item.Where(c => c.Name.Contains(name));
            }
            return View(item);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Customers.Where(x => x.CustomerId ==id).FirstOrDefault());
            }
            
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }

                return RedirectToAction("Index","Customer");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Customers.Where(x => x.CustomerId == id).FirstOrDefault());
            }
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Customer customer)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Entry(customer).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index","Customer");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Customers.Where(x => x.CustomerId == id).FirstOrDefault());
            }
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                   Customer customer = db.Customers.Where(c => c.CustomerId == id).FirstOrDefault();
                    db.Customers.Remove(customer);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
