using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.Services.Emails
{
    public class EmailtemplateHelpers
    {
        private static string FOLDER = "EmailTemplates";

        private static readonly string REGISTER_TEMPLATE_FILE = "RegisterConfirm.html";

        private static readonly string CONFIRM_RESET_PASSWORD_TEMPLATE_FILE = "ConfirmResetPassword.html";

        private static readonly string NEWPASSWORD_TEMPLATE_FILE = "NewPasswordTemplate.html";


        private static string GetTemplate(string rootPath, string templateFile)
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

        public static string REGISTER_TEMPLATE(string rootPath)
        {
            return GetTemplate(rootPath, REGISTER_TEMPLATE_FILE);
        }
        public static string CONFIRM_RESET_PASSWORD(string rootPath)
        {
            return GetTemplate(rootPath, CONFIRM_RESET_PASSWORD_TEMPLATE_FILE);
        }

        public static string NEWPASSWORD_TEMPLATE(string rootPath)
        {
            return GetTemplate(rootPath, NEWPASSWORD_TEMPLATE_FILE);
        }

    }


}
