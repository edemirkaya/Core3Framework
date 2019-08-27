using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ServiceResult<Categories> GetCategory(int categoryId)
        {
            return _categoryService.GetCategory(categoryId);
        }

        [HttpGet]
        public ServiceResult<List<Categories>> GetAllCategory()
        {
            return _categoryService.GetAllCategory();
        }
    }
}