using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;

namespace Core3_Framework.Contracts.Services
{
    public interface IUserService
    {
        ServiceResult<Users> GetUser(int p_KullaniciId);
        ServiceResult<Users> GetUser(string userName, string password);

        ServiceResult<Users> GetUser(string userName);
    }
}
