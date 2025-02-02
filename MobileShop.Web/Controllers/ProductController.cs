using MobileShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Web.Controllers
{
    public class ProductController : Controller
    {
        MobileShopDBEntities dBEntities = new MobileShopDBEntities();
        public ActionResult Index()
        {
            return View(dBEntities.Products.ToList());
        }
        public ActionResult ProductDetails(int id)
        {
            Product productDetails = dBEntities.Products.Find(id);
            return View(productDetails);
        }
    }
}