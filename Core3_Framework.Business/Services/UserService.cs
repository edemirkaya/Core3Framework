using CommonCore.Interfaces;
using CommonCore.Server.Services;
using CommonCore.Types.Enums;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Core3_Framework.Data;
using System.Linq;

namespace Core3_Framework.Business.Services
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(AppDb _dbContext, ILogHelper _iLogHelper)
                : base(_dbContext)
        {

        }

        public ServiceResult<Users> GetUser(string userName, string password)
        {
            int hata = 0;
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;
            Users kullanici = new Users();
            kullanici = dbContext.User.Where(x => x.Username == userName && x.Password == password).FirstOrDefault();

            if (kullanici == null)
            {
                hataMesaji = "Kullanıcı bulunamadı.";
                sonucTipi = EnumServiceResultType.Error;
                hata++;
            }
            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<Users> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = kullanici };
        }

        public ServiceResult<Users> GetUser(int p_KullaniciId)
        {
            int hata = 0;
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;
            Users kullanici = new Users();
            kullanici = dbContext.User.Where(x => x.Id == p_KullaniciId).FirstOrDefault();

            if (kullanici == null)
            {
                hataMesaji = "Kullanıcı bulunamadı.";
                sonucTipi = EnumServiceResultType.Error;
                hata++;
            }
            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<Users> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = kullanici };
        }

        public ServiceResult<Users> GetUser(string userName)
        {
            int hata = 0;
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;
            Users kullanici = new Users();
            kullanici = dbContext.User.Where(x => x.Username == userName).FirstOrDefault();

            if (kullanici == null)
            {
                hataMesaji = "Kullanıcı bulunamadı.";
                sonucTipi = EnumServiceResultType.Error;
                hata++;
            }
            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<Users> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = kullanici };
        }
    }
}
