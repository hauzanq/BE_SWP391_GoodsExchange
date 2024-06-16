using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IServiceWrapper
    {
        public ICategoryService CategoryServices { get;}
        public IEmailService EmailServices { get;}
        public IProductService ProductServices { get;}
        public IRatingService RatingServices { get;}
        public IReportService ReportServices { get;}
        public IRoleService RoleServices { get;}
        public IUserService UserServices { get;}
    }
}
