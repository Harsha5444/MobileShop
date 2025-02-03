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
        public ActionResult Edit(int id)
        {
           
            var product = dBEntities.Products.FirstOrDefault(p => p.ProductID == id);

            // Get all categories for the dropdown
            var categories = dBEntities.Categories.ToList();
            var brands = dBEntities.Brands.ToList();

            // Create a ViewModel (you can also use the Product model directly if it's already set up)
            var viewModel = new ProductEditViewModel
            {
                product = product,
                categories = categories, // Populate the categories
                brands = brands
            };

            return View(viewModel);
        }
    }
}