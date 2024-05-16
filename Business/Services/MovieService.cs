using Business.Models;
using Business.Services.Bases;
using DataAccess.Context;
using DataAccess.Results.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace Business.Services
{
    public interface IMovieService
    {
        IQueryable<MovieModel> Query();
        Result Add(MovieModel movie);
        Result Update(MovieModel movie);
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
            return _db.Movies.OrderByDescending(m => m.ReleaseDate).ThenByDescending(m => m.BoxOffice).ThenBy(m => m.Name).Select(m => new MovieModel()
            {
                Guid = m.Guid,
                Id = m.Id,
                Name = m.Name,
                ReleaseDate = m.ReleaseDate,
                Country = m.Country,
                Length = m.Length,
                DirectorId = m.DirectorId,
                BoxOffice = m.BoxOffice,
            });
        }

        Result IMovieService.Add(MovieModel movie)
        {
            throw new NotImplementedException();
        }

        Result IMovieService.Delete(int id)
        {
            throw new NotImplementedException();
        }

        Result IMovieService.Update(MovieModel movie)
        {
            throw new NotImplementedException();
        }
    }
}
