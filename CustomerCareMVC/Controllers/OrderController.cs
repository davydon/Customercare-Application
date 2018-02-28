using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerCareMVC.Models;
using System.Data.Entity;

namespace CustomerCareMVC.Controllers
{
    public class OrderController : Controller
    {
        private DbModel db = new DbModel();
        // GET: Order
        public ActionResult Index(string customerName)
        {
            var item = from c in db.Orders
                       select c;
            if (!String.IsNullOrEmpty(customerName))         
            {
                item = item.Where(c => c.CustomerName.Contains(customerName));
            }
            return View(item);
        }

        public ActionResult ListOrder(string customerName)
        {
            var item = from c in db.Orders
                       select c;
            if (!String.IsNullOrEmpty(customerName))
            {
                item = item.Where(c => c.CustomerName.Contains(customerName));
            }
            return View(item);
        }



        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Orders.Where(x => x.OrderId == id).FirstOrDefault());
            }
            
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(Order order)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Orders.Add(order);
                    db.SaveChanges();
                }
                return RedirectToAction("Index","Order");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Orders.Where(x => x.OrderId == id).FirstOrDefault());
            }
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Entry(order).State = EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index","Order");
            }
            catch
            {
                return View();
            }
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Orders.Where(x => x.OrderId == id).FirstOrDefault());
            }
        }

        // POST: Order/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    Order order = db.Orders.Where(c => c.OrderId == id).FirstOrDefault();
                    db.Orders.Remove(order);
                    db.SaveChanges();
                }

                return RedirectToAction("Index","Order");
            }
            catch
            {
                return View();
            }
        }
    }
}
