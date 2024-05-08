using Business.Models;
using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Results;
using DataAccess.Results.Bases;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IDirectorService
    {
        IQueryable<DirectorModel> Query();
        Result Add(DirectorModel model);
        Result Update(DirectorModel model);
        Result Delete(int id);
    }
    public class DirectorService : IDirectorService
    {
        private readonly Db _db;

        public DirectorService(Db db)
        {
            _db = db;
        }

        public IQueryable<DirectorModel> Query()
        {
            return _db.Directors.Include(p => p.Movies).Select(p=>new DirectorModel()
            {
                Guid = p.Guid,
                Id=p.Id,
                Name = p.Name,

                Movies = string.Join("<br/>",p.Movies.Select(m=>m.Name))
            });
        }

        public Result Add(DirectorModel model)
        {
            if (_db.Directors.Any(p => p.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Director with the same name exists!");
            Director entity = new Director()
            {
                Guid = Guid.NewGuid().ToString(),
                Name = model.Name.Trim(),
            };

            _db.Add(entity);

            _db.SaveChanges();
            return new SuccessResult();
        }

        public Result Update(DirectorModel model)   
        {
            if (_db.Directors.Any(p => p.Id != model.Id && p.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Director with the same name exists!");
            Director entity = _db.Directors.Find(model.Id);
            if (entity is null)
                return new ErrorResult("Director not found!");
            entity.Name = model.Name.Trim();

            _db.Update(entity);
            _db.SaveChanges();
            return new SuccessResult();
        }

        public Result Delete(int id)
        {
            Director entity = _db.Directors.Include(r=>r.Movies).SingleOrDefault(p=> p.Id == id);
            if (entity is null)
                return new ErrorResult("Director not found");
            if (entity.Movies is not null && entity.Movies.Any())
                return new ErrorResult("Director can't be deleted because it has relational movies");
            _db.Remove(entity);
            _db.SaveChanges();
            return new SuccessResult();
        }
    }
}
