using CommonCore.Caching;
using CommonCore.Interfaces;
using CommonCore.Server.Interfaces;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Core3_Framework.Data;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core3_Framework.Business.Infrastructure
{
    public class CacheManager : CacheManagerBase, IServiceAuthorizationManager, ICacheManager
    {
        private class CachePolicyNames
        {
            internal const string Expire5Mins = "Expire5mins";
            internal const string Expire1Hour = "Expire1hour";
            internal const string NeverExpire = "NeverExpire";
        }

        private class CacheKeys
        {
            internal const string AuditTablesCacheKey = "_AuditTables";
            internal const string CacheItemList = "_CacheItemList";
            internal const string AuthorizationPrefix = "Auth_";
        }

        private readonly AppDb dbContext;
        private readonly IRoleService rolService;

        public CacheManager(IMemoryCache _memoryCache, AppDb _dbContext, IRoleService _rolService)
            : base(_memoryCache)
        {
            dbContext = _dbContext;
            rolService = _rolService;
        }

        public bool IsAuthorisedForFunctionAsync(int[] functionGroupIds)
        {
            var userName = dbContext.userId;
            var roller = GetUsersAuthorizationsFunc();
            var isAuthorized = false;
            var rolIds = roller.Select(r => r.Id);
            //if users roles contains role ids from attribute
            isAuthorized = !functionGroupIds.ToList().Except(rolIds).Any();

            return isAuthorized;
        }

        public bool IsAuthorisedForService(int[] serviceGroupIds)
        {
            var userName = dbContext.userName;
            var roller = GetUsersAuthorizationsFunc();

            var isAuthorized = false;
            var rolIds = roller.Select(r => r.Id);
            //if users roles contains role ids from attribute
            isAuthorized = !serviceGroupIds.ToList().Except(rolIds).Any();

            return isAuthorized;
        }

        public bool IsAuthorisedForPage(int[] sayfaGrupId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Roles> GetUsersAuthorizationsFunc()
        {
            var userId = dbContext.userId;


            IEnumerable<Roles> roller = rolService.GetRoleByUserId(userId);

            return roller;
        }


        private IEnumerable<IAuditTableEntity> GetAuditTablesFunc()
        {

            return dbContext.AuditTables.ToList();

        }


        public IEnumerable<IAuditTableEntity> GetAuditTables()
        {
            IEnumerable<IAuditTableEntity> _auditTables = GetLazyCacheItem(CacheKeys.AuditTablesCacheKey, GetAuditTablesFunc, CachePolicyNames.Expire5Mins).Value;
            dbContext.AuditTables = _auditTables;
            return _auditTables;
        }

        private IEnumerable<CacheItems> GetCacheItemListFunc()
        {
            var res = new List<CacheItems>();
            return dbContext.CacheItem.ToList();


        }

        public IEnumerable<CacheItems> GetCacheItemList()
        {
            return GetLazyCacheItem(CacheKeys.CacheItemList, GetCacheItemListFunc, CachePolicyNames.Expire5Mins).Value;
        }

    }
}
