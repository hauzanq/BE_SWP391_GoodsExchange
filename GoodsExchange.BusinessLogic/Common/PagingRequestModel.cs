using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
