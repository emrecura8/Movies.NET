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
    public class DirectorsController : MVCControllerBase
    {
        // TODO: Add service injections here
        private readonly IDirectorService _directorService;

        public DirectorsController(IDirectorService directorService)
        {
            _directorService = directorService;
        }

        // GET: Directors
        public IActionResult Index()
        {
            List<DirectorModel> directorList = _directorService.Query().OrderBy(p=>p.Name).ToList(); // TODO: Add get collection service logic here
            return View(directorList);
        }

        // GET: Directors/Details/5
        public IActionResult Details(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(p=>p.Id==id); // TODO: Add get item service logic here
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                Result result = _directorService.Add(director);
                if(result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("",result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(director);
        }

        // GET: Directors/Edit/5
        public IActionResult Edit(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(p=>p.Id==id); // TODO: Add get item service logic here
            if (director == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(director);
        }

        // POST: Directors/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DirectorModel director)
        {
            if (ModelState.IsValid)
            {
                Result result = _directorService.Update(director);
                if(result.IsSuccessful)
                    return RedirectToAction(nameof(Index));
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(director);
        }

        // GET: Directors/Delete/5
        public IActionResult Delete(int id)
        {
            DirectorModel director = _directorService.Query().SingleOrDefault(p=>p.Id==id); // TODO: Add get item service logic here
            if (director == null)
            {
                return NotFound();
            }
            return View(director);
        }

        // POST: Directors/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Result result = _directorService.Delete(id);
            TempData["Message"]=result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
