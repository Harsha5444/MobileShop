﻿using MobileShop.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MobileShop.Web.Controllers
{
    public class HomeController : Controller
    {
        MobileShopDBEntities dBEntities = new MobileShopDBEntities();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User credentials)
        {
            User user = dBEntities.Users.FirstOrDefault(u => u.Username == credentials.Username);
            if (user != null)
            {
                if (user.PasswordHash == credentials.PasswordHash)
                {
                    Session["UserName"] = credentials.Username;
                    ViewBag.Message = "Login Successful";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Invalid Password";
                    ModelState.AddModelError("PasswordHash", "Please enter correct password");
                    return View();
                }
            }
            else
            {
                ViewBag.Message = "Invalid Username";
                ModelState.AddModelError("Username", "Username does not exits");
                return View();
            }
        }
        [HttpPost]
        public ActionResult Register(User user,string ConfirmPassword)
        {
            if (!ModelState.IsValid)
            {
                return View(user); 
            }
            string normalizedEmail = user.Email.Trim().ToLower();
            if (dBEntities.Users.Any(u => u.Email.ToLower() == normalizedEmail))
            {
                ModelState.AddModelError("Email", "Email is already registered");
                return View(user);
            }
            if(dBEntities.Users.Any(u => u.Username.ToLower() == user.Username.ToLower()))
            {
                ModelState.AddModelError("Username", "Username is already registered");
                return View(user);
            }
            if(user.PasswordHash != ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Password does not match");
                return View(user);
            }
            user.Email = normalizedEmail;
            user.CreatedAt = DateTime.Now;
            dBEntities.Users.Add(user);
            dBEntities.SaveChanges();
            Session["UserName"] = user.Username;
            TempData["Message"] = "User Registered Successfully";
            return RedirectToAction("Login");
        }
        public ActionResult Support()
        {
            return View();
        }
    }
}