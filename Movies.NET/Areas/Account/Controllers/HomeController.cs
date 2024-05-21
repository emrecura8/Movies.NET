﻿using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using DataAccess.Enums;

namespace Movies.NET.Areas.Account.Controllers
{
    [Area("Account")]
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserModel user)
        {
            ModelState.Remove(nameof(user.RoleId));
            if(ModelState.IsValid)
            {
                var existingUser = _userService.Query().SingleOrDefault(u=>u.UserName == user.UserName && u.Password == u.Password && u.isActive);
                if(existingUser != null)
                {
                    ModelState.AddModelError("", "Invalid user name and password!");
                    return View(user);
                }
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, existingUser.UserName),
                    new Claim(ClaimTypes.Role, existingUser.RoleOutput.Name)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home", new {area=""});
            }
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        public IActionResult AccessDenied()
        {
            return View("Error","You don't have access to this resource");
        }

        public IActionResult Register()
        {
            return null;
        }

        [HttpPost]
        public IActionResult Register(UserModel user)
        {
            ModelState.Remove(nameof(user.RoleId));
            if (ModelState.IsValid)
            {
                user.Status = Statuses.Normal;
                user.isActive = true;
                user.RoleId = (int)Roles.User;
                var result = _userService.Add(user);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Login));
                ModelState.AddModelError("", result.Message);
            }
            return View();
        }

        #region Extra(Retrieving user data in JSON format)
        public IActionResult GetUsers()
        {
            return Json(_userService.GetList());
        }
        #endregion
    }
}
