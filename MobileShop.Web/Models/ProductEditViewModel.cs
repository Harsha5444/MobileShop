using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileShop.Web.Models
{
    public class ProductEditViewModel
    {
        public Product product { get; set; }
        public List<Category> categories { get; set; }
        public List<Brand> brands { get; set; }
    }
}