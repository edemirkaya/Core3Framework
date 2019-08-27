using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class UserController : BaseController
    {
        IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [AllowAnonymous]
        public ServiceResult<Users> UserGetir(int p_UserId)
        {
            return userService.GetUser(p_UserId);
        }
    }
}