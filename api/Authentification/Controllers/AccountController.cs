using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Authentification.Models;
using Microsoft.AspNetCore.Mvc;
using Authentification.Repositories.UserRepository;
using Authentification.ViewModels;

namespace Authentification.Controllers
{
    public class AccountController : Controller
    {//подключение к бд
        private UserRepository userRepository = new UserRepository("Server=localhost;Port=5433;Database=projectstoredb;User Id=mushroomer; password=ytrhjvfyn1;");

        //переход к странице с логином
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //авторизация
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { email = model.Email, password = model.Password };
                //приоверка на полученные данные из модели логина
                if (userRepository.isUserExists(user))
                {
                    await Authenticate(model.Email); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Not correct login/password");
            }
            return View(model);
        }


        //переход к окну с регистрацией
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ViewModels.RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { email = model.Email, password = model.Password};
                if (!userRepository.isUserExists(user))
                {
                    // добавляем пользователя в бд
                    userRepository.AddAsync(new User { email = model.Email, password = model.Password});
                    await Authenticate(model.Email); // аутентификация
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login already exists!");
                }

            }

            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}