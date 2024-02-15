using Business.Models;
using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IRoleService
    {
        IQueryable<RoleModel> Query();
    }

    public class RoleService : IRoleService
    {
        private readonly Db _db;

        public RoleService(Db db)
        {
            _db = db;
        }


        public IQueryable<RoleModel> Query()
        {
            return _db.Roles.Select(roleEntity => new RoleModel()
            {
                MovieId = roleEntity.MovieId,
                Id = roleEntity.Id,
                Name = roleEntity.Name,
            });
        }
    };
}
