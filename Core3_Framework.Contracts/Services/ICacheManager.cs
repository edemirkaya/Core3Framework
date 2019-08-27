using CommonCore.Interfaces;
using Core3_Framework.Contracts.DataContracts;
using System.Collections.Generic;

namespace Core3_Framework.Contracts.Services
{
    public interface ICacheManager
    {
        IEnumerable<CacheItems> GetCacheItemList();

        IEnumerable<IAuditTableEntity> GetAuditTables();
        bool IsAuthorisedForFunctionAsync(int[] functionGroupIds);

        bool IsAuthorisedForService(int[] serviceGroupIds);

        bool IsAuthorisedForPage(int[] pageGrupId);
    }
}
