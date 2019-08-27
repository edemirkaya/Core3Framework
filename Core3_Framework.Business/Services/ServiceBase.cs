using AutoMapper;
using CommonCore.Interfaces;
using Core3_Framework.Data;
using System;

namespace Core3_Framework.Business.Services
{
    public class ServiceBase
    {
        public readonly AppDb dbContext;
        public readonly ILogHelper Ilog;
        public readonly IMapper Mapper;

        public ServiceBase(AppDb _dbContext)
        {
            dbContext = _dbContext;
        }
        public ServiceBase(AppDb _dbContext, ILogHelper _iLogHelper)
        {
            dbContext = _dbContext;
            Ilog = _iLogHelper;
        }

        public ServiceBase(AppDb _dbContext, ILogHelper _iLogHelper, IMapper _mapper)
        {
            dbContext = _dbContext;
            Ilog = _iLogHelper;
            Mapper = _mapper;
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
