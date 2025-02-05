using MobileShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Web.Controllers
{
    public class BrandController : Controller
    {
        MobileShopDBEntities dBEntities = new MobileShopDBEntities();
        public ActionResult Index()
        {
            return View(dBEntities.Brands.ToList());
        }
        public ActionResult Details(int id)
        {
            Brand b = dBEntities.Brands.Find(id);
            List<Product> products = dBEntities.Products.Where(x => x.BrandID == id).ToList();
            ViewBag.Brand = b.BrandName;
            return View(products);
        }
        public ActionResult Edit(int id)
        {
            Brand b = dBEntities.Brands.Find(id);
            return View(b);
        }
        [HttpPost]
        public ActionResult Edit(Brand b)
        {
            dBEntities.Entry(b).State = System.Data.Entity.EntityState.Modified;
            dBEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Brand b)
        {
            dBEntities.Brands.Add(b);
            dBEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Brand b = dBEntities.Brands.Find(id);
            return View(b);
        }        
        [HttpPost]
        public ActionResult Delete(int id, string test)
        {
            dBEntities.Brands.Remove(dBEntities.Brands.Find(id));
            dBEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}