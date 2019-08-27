using Core3_Framework.Contracts.DataContracts;
using System.Collections.Generic;

namespace Core3_Framework.Contracts.Services
{
    public interface IRoleService
    {
        IEnumerable<Roles> GetRoleByUserId(int userId);

        IEnumerable<Roles> GetRoleByUserName(string userName);
    }
}
