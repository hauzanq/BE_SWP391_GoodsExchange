using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public  interface IEmailTemplateHelper
    {
        
        public  string REGISTER_TEMPLATE(string rootPath);
        public  string CONFIRM_RESET_PASSWORD(string rootPath);

        public  string NEWPASSWORD_TEMPLATE(string rootPath);

    }
}
