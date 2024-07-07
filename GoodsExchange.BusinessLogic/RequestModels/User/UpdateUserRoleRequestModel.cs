using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.User
{
    public class UpdateUserRoleRequestModel

    {

        [Required(ErrorMessage = "User ID is required.")]
        public Guid id { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        public bool status { get; set; }

        [Required(ErrorMessage = "Role ID is required.")]
        public Guid roleId { get; set; }
    }
}
