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
using System.Threading.Tasks;

namespace Core3_Framework.Business.Services
{
    public class CategoryService : ServiceBase, ICategoryService
    {
        public CategoryService(AppDb _dbContext, ILogHelper _iLogHelper)
               : base(_dbContext)
        {

        }
        public async Task<ServiceResult<List<Categories>>> GetAllCategory()
        {
            int hata = 0;
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;

            List<Categories> category = new List<Categories>();

            category = dbContext.Categories.AsNoTracking().ToList();

            if (category == null)
            {
                hataMesaji = "Ürün bulunamadı.";
                sonucTipi = EnumServiceResultType.Error;
                hata++;
            }

            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<List<Categories>> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = category };
        }

        public async Task<ServiceResult<List<Categories>>> GetCategoryBySeo(string seoURL)
        {
            int hata = 0;
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;

            List<Categories> category = new List<Categories>();

            category = dbContext.Categories.Include(i=> i.Products).Where(q=> q.SeoURL ==seoURL).AsNoTracking().ToList();

            if (category == null)
            {
                hataMesaji = "Ürün bulunamadı.";
                sonucTipi = EnumServiceResultType.Error;
                hata++;
            }

            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<List<Categories>> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = category };
        }

        public async Task<ServiceResult<Categories>> GetCategory(int categoryId)
        {
            int hata = 0;
            string hataMesaji = string.Empty;
            EnumServiceResultType sonucTipi = EnumServiceResultType.Unspecified;

            Categories category = new Categories();

            category = dbContext.Categories.AsNoTracking().Where(x => x.CategoryId == categoryId).Select(y => new Categories
            {
                CategoryId = y.CategoryId,
                CategoryName = y.CategoryName
            }).FirstOrDefault();

            if (category == null)
            {
                hataMesaji = "Ürün bulunamadı.";
                sonucTipi = EnumServiceResultType.Error;
                hata++;
            }

            sonucTipi = EnumServiceResultType.Success;
            return new ServiceResult<Categories> { Message = hataMesaji, ServiceResultType = sonucTipi, Result = category };
        }
    }
}
