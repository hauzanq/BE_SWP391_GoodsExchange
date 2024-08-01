using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Common.Exceptions;
using GoodsExchange.BusinessLogic.Extensions;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Report;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Enums;
using GoodsExchange.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class ReportService : IReportService
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IServiceWrapper _serviceWrapper;

        public ReportService(GoodsExchangeDbContext context, IHttpContextAccessor httpContextAccessor, IServiceWrapper serviceWrapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _serviceWrapper = serviceWrapper;
        }

        public async Task<ResponseModel<bool>> UpdateReportStatusAsync(Guid id, ReportStatus status)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                throw new NotFoundException("Report does not exist");
            }

            report.Status = status;

            await _context.SaveChangesAsync();

            return new ResponseModel<bool>($"The report status was update to {status.ToString()}.", true);
        }
        public async Task<ResponseModel<ReportViewModel>> SendReportAsync(CreateReportRequestModel request)
        {
            var user = await _serviceWrapper.UserServices.GetUserAsync(Guid.Parse(_httpContextAccessor.GetCurrentUserId()));

            var receiver = await _serviceWrapper.UserServices.GetUserByProductId(request.ProductId);

            if (await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId) == null)
            {
                throw new NotFoundException("This product does not exist.");
            }

            if (await _serviceWrapper.ProductServices.IsProductBelongToSellerAsync(request.ProductId, user.UserId))
            {
                throw new BadRequestException("This product belongs to you so you cannot report this product.");
            }

            var report = new Report()
            {
                Reason = request.Reason,
                DateCreated = DateTime.Now,
                SenderId = user.UserId,
                ReceiverId = receiver.UserId,
                ProductId = request.ProductId,
                Status = ReportStatus.AwaitingApproval
            };

            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();

            var product = await _serviceWrapper.ProductServices.GetProductAsync(request.ProductId);

            var result = new ReportViewModel()
            {

                ReportMade = user.FirstName + " " + user.LastName,
                ReportReceived = await _serviceWrapper.UserServices.GetUserFullNameAsync(report.ReceiverId),
                ReportId = report.ReportId,
                ProductId = product.ProductId,
                ProductImages = product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                ProductName = product.ProductName,
                Reason = report.Reason,
            };
            return new ResponseModel<ReportViewModel>("The report was submitted successfully.", result);
        }

        public async Task<ResponseModel<PageResult<ReportViewModel>>> GetReportsAsync(PagingRequestModel paging, ReportsRequestModel request)
        {
            var query = _context.Reports.Where(r => r.Status == ReportStatus.AwaitingApproval)
                        .Include(r => r.Sender)
                        .Include(r => r.Receiver)
                        .Include(r => r.Product).AsQueryable();

            #region Filtering report

            if (!string.IsNullOrEmpty(request.Sender))
            {
                query = query.Where(r => r.Sender.UserName.Contains(request.Sender));
            }

            if (!string.IsNullOrEmpty(request.Receiver))
            {
                query = query.Where(r => r.Receiver.UserName.Contains(request.Receiver));
            }

            if (request.FromDate != null)
            {
                query = query.Where(r => r.DateCreated >= request.FromDate);
            }

            if (request.ToDate != null)
            {
                query = query.Where(r => r.DateCreated <= request.ToDate);
            }

            #endregion

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / paging.PageSize);

            var data = await query.Select(report => new ReportViewModel()
            {
                ReportMade = report.Sender.FirstName + " " + report.Sender.LastName,
                ReportReceived = report.Receiver.FirstName + " " + report.Receiver.LastName,
                ProductId = report.ProductId,
                ProductImages = report.Product.ProductImages.Select(pi => pi.ImagePath).ToList(),
                ProductName = report.Product.ProductName,
                ReportId = report.ReportId,
                Reason = report.Reason,
                Status = report.Status.ToString()
            }).ToListAsync();


            var result = new PageResult<ReportViewModel>()
            {
                Items = data,
                TotalPage = totalPages,
                CurrentPage = paging.PageIndex
            };

            return new ResponseModel<PageResult<ReportViewModel>>(result);
        }

        public async Task<ResponseModel<ReportViewModel>> GetReportByIdAsync(Guid id)
        {
            var report = await _context.Reports.Include(r => r.Sender)
                                                .Include(r => r.Receiver)
                                                .Include(r => r.Product)
                                                .FirstOrDefaultAsync(r => r.ReportId == id);
            if (report == null)
            {
                throw new NotFoundException("Report does not exist");
            }

            var result = new ReportViewModel()
            {
                ReportMade = report.Sender.FirstName + " " + report.Sender.LastName,
                ReportReceived = report.Receiver.FirstName + " " + report.Receiver.LastName,
                ProductId = report.ProductId,
                ProductName = report.Product.ProductName,
                Reason = report.Reason,
                Status = report.Status.ToString()
            };

            return new ResponseModel<ReportViewModel>("The report was retrieved successfully.", result);
        }

        public async Task<int> CountReportsReceivedOfUserAsync(Guid userId)
        {
            return await _context.Reports.Where(r => r.ReceiverId == userId && r.Status == ReportStatus.Approved).CountAsync();
        }

        public async Task<bool> IsUserReportedOnProduct(Guid userid, Guid productid)
        {
            var result = _context.Reports.Any(r => r.SenderId == userid && r.ProductId == productid);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}
