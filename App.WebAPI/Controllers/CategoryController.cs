using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommonCore.Server.Services;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(int categoryId)
        {
            return Ok(await _categoryService.GetCategory(categoryId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            return Ok(await _categoryService.GetAllCategory());
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoryBySeo(string seoUrl)
        {
            return Ok(await _categoryService.GetCategoryBySeo(seoUrl));
        }
    }
}