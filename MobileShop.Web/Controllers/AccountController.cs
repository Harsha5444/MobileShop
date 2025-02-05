using MobileShop.Web.Models;
using System.Linq;
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
        public ActionResult EditProfile(int id)
        {
            var user = dBEntities.Users.FirstOrDefault(u => u.UserID == id);
            return View(user);
        }
        [HttpPost]
        public ActionResult EditProfile(User user)
        {
            if (ModelState.IsValid)
            {
                dBEntities.Entry(user).State = System.Data.Entity.EntityState.Modified;
                dBEntities.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
            return View(user);
        }
    }
}