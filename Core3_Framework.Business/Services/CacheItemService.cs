using CommonCore.Interfaces;
using CommonCore.Server.Services;
using CommonCore.Types.Enums;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using System.Collections.Generic;

namespace Core3_Framework.Business.Services
{
    public class CacheItemService : ICacheItemService
    {
        ICacheManager cacheManager;

        public CacheItemService(ICacheManager _cacheManager)
        {
            cacheManager = _cacheManager;
        }
        public ServiceResult<IEnumerable<CacheItems>> GetAll()
        {
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;
            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<IEnumerable<CacheItems>> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = cacheManager.GetCacheItemList() };
        }

        public ServiceResult<IEnumerable<IAuditTableEntity>> GetAuditTables()
        {
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;
            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<IEnumerable<IAuditTableEntity>> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = cacheManager.GetAuditTables() };
        }


        public ServiceResult<bool> IsAuthorisedForFunction(int[] functionGroupIds)
        {
            //string hataMesaji = string.Empty;
            //EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;
            //sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<bool>(cacheManager.IsAuthorisedForFunctionAsync(functionGroupIds));
        }

        public ServiceResult<bool> IsAuthorisedForService(int[] serviceGroupIds)
        {
            //string hataMesaji = string.Empty;
            //EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;
            //sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<bool>(cacheManager.IsAuthorisedForService(serviceGroupIds));
        }

        public ServiceResult<bool> IsAuthorisedForPage(int[] sayfaGrupId)
        {
            //string hataMesaji = string.Empty;
            //EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;
            //sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<bool>(cacheManager.IsAuthorisedForPage(sayfaGrupId));
        }


    }
}
