﻿using GoodsExchange.BusinessLogic.Services.Interface;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class EmailHelperService : IEmailHelperService
    {
        private  string FOLDER = "EmailTemplates";
        private  readonly string REGISTER_TEMPLATE_FILE = "RegisterConfirm.html";
        private readonly string UPDATE_TEMPLATE_FILE = "UpdateProfileConfirm.html";

        private  readonly string CONFIRM_RESET_PASSWORD_TEMPLATE_FILE = "ConfirmResetPassword.html";

        private  readonly string NEWPASSWORD_TEMPLATE_FILE = "NewPasswordTemplate.html";
        private  readonly string EXCHANGE_REQUEST_TEMPLATE_FILE = "ExchangeRequestTemplate.html";

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

       

        public string UPDATE_NEWEMAIL_TEMPLATE(string rootPath)
        {
            return GetTemplate(rootPath, UPDATE_TEMPLATE_FILE);
        }

        public string EXCHANGE_REQUEST_TEMPLATE(string rootPath)
        {
            return GetTemplate(rootPath, EXCHANGE_REQUEST_TEMPLATE_FILE);
        }
    }
}
