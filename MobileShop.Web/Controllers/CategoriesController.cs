using MobileShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Web.Controllers
{
    public class CategoriesController : Controller
    {
        MobileShopDBEntities dBEntities = new MobileShopDBEntities();
        public ActionResult Index()
        {
            return View(dBEntities.Categories.ToList());
        }
        public ActionResult Details(int id)
        {
            Category c = dBEntities.Categories.Find(id);
            List<Product> products = dBEntities.Products.Where(x => x.CategoryID == id).ToList();
            ViewBag.Category = c.CategoryName;
            return View(products);
        }
        public ActionResult Edit(int id)
        {
            Category c = dBEntities.Categories.Find(id);
            return View(c);
        }
        [HttpPost]
        public ActionResult Edit(Category c)
        {
            dBEntities.Entry(c).State = EntityState.Modified;
            dBEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category c)
        {
            dBEntities.Categories.Add(c);
            dBEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            Category c = dBEntities.Categories.Find(id);
            return View(c);
        }
        [HttpPost]
        public RedirectToRouteResult Delete(int id,string test)
        {
            dBEntities.Categories.Remove(dBEntities.Categories.Find(id));
            dBEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}