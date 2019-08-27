using CommonCore.Interfaces;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Core3_Framework.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Core3_Framework.Business.Services
{
    public class RoleService :ServiceBase, IRoleService
    {
        public RoleService(AppDb _dbContext, ILogHelper _iLogHelper)
               : base(_dbContext)
        {

        }

        public IEnumerable<Roles> GetRoleByUserId(int userId)
        {
            return dbContext.UserRole.AsNoTracking().Include(x => x.Roles).Where(x => x.UserId == userId).Select(y => new Roles
            {
                Id = y.RoleId,
                Name = y.Roles.Name
            });
            //return dbContext.Rol.Where(m => m.Userlar.Any(m2 => m2.Id == userId)).ToList();
            //return dbContext.Rol.Where(m => m.UserRol
            //return dbContext.Roller.Where(m => m.Userlar.Any(m2 => m2.Id == userId)).ToList();
        }

        public IEnumerable<Roles> GetRoleByUserName(string userName)
        {
            Users User = dbContext.User.Where(x => x.Username == userName).FirstOrDefault();
            if (User == null)
                return null;

            return dbContext.UserRole.AsNoTracking().Include(x => x.Roles).Where(x => x.UserId == User.Id).Select(y => new Roles
            {
                Id = y.RoleId,
                Name = y.Roles.Name
            });
            //return dbContext.Rol.Where(m => m.Userlar.Any(m2 => m2.Username == userName)).ToList();
            //return base.GetContext<Db>().Roller.Where(m => m.Userlar.Any(m2 => m2.Username == userName)).ToList();
        }
    }
}
