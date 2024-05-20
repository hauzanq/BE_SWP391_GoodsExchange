using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.Services;
using GoodsExchange.BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]

    [Route("/api/v1/products")]
    public class ProductController : ControllerBase
    {

        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<ActionResult<ProductViewModel>> CreateProduct(CreateProductRequestModel productCreate)
        {
            var productCreated = await _productService.CreateProduct(productCreate);

            if (productCreated == null)
            {
                return NotFound("");
            }
            return productCreated;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> GetAll()
        {
            var productList = await _productService.GetAll();

            if (productList == null)
            {
                return NotFound("");
            }
            return productList;
        }


        [HttpGet("idTmp")]
        public async Task<ActionResult<ProductViewModel>> GetById(int idTmp)
        { 
            var productDetail = await _productService.GetById(idTmp);

            if (productDetail == null)
            {
                return NotFound("");
            }
            return productDetail;
        }


        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteProduct(int idTmp)
        {
            var check = await _productService.DeleteProduct(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }


        [HttpPut]
        public async Task<ActionResult<ProductViewModel>> UpdateProduct(UpdateProductRequestModel productCreate)
        {
            var productUpdated = await _productService.UpdateProduct(productCreate);

            if (productUpdated == null)
            {
                return NotFound("");
            }
            return productUpdated;
        }
    }

}
