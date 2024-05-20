﻿#nullable disable
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
using Movies.NET.Controllers.Bases;

//Generated from Custom Template.
namespace Movies.NET.Controllers
{
    public class MoviesController : MVCControllerBase
    {
        // TODO: Add service injections here
        private readonly IMovieService _movieService;
        private readonly IDirectorService _directorService;
        private readonly IUserService _userService;


        public MoviesController(IMovieService movieService, IDirectorService directorService, IUserService userService)
        {
            _movieService = movieService;
            _directorService = directorService;
            _userService = userService;
        }

        // GET: Movies
        public IActionResult Index()
        {
            List<MovieModel> movieList = _movieService.GetList(); // TODO: Add get collection service logic here
            return View("List",movieList);
        }

        // GET: Movies/Details/5
        public IActionResult Details(int id)
        {
            MovieModel movie = _movieService.GetItem(id); // TODO: Add get item service logic here
            if (movie == null)
            {
                return View("Error",$"Movie with {id} not found");
            }
            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                var result = _movieService.Add(movie);
                if (result.IsSuccessful)
                    return RedirectToAction(nameof(Details), new {id=movie.Id});
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id","Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(movie);
        }

        // GET: Movies/Edit/5
        public IActionResult Edit(int id)
        {
            MovieModel movie = _movieService.GetItem(id); // TODO: Add get item service logic here
            if (movie == null)
            {
                return View("Error",$"Game with ID {id} not found!");
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(movie);
        }

        // POST: Movies/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MovieModel movie)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                var result = _movieService.Update(movie);
                if(result.IsSuccessful) return RedirectToAction(nameof(Details),new {id=movie.Id});
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["DirectorId"] = new SelectList(_directorService.Query().ToList(), "Id", "Name");
            ViewBag.Users = new MultiSelectList(_userService.GetList(), "Id", "UserName");
            return View(movie);
        }

        // GET: Movies/Delete/5
        public IActionResult Delete(int id)
        {
            MovieModel movie = null; // TODO: Add get item service logic here
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            return RedirectToAction(nameof(Index));
        }
	}
}
