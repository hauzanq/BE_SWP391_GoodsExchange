using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.Common
{
    public class PagingRequestModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "PageIndex must be greater than 1")]
        public int PageIndex { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "PageSize must be greater than 1")]
        public int PageSize { get; set; }
    }
}
