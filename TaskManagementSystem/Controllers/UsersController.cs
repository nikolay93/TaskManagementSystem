using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementModule;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class UsersController : BaseController
    {
        // list all users
        public ActionResult Index()
        {
            var users = db.Users.ToList();
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }

        // Post action for creating a user in the DB
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    Name = userModel.Name,
                    Email = userModel.Email
                };
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userModel);
        }

        // Edit GET action for fetching the data for specific user from the DB
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            UserViewModel userVM = new UserViewModel()
            {
                Name = user.Name,
                Email = user.Email
            };
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(userVM);
        }

        // Post action for saving the new data for a specific user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userVM)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.Find(userVM.Id);
                if (user == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                user.Name = userVM.Name;
                user.Email = userVM.Email;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userVM);
        }

        // Delete user from DB
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}