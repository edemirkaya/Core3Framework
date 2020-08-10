using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core3_Framework.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace App.WebAPI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProducts());
        }

        [HttpGet]
        public async Task<IActionResult> GetProductbyId(int productId)
        {
            return Ok(await _productService.GetProductbyId(productId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            return Ok(await _productService.GetProductsByCategoryId(categoryId));
        }
    }
}
