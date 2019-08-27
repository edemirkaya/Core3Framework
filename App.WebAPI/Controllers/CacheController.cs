using System.Collections.Generic;
using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class CacheController : Controller
    {
        private readonly ICacheItemService cacheItemService;

        public CacheController(ICacheItemService _cacheItemService)
        {
            cacheItemService = _cacheItemService;
        }

        [Authorize]
        [HttpGet]
        public ServiceResult<IEnumerable<CacheItems>> CacheleriGetir()
        {
            return cacheItemService.GetAll();
        }


        [Authorize]
        [HttpPost]
        public ServiceResult<bool> IsAuthorisedForFunctionAsync([FromBody] int[] functionGroupIds)
        {
            return cacheItemService.IsAuthorisedForFunction(functionGroupIds);
        }

        [Authorize]
        [HttpPost]
        public ServiceResult<bool> IsAuthorisedForService([FromBody] int[] serviceGroupIds)
        {
            return cacheItemService.IsAuthorisedForService(serviceGroupIds);
        }

        [Authorize]
        [HttpPost]
        public ServiceResult<bool> IsAuthorisedForPage([FromBody] int[] sayfaGrupId)
        {
            return cacheItemService.IsAuthorisedForPage(sayfaGrupId);
        }


    }
}