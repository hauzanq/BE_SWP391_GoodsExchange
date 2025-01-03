﻿using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOptions<EmailSettings> _emailSettings;
        private readonly IConfiguration _configuration;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IServer _server;
        public ServiceWrapper(GoodsExchangeDbContext context,
            IHttpContextAccessor httpContextAccessor,
            IOptions<EmailSettings> emailSettings,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            IServer server)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _emailSettings = emailSettings;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            _server = server;
        }

        private ICategoryService _categoryService;
        public ICategoryService CategoryServices
        {
            get
            {
                if (_categoryService is null)
                {
                    _categoryService = new CategoryService(_context);
                }
                return _categoryService;
            }
        }

        private IEmailHelperService _emailTemplateHelper;

        public IEmailHelperService EmailHelperServices
        {
            get
            {
                if (_emailTemplateHelper is null)
                {
                    _emailTemplateHelper = new EmailHelperService();
                }
                return _emailTemplateHelper;
            }
        }



        private IEmailService _emailService;
        public IEmailService EmailServices
        {
            get
            {
                if (_emailService is null)
                {
                    _emailService = new EmailService(_emailSettings, this, _webHostEnvironment, _server);
                }
                return _emailService;
            }
        }

        private IProductService _productService;
        public IProductService ProductServices
        {
            get
            {
                if (_productService is null)
                {
                    _productService = new ProductService(_context, _httpContextAccessor, this);
                }
                return _productService;
            }
        }


        private IRatingService _ratingService;
        public IRatingService RatingServices
        {
            get
            {
                if (_ratingService is null)
                {
                    _ratingService = new RatingService(_context, _httpContextAccessor, this);
                }
                return _ratingService;
            }
        }

        private IReportService _reportService;
        public IReportService ReportServices
        {
            get
            {
                if (_reportService is null)
                {
                    _reportService = new ReportService(_context, _httpContextAccessor, this);
                }
                return _reportService;
            }
        }

        private IRoleService _roleService;
        public IRoleService RoleServices
        {
            get
            {
                if (_roleService is null)
                {
                    _roleService = new RoleService(_context);
                }
                return _roleService;
            }
        }

        private IUserService _userService;

        public IUserService UserServices
        {
            get
            {
                if (_userService is null)
                {
                    _userService = new UserService(_context, _httpContextAccessor, _configuration, this);
                }
                return _userService;
            }
        }

        private IFirebaseStorageService _firebaseStorageService;
        public IFirebaseStorageService FirebaseStorageServices
        {
            get
            {
                if (_firebaseStorageService is null)
                {
                    _firebaseStorageService = new FirebaseStorageService(_configuration);
                }
                return _firebaseStorageService;
            }
        }

        private ITransactionService _transactionService;
        public ITransactionService TransactionService
        {
            get
            {
                if (_transactionService is null)
                {
                    _transactionService = new TransactionService(_context, _httpContextAccessor, this);
                }
                return _transactionService;
            }
        }

        private IExchangeRequestService _preOrderService;
        public IExchangeRequestService ExchangeRequestService
        {
            get
            {
                if (_preOrderService is null)
                {
                    _preOrderService = new ExchangeRequestService(_context, _httpContextAccessor, this);
                }
                return _preOrderService;
            }
        }


    }
}
