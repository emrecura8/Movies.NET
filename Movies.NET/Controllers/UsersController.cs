#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Context;
using DataAccess.Entities;
using Business.Services;
using Business.Models;
using DataAccess.Results.Bases;
using Movies.NET.Controllers.Bases;

//Generated from Custom Template.
namespace Movies.NET.Controllers
{
    public class UsersController : MVCControllerBase
    {
        // TODO: Add service injections here
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public UsersController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        // GET: Users
        public IActionResult Index()
        {
            List<UserModel> userList = _userService.GetList(); // TODO: Add get collection service logic here
            return View(userList);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            UserModel user = _userService.GetItem(id); // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                Result result = _userService.Add(user);
                if(result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                        return RedirectToAction(nameof(Details), new {id=user.Id});
                }
                ModelState.AddModelError("",result.Message);
            }

            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            UserModel user = _userService.GetItem(id); // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            return View(user);
        }

        // POST: Users/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                Result result = _userService.Update(user);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new {id=user.Id});
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {
            UserModel user = _userService.GetItem(id); // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _userService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
