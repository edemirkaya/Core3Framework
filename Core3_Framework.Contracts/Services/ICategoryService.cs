using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core3_Framework.Contracts.Services
{
    public interface ICategoryService
    {
        [Get("/GetCategory/{categoryId}")]
        Task<ServiceResult<Categories>> GetCategory(int categoryId);

        [Get("/GetAllCategory")]
        Task<ServiceResult<List<Categories>>> GetAllCategory();
    }
}
