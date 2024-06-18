using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly GoodsExchangeDbContext _context;
        public CategoryService(GoodsExchangeDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResult<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel categoryCreate)
        {
            var category = new Category()
            {
                CategoryName = categoryCreate.CategoryName,
            };
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            var result = new CategoryViewModel()
            {
                CategoryName = category.CategoryName,

            };
            return new ApiSuccessResult<CategoryViewModel>(result);

        }
        
        public async Task<ApiResult<bool>> DeleteCategory(Guid id)
        {
            var existedCategory = await _context.Categories.FindAsync(id);
        
            if (existedCategory == null)
            {
                return new ApiErrorResult<bool>("Category does not exist.");
            }
            _context.Remove(existedCategory);
            await _context.SaveChangesAsync();
            return new ApiSuccessResult<bool>(true);

        }

        public async Task<ApiResult<List<CategoryViewModel>>> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            var result = categories.Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,

            }).ToList();
            return new ApiSuccessResult<List<CategoryViewModel>>(result);
        }

        public async Task<ApiResult<CategoriesDetailViewModel>> GetById(Guid idTmp)
        {
            var category = await _context.Categories.FindAsync(idTmp);
            if (category == null)
            {
                return new ApiErrorResult<CategoriesDetailViewModel>("Category does not exist.");
            }
            var product = await _context.Products.FirstOrDefaultAsync(p => p.CategoryId == category.CategoryId);

            var result = new CategoriesDetailViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                ProductID = product.ProductId,
                ProductName = product.ProductName,
                //ProductImageUrl = product.ProductImageUrl,
                Description = product.Description,
                Price = product.Price,
                ApprovedDate = product.ApprovedDate,
                UserUploadId = product.UserUploadId,

            };
            return new ApiSuccessResult<CategoriesDetailViewModel>(result);
        }

        public async Task<ApiResult<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel categoryUpdate)
        {
            var categoriesExisted = await _context.Categories.FindAsync(categoryUpdate.CategoryId);
            if (categoriesExisted == null)
            {
                return new ApiErrorResult<CategoryViewModel>("Category does not exist.");
            }

            categoriesExisted.CategoryName = categoryUpdate.CategoryName;
            await _context.SaveChangesAsync();
            var result = new CategoryViewModel()
            {

                CategoryName = categoryUpdate.CategoryName,
            };
            return new ApiSuccessResult<CategoryViewModel>(result);
        }
    }
}
