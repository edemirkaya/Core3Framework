using System.Threading.Tasks;

namespace CommonCore.Web.Interfaces
{
    public interface IAuthorizationManager
    {
        Task<bool> IsAuthorisedForFunctionAsync(int[] fonksiyonGrupId);
        Task<bool> IsAuthorisedForPage(int[] sayfaGrupId);
    }
}
