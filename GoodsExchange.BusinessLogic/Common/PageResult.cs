using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.Common
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
