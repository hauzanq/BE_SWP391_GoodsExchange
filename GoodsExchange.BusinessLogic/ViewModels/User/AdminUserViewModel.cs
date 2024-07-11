using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.ViewModels.User
{
    public class AdminUserViewModel : BaseUserViewModel
    {
        public Guid UserId { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }
    }
}
