namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public  interface IEmailHelperService
    {
        
        public  string REGISTER_TEMPLATE(string rootPath);
        public  string CONFIRM_RESET_PASSWORD(string rootPath);

        public  string NEWPASSWORD_TEMPLATE(string rootPath);

        public string UPDATE_NEWEMAIL_TEMPLATE(string rootPath);
        public string EXCHANGE_REQUEST_TEMPLATE(string rootPath);

    }
}
