using MobileShop.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                dBEntities.Entry(product).State = System.Data.Entity.EntityState.Modified;
                dBEntities.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public ActionResult Create()
        {
            var viewModel = new ProductEditViewModel
            {
                categories = dBEntities.Categories.ToList(),
                brands = dBEntities.Brands.ToList(),
                product = new Product()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductEditViewModel viewModel, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Images/"), Path.GetFileName(ImageFile.FileName));
                    ImageFile.SaveAs(path);
                    viewModel.product.ImagePath = "/Images/" + ImageFile.FileName; // Store relative path
                }

                dBEntities.Products.Add(viewModel.product);
                dBEntities.SaveChanges();
                return RedirectToAction("Index");
            }

            // Reload dropdown lists if validation fails
            viewModel.categories = dBEntities.Categories.ToList();
            viewModel.brands = dBEntities.Brands.ToList();

            return View(viewModel);
        }

        public ActionResult Delete(int id)
        {
            var product = dBEntities.Products.FirstOrDefault(p => p.ProductID == id);
            dBEntities.Products.Remove(product);
            dBEntities.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}