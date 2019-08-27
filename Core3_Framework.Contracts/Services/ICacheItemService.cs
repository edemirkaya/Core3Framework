using CommonCore.Interfaces;
using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using System.Collections.Generic;

namespace Core3_Framework.Contracts.Services
{
    public interface ICacheItemService
    {
        ServiceResult<IEnumerable<CacheItems>> GetAll();

        ServiceResult<IEnumerable<IAuditTableEntity>> GetAuditTables();

        ServiceResult<bool> IsAuthorisedForFunction(int[] functionGroupIds);


        ServiceResult<bool> IsAuthorisedForService(int[] serviceGroupIds);

        ServiceResult<bool> IsAuthorisedForPage(int[] sayfaGrupId);
    }
}
