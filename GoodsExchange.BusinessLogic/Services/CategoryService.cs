using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
namespace GoodsExchange.BusinessLogic.Services
{

    public interface ICategoryService
    {
        Task<ApiResult<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel categoryCreate);
        Task<ApiResult<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel categoryUpdate);
        Task<ApiResult<bool>> DeleteCategory(Guid id);
        Task<ApiResult<List<CategoryViewModel>>> GetAll();
        Task<ApiResult<CategoriesDetailViewModel>>GetById(Guid idTmp);
    }

    public class CategoryService : ICategoryService
    {
        private readonly GoodsExchangeDbContext _dbContext;
        public CategoryService(GoodsExchangeDbContext dbContext)

        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Create Categories has only categoriesName because categoriesID is indentity base on guid 
        /// </summary>
        /// <param name="categoryCreate"></param>
        /// <returns></returns>
        public async Task<ApiResult<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel categoryCreate)
        {
            var category = new Category()
            {
                CategoryName = categoryCreate.CategoryName,
            };
            await _dbContext.AddAsync(category);
            await _dbContext.SaveChangesAsync();

            var result = new CategoryViewModel()
            {
                CategoryName = category.CategoryName,

            };
            return new ApiSuccessResult<CategoryViewModel>(result);

        }
        /// <summary>
        /// Delete categories must be delete the relationship of categories such as Product ..
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResult<bool>> DeleteCategory(Guid id)
        {
            var existedCategory = await _dbContext.Categories
                                              //    .Include(c => c.Products)
                                                  .Where(c=> c.CategoryId.Equals(id)).FirstOrDefaultAsync();
            if (existedCategory == null)
            {
                return new ApiErrorResult<bool>("Categories doesn't existed");
            }
            _dbContext.Remove(existedCategory);
            await _dbContext.SaveChangesAsync();
            return new ApiSuccessResult<bool>(true);

        }

        public async Task<ApiResult<List<CategoryViewModel>>> GetAll()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var result = categories.Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,

            }).ToList();
            return new ApiSuccessResult<List<CategoryViewModel>>(result);
        }



        public async Task<ApiResult<CategoriesDetailViewModel>>GetById(Guid idTmp)
        {
           var category = await _dbContext.Categories.FindAsync(idTmp);
            if(category == null)
            {
                return new ApiErrorResult<CategoriesDetailViewModel>("Cayegories doesm't existed ");
            }
            var product = await _dbContext.Products.FirstOrDefaultAsync(p=> p.CategoryId == category.CategoryId);

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
            var categoriesExisted = await _dbContext.Categories.FindAsync(categoryUpdate.CategoryId);
            if(categoriesExisted == null)
            {
                return new ApiErrorResult<CategoryViewModel>("Categories doesn't existed");
            }
            
            categoriesExisted.CategoryName = categoryUpdate.CategoryName;
            await _dbContext.SaveChangesAsync();
            var result = new CategoryViewModel()
            {
                
                CategoryName = categoryUpdate.CategoryName,
            };
            return new ApiSuccessResult<CategoryViewModel>(result);

            

                        
        }
    }

}
