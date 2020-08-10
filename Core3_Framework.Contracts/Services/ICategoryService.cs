using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core3_Framework.Contracts.Services
{
    public interface ICategoryService
    {
        Task<ServiceResult<Categories>> GetCategory(int categoryId);

        Task<ServiceResult<List<Categories>>> GetAllCategory();

        Task<ServiceResult<List<Categories>>> GetCategoryBySeo(string seoURL);
    }
}
