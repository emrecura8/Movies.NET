using Business.Models;
using Business.Services.Bases;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

#nullable disable

namespace Business.Services
{
    public interface IMovieService
    {
        IQueryable<MovieModel> Query();
        Result Add(MovieModel model);
        Result Update(MovieModel model);
        Result Delete(int id);
        List<MovieModel> GetList() => Query().ToList();
        MovieModel GetItem(int id) => Query().SingleOrDefault(m => m.Id == id);
    }

    public class MovieService : ServiceBase, IMovieService
    {
        public MovieService(Db db) : base(db)
        {
        }

        IQueryable<MovieModel> IMovieService.Query()
        {
            return _db.Movies.Include(m=>m.Director).Include(m=>m.UserMovies).ThenInclude(um=>um.User).
                OrderByDescending(m => m.ReleaseDate).ThenByDescending(m => m.BoxOffice).ThenBy(m => m.Name).
                Select(m => new MovieModel()
            {
                Guid = m.Guid,
                Id = m.Id,
                Name = m.Name,
                ReleaseDate = m.ReleaseDate,
                Country = m.Country,
                Length = m.Length,
                DirectorId = m.DirectorId,
                BoxOffice = m.BoxOffice,

                DirectorOutput=m.Director.Name,
                ReleaseDateOutput=m.ReleaseDate.HasValue ? m.ReleaseDate.Value.ToString("MM/dd/yyyy"):string.Empty,

                Users = m.UserMovies.Select(um=>new UserModel()
                {
                    UserName=um.User.UserName,
                    Status=um.User.Status
                }).ToList(),

                UsersInput = m.UserMovies.Select(um=>um.UserId).ToList(),

                TotalBoxOfficeOutput=(m.BoxOffice??0).ToString("C2"),
                LengthOutput=m.Length.ToString(),
				CountryOutput=m.Country.ToString(),
                ImdbRateOutput=m.ImdbRate.ToString(1 == 0 ? "0":"0.0"),

               
                
            });
        }

        Result IMovieService.Add(MovieModel model)
        {
            if (_db.Movies.Any(m => m.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Movie with the same name exists.");
            Movie entity = new Movie()
            {
                Name = model.Name.Trim(),
                ReleaseDate = model.ReleaseDate,
                Country = model.Country,
                Length = model.Length,
                DirectorId = (int)model.DirectorId,
                BoxOffice = model.BoxOffice,
                ImdbRate = model.ImdbRate,

                UserMovies = model.UsersInput?.Select(userInput => new UserMovie()
                {
                    UserId = userInput
                }).ToList(),
            };

            _db.Movies.Add(entity);
            _db.SaveChanges();
            
            model.Id =entity.Id;
            return new SuccessResult();
        }

        Result IMovieService.Delete(int id)
        {
           Movie entity = _db.Movies.Include(m=>m.UserMovies).SingleOrDefault(m=>m.Id==id);
            if (entity is null) return new ErrorResult("Movie not found");
            _db.UserMovies.RemoveRange(entity.UserMovies);

            _db.Movies.Remove(entity);
            _db.SaveChanges();

            return new SuccessResult("Movie deleted succesfully");
        }

        Result IMovieService.Update(MovieModel model)
        {
            if (_db.Movies.Any(m => m.Id != model.Id && m.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Movie with same name exists");

            Movie entity = _db.Movies.Include(m=>m.UserMovies).SingleOrDefault(m=>m.Id == model.Id);

            if (entity == null) return new ErrorResult("Movie not found");

            _db.UserMovies.RemoveRange(entity.UserMovies);

            entity.Name = model.Name.Trim();
            entity.ReleaseDate = model.ReleaseDate;
            entity.Country = model.Country;
            entity.Length = model.Length;
            entity.DirectorId = (int)model.DirectorId;
            entity.BoxOffice = model.BoxOffice;
            entity.ImdbRate = model.ImdbRate;
      
            entity.UserMovies = model.UsersInput?.Select(userInput => new UserMovie()
            {
                UserId=userInput
            }).ToList();

            _db.Movies.Update(entity);
            _db.SaveChanges();

            return new SuccessResult();
        }
    }
}
