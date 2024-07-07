using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.User
{
     public class UpdateUserRoleRequestModel
    {
       public Guid id {  get; set; }
       public bool status { get; set; }
       public Guid roleId { get; set; }
    }
}
