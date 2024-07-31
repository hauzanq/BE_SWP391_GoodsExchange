using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
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

        public async Task<ResponseModel<CategoryViewModel>> CreateCategory(CreateCategoryRequestModel categoryCreate)
        {
            var category = new Category()
            {
                CategoryName = categoryCreate.CategoryName,
            };
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();

            var result = new CategoryViewModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };
            return new ResponseModel<CategoryViewModel>("The category was created successfully.", result);
        }
        
        public async Task<ResponseModel<bool>> DeleteCategory(Guid id)
        {
            var existedCategory = await _context.Categories.FindAsync(id);
        
            if (existedCategory == null)
            {
                throw new NotFoundException("Category does not exist.");
            }
            _context.Remove(existedCategory);
            await _context.SaveChangesAsync();
            return new ResponseModel<bool>("The category was deleted successfully.", true);
        }

        public async Task<ResponseModel<PageResult<CategoryViewModel>>> GetCategories(PagingRequestModel paging)
        {
            var categories = await _context.Categories.ToListAsync();
            var data = categories.Select(c => new CategoryViewModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,

            }).ToList();

            var number = data.Count();
            var pages = (int)Math.Ceiling((double)number / paging.PageSize);

            var result = new PageResult<CategoryViewModel>()
            {
                Items = data,
                TotalPage = pages,
                CurrentPage = paging.PageIndex
            };
            return new ResponseModel<PageResult<CategoryViewModel>>(result);
        }

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);
            if (category == null)
            {
                throw new NotFoundException("Category does not exist.");
            }
            return category;
        }

        public async Task<ResponseModel<CategoryViewModel>> UpdateCategory(UpdateCategoryRequestModel categoryUpdate)
        {
            var categoriesExisted = await _context.Categories.FindAsync(categoryUpdate.CategoryId);
            if (categoriesExisted == null)
            {
                throw new NotFoundException("Category does not exist.");
            }

            categoriesExisted.CategoryName = categoryUpdate.CategoryName;
            await _context.SaveChangesAsync();
            var result = new CategoryViewModel()
            {
                CategoryName = categoryUpdate.CategoryName,
            };
            return new ResponseModel<CategoryViewModel>("The category was updated successfully.", result);
        }
    }
}
