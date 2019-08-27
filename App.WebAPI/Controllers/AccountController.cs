using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ServiceResult<Users> KullaniciGetir(int p_KullaniciId)
        {
            ServiceResult<Users> result = _userService.GetUser(p_KullaniciId);
            return result;
        }

        [HttpGet]
        public ServiceResult<Users> KullaniciGetir(string userName, string password)
        {
            ServiceResult<Users> result = _userService.GetUser(userName, password);
            return result;
        }
    }
}