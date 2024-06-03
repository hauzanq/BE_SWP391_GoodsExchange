using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.Email
{
    public class EmailRequestModel
    {
        public string ToEmail {  get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
