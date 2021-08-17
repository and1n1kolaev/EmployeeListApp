using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeListApp.DAL.Interfaces;
using EmployeeListApp.WEB.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using X.PagedList;

namespace EmployeeListApp.WEB.Controllers
{
    namespace AuthApp.Controllers
    {
        public class AccountController : Controller
        {
            private readonly ILogger<AccountController> _logger;
            private readonly IToastifyService _notifyService;
            private readonly IUnitOfWork _uow;
            public AccountController(ILogger<AccountController> logger, IUnitOfWork uow, IToastifyService notifyService)
            {
                _notifyService = notifyService;
                _logger = logger;
                _uow = uow;
            }
            [HttpGet]
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginViewModel model)
            {
                if (ModelState.IsValid)
                {
                    var user = await _uow.UserRepository.GetByLoginAsync(model.Login);
                    var currentPasswordHash = HashService.GetHash(model.Password);

                    if (user != null && HashService.HashCompare(user.PasswordHash, currentPasswordHash)) 
                    {
                        await Authenticate(model.Login); 
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                return View(model);
            }

            [HttpGet]
            public IActionResult Add()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Add(AddUserViewModel model)
            {
                if (ModelState.IsValid)
                {
                  var user = await _uow.UserRepository.GetByLoginAsync(model.Login);
                    if (user == null)
                    {
                        await _uow.UserRepository.AddAsync(new DAL.Entities.User() 
                        { 
                            Login = model.Login,
                            PasswordHash = HashService.GetHash(model.Password),
                            LastActivityTime = DateTime.Now,                       
                        });

                        return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                return View(model);
            }

            private async Task Authenticate(string login)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, login)
                };

                ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
            }

            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction("Login", "Account");
            }
            protected override void Dispose(bool disposing)
            {
                _uow.Dispose();
                base.Dispose(disposing);
            }

        }
    }
}
