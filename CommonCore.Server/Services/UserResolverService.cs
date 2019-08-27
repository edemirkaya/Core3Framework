using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace CommonCore.Server.Services
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _context;
        public UserResolverService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public string GetUserName()
        {
            if (_context.HttpContext.User.Identities.First().Claims.Count() == 0)
                return "";
            return _context.HttpContext.User.Identities.First().Claims.First(c => c.Type.Contains("emailaddress")).Value;
        }

        public int GetUserId()
        {
            if (_context.HttpContext.User.Identities.First().Claims.Count() == 0)
                return -1;
            return Convert.ToInt32(_context.HttpContext.User.Identities.First().Claims.First(c => c.Type.Contains("UserId")).Value);
        }

    }
}
