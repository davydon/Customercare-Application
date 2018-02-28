using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CustomerCareMVC.Models;
using System.Data.Entity;

namespace CustomerCareMVC.Controllers
{
    public class ProductController : Controller
    {
        private DbModel db = new DbModel();
        // GET: Product
        public ActionResult Index(string productName)
        {
            var item = from p in db.Products
                       select p;
            if (!String.IsNullOrEmpty(productName))
            {
                item = item.Where(c => c.ProductName.Contains(productName));

            }
            return View(item);
        }

        public ActionResult ListProduct(string productName)
        {
            var item = from p in db.Products
                       select p;
            if (!String.IsNullOrEmpty(productName))
            {
                item = item.Where(c => c.ProductName.Contains(productName));

            }
            return View(item);
        }



        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Products.Where(x => x.ProductId == id).FirstOrDefault());
            }
           
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Products.Add(product);
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Product added successfully";

                }
                
                return RedirectToAction("Index", "Product");
            }
            catch
            {
                return View();
            }
        }




        // GET: Product/Create
        public ActionResult New()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult New(Product product)
        {
            
                using (DbModel db = new DbModel())
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.SuccessMessage = "Product added Successfully";
                return View("New", new Product());
            
        }






        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Products.Where(x => x.ProductId == id).FirstOrDefault());
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Product updated successfully";
                }

                return RedirectToAction("Index","Product");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (DbModel db = new DbModel())
            {
                return View(db.Products.Where(x => x.ProductId == id).FirstOrDefault());
            }
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                using (DbModel db = new DbModel())
                {
                    Product product = db.Products.Where(c => c.ProductId == id).FirstOrDefault();
                    db.Products.Remove(product);
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
