using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Authentification.Models;
using Microsoft.AspNetCore.Authorization;
using Authentification.Repositories;
using Authentification.Repositories.UserRepository;

namespace Authentification.Controllers
{
    public class HomeController : Controller
    {
        IRepository<User> db;

        public HomeController()
        {
            db = new UserRepository("Server=localhost;Port=5433;Database=projectstoredb;User Id=mushroomer; password=ytrhjvfyn1;");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(User user)
        {
            if (ModelState.IsValid)
            {
                db.AddAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        public ActionResult GetAsync(long id)
        {
            User user = (User)db.GetAsync(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.EditAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        [HttpGet]
        public ActionResult Remove(long id)
        {
            db.RemoveAsync(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Index()
        {
            return Content(User.Identity.Name);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

