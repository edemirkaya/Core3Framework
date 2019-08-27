using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3_Framework.Contracts.Services
{
    public interface IProductService
    {
        ServiceResult<Products> GetProductbyId(int productId);
        IEnumerable<Products> GetProductsByCategoryId(int categoryId);
    }
}
