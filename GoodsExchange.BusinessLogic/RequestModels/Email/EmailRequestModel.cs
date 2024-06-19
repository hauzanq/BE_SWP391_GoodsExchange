using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.Email
{
    /// <summary>
    /// SOME INFORMATION ABOUT GMAIL ToEmail.
    /// Subject : nội dung Title của một Email Thông thường 
    /// Body : Render HTML/CSS to send content with popular and friendly for ToEmail --> Content
    /// </summary>
    public class EmailRequestModel
    {
        public string ToEmail {  get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
