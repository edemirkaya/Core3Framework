using System;
using App.WebAPI.Models;
using CommonCore.Interfaces;
using Core3_Framework.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace App.WebAPI.Controllers
{
    public class BaseController : Controller
    {
        private readonly DbContext dbContext;
        public readonly ILogHelper Ilog;

        public BaseController()
        {

        }

        public BaseController(DbContext _dbContext, ICacheItemService _cacheItemService)
        {
            dbContext = _dbContext;
        }

        public BaseController(DbContext _dbContext, ICacheItemService _cacheItemService, ILogHelper _iLogHelper)
        {
            dbContext = _dbContext;
            Ilog = _iLogHelper;
        }

        public bool Log(string logMessage, CommonCore.Types.Enums.LogType logType)
        {
            Type type = this.GetType().UnderlyingSystemType;
            String className = type.Name;
            String nameSpace = type.Namespace;

            return Ilog.Log(nameSpace + "." + className + "|" + logMessage, logType);
        }


    }
}