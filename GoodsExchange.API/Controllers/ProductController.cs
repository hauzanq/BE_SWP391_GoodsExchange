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
        public ActionResult<ProductViewModel> CreateProduct(CreateProductRequestModel productCreate)
        {
            var productCreated = _productService.CreateProduct(productCreate);

            if (productCreated == null)
            {
                return NotFound("");
            }
            return productCreated;
        }


        [HttpGet]
        public ActionResult<List<ProductViewModel>> GetAll()
        {
            var productList = _productService.GetAll();

            if (productList == null)
            {
                return NotFound("");
            }
            return productList;
        }


        [HttpGet("idTmp")]
        public ActionResult<ProductViewModel> GetById(int idTmp)
        {
            var productDetail = _productService.GetById(idTmp);

            if (productDetail == null)
            {
                return NotFound("");
            }
            return productDetail;
        }


        [HttpDelete]
        public ActionResult<bool> DeleteProduct(int idTmp)
        {
            var check = _productService.DeleteProduct(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }


        [HttpPut]
        public ActionResult<ProductViewModel> UpdateProduct(UpdateProductRequestModel productCreate)
        {
            var productUpdated = _productService.UpdateProduct(productCreate);

            if (productUpdated == null)
            {
                return NotFound("");
            }
            return productUpdated;
        }
    }

}
