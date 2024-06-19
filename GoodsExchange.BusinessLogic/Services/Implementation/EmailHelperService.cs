using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Email;
using GoodsExchange.BusinessLogic.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class EmailHelperService : IEmailTemplateHelper
    {
        private  string FOLDER = "EmailTemplates";
        private  readonly string REGISTER_TEMPLATE_FILE = "RegisterConfirm.html";

        private  readonly string CONFIRM_RESET_PASSWORD_TEMPLATE_FILE = "ConfirmResetPassword.html";

        private  readonly string NEWPASSWORD_TEMPLATE_FILE = "NewPasswordTemplate.html";

        public string REGISTER_TEMPLATE(string rootPath)
        {
            return GetTemplate(rootPath, REGISTER_TEMPLATE_FILE);
        }

        public string CONFIRM_RESET_PASSWORD(string rootPath)
        {
            return GetTemplate(rootPath, CONFIRM_RESET_PASSWORD_TEMPLATE_FILE);
        }

        public string NEWPASSWORD_TEMPLATE(string rootPath)
        {
            return GetTemplate(rootPath, NEWPASSWORD_TEMPLATE_FILE);
        }

        private string GetTemplate(string rootPath,string templateFile)
        {
            string templatePath = Path.Combine(rootPath, FOLDER, templateFile);
            try
            {
                using (var streamReader = File.OpenText(templatePath))
                {
                    return streamReader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading template file {templatePath}: {ex.Message}");
            }
            return string.Empty;
        }

        //public Task<ApiResult<string>> GetTemplate(EmailHelperRequestModel request)
        //{
        //    string templatePath = Path.Combine(request.RootPath, FOLDER, request.templateFile);
        //    try
        //    {
        //        using (var streamReader = File.OpenText(request.templateFile))
        //        {
        //            string content = streamReader.ReadToEnd();

        //            return new Task<ApiSuccessResult<string>>(content);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Task<ApiErrorResult<string>>(string.Empty);
        //    }

        //}

        //public Task<ApiResult<string>> REGISTER_TEMPLATE(string rootPath)
        //{
        //   return 
        //}




    }
}
