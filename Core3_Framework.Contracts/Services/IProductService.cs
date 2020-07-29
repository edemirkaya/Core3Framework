using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core3_Framework.Contracts.Services
{
    public interface IProductService
    {
        Task<ServiceResult<Products>> GetProductbyId(int productId);
        Task<ServiceResult<List<Products>>> GetProductsByCategoryId(int categoryId);
    }
}
