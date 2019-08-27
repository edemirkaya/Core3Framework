using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core3_Framework.Contracts.Services
{
    public interface ICategoryService
    {
        ServiceResult<Categories> GetCategory(int categoryId);
        ServiceResult<List<Categories>> GetAllCategory();
    }
}
