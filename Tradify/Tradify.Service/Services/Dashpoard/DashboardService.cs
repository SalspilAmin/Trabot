using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;

namespace Tradify.Service.Services.Dashpoard
{

   

    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(
            ApplicationDbContext _context)
        {
            this._context = _context;
        }



        public async Task<AdminDashboardDto> GetAdminDashboardAsync()
        {
            var response = new AdminDashboardDto
            {
                TotalUsers = await _context.Users.CountAsync(),

                TotalSellers = await _context.Sellers.CountAsync(),

                TotalInstructors = await _context.Instructors.CountAsync(),

                TotalStores = await _context.Stores.CountAsync(),

                TotalProducts = await _context.Products.CountAsync(),

                TotalServices = await _context.Service.CountAsync(),

                TotalOrders = await _context.Orders.CountAsync(),

                TotalBookings = await _context.Bookings.CountAsync(),

                TotalRevenue = await _context.Payments
                    .Where(x => x.PaymentStatus == PaymentStatus.Paid)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0,

                PendingPayouts = await _context.Payouts
                    .Where(x => x.PaymentStatus == PaymentStatus.Pending)
                    .SumAsync(x => (decimal?)x.Amount) ?? 0
            };

            return response;
        }









    }

}
