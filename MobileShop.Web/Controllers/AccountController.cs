using MobileShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MobileShop.Web.Controllers
{
    public class AccountController : Controller
    {
        MobileShopDBEntities dBEntities = new MobileShopDBEntities();
        public ActionResult Orders()
        {
            return View();
        }
        public new ActionResult Profile()
        {
            string username = Session["UserName"] as string;
            User userdetails = dBEntities.Users.SingleOrDefault(u => u.Username == username);
            return View(userdetails);
        }

    }
}