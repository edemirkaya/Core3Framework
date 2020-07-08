﻿using CommonCore.Interfaces;
using CommonCore.Server.Services;
using CommonCore.Types.Enums;
using Core3_Framework.Contracts.DataContracts;
using Core3_Framework.Contracts.Services;
using Core3_Framework.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core3_Framework.Business.Services
{
    public class ProductService : ServiceBase
    {
        public ProductService(AppDb _dbContext, ILogHelper _iLogHelper)
               : base(_dbContext)
        {

        }

        public ServiceResult<Products> GetProductbyId(int productId)
        {
            int hata = 0;
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;

            Products product = new Products();

            product = dbContext.Products.AsNoTracking().Include(x => x.Categories).Where(x => x.Id == productId).Select(y => new Products
            {
                Id = y.Id,
                ProductName = y.ProductName,
                QuantityPerUnit = y.QuantityPerUnit,
                UnitPrice = y.UnitPrice,
                UnitsInStock = y.UnitsInStock,
                CategoryId = y.CategoryId
            }).FirstOrDefault();

            if (product == null)
            {
                hataMesaji = "Ürün bulunamadı.";
                sonucTipi = EnumServiceResultType.Error;
                hata++;
            }

            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<Products> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = product };
        }

        public IEnumerable<Products> GetProductsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
