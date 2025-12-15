using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL.Data.Contexts;
using Restaurant.PL.Models;
using System.Diagnostics;

namespace Restaurant.PL.Controllers
{


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // Inject DbContext.
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Landing()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            try
            {
                // ===== STATISTICS =====
                ViewBag.TotalOrdersToday = await _context.Orders
                    .Where(o => o.OrderDate.Date == DateTime.Today)
                    .CountAsync();

                ViewBag.TotalMenuItems = await _context.MenuItems.CountAsync();
                ViewBag.ActiveCustomers = await _context.Customers.CountAsync();

                var revenueTodayRaw = await _context.Orders
                    .Where(o => o.OrderDate.Date == DateTime.Today)
                    .SumAsync(o => (decimal?)o.Total) ?? 0;

                ViewBag.RevenueToday = revenueTodayRaw.ToString("C");
                ViewBag.RevenueTodayRaw = revenueTodayRaw;

                // Calculate trends
                var yesterdayOrders = await _context.Orders
                    .Where(o => o.OrderDate.Date == DateTime.Today.AddDays(-1))
                    .CountAsync();

                ViewBag.OrderTrend = yesterdayOrders > 0
                    ? Math.Round(((ViewBag.TotalOrdersToday - yesterdayOrders) / (double)yesterdayOrders) * 100, 0)
                    : 0;

                // ===== RECENT ACTIVITIES =====
                var activities = new List<ActivityItem>();

                // Recent Orders (last 2)
                var recentOrders = await _context.Orders
                    .Include(o => o.NavTable)
                    .Include(o => o.NavOrderItems)
                        .ThenInclude(oi => oi.MenuItem)
                    .OrderByDescending(o => o.OrderDate)
                    .Take(2)
                    .ToListAsync();

                foreach (var order in recentOrders)
                {
                    activities.Add(new ActivityItem
                    {
                        Type = "order",
                        Title = $"New Order #{order.Id}",
                        Description = $"Table {order.NavTable?.TableNumber ?? "N/A"} - {string.Join(", ", order.NavOrderItems.Take(2).Select(oi => oi.MenuItem?.ItemName ?? "Unknown"))}",
                        TimeAgo = GetTimeAgo(order.OrderDate),
                        Timestamp = order.OrderDate
                    });
                }

                // Recent Payments (last 1)
                var recentPayment = await _context.Payments
                    .Include(p => p.NavOrder)
                    .OrderByDescending(p => p.PaymentDate)
                    .FirstOrDefaultAsync();

                if (recentPayment != null)
                {
                    activities.Add(new ActivityItem
                    {
                        Type = "payment",
                        Title = "Payment Received",
                        Description = $"Order #{recentPayment.OrderId} - ${recentPayment.Amount:F2} paid",
                        TimeAgo = GetTimeAgo(recentPayment.PaymentDate),
                        Timestamp = recentPayment.PaymentDate
                    });
                }

                // Recent Customer (last 1)
                var recentCustomer = await _context.Customers
                    .OrderByDescending(c => c.CreatedOn)
                    .FirstOrDefaultAsync();

                if (recentCustomer != null)
                {
                    activities.Add(new ActivityItem
                    {
                        Type = "customer",
                        Title = "New Customer",
                        Description = $"{recentCustomer.FirstName} {recentCustomer.LastName} registered",
                        TimeAgo = GetTimeAgo(recentCustomer.CreatedOn),
                        Timestamp = recentCustomer.CreatedOn
                    });
                }

                ViewBag.RecentActivities = activities
                    .OrderByDescending(a => a.Timestamp)
                    .Take(4)
                    .ToList();

                // ===== SYSTEM ALERTS =====
                var alerts = new List<AlertItem>();

                // Low stock check
                var lowStockCount = await _context.MenuItems
                    .Where(m => m.Stock < 10)
                    .CountAsync();

                if (lowStockCount > 0)
                {
                    alerts.Add(new AlertItem
                    {
                        Type = "warning",
                        Title = "Low Stock Alert",
                        Message = $"{lowStockCount} menu items are running low on stock."
                    });
                }

                // Active orders count
                var activeOrdersCount = await _context.Orders
                    .Where(o => o.Status == "Pending" || o.Status == "InProgress")
                    .CountAsync();

                if (activeOrdersCount > 5)
                {
                    alerts.Add(new AlertItem
                    {
                        Type = "info",
                        Title = "Peak Hours",
                        Message = $"High order volume: {activeOrdersCount} active orders."
                    });
                }

                // Daily goal check
                var dailyGoal = 10000m;
                if (revenueTodayRaw >= dailyGoal)
                {
                    alerts.Add(new AlertItem
                    {
                        Type = "success",
                        Title = "Daily Goal Achieved! 🎉",
                        Message = $"Revenue target of ${dailyGoal:N0} reached!"
                    });
                }

                ViewBag.Alerts = alerts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data");
                SetDefaultViewBagValues();
            }

            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Helper Methods

        private string GetTimeAgo(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan.TotalMinutes < 1) return "Just now";
            if (timeSpan.TotalMinutes < 60) return $"{(int)timeSpan.TotalMinutes} min ago";
            if (timeSpan.TotalHours < 24)
            {
                var hours = (int)timeSpan.TotalHours;
                return $"{hours} hour{(hours > 1 ? "s" : "")} ago";
            }
            if (timeSpan.TotalDays < 7)
            {
                var days = (int)timeSpan.TotalDays;
                return $"{days} day{(days > 1 ? "s" : "")} ago";
            }

            return dateTime.ToString("MMM dd");
        }

        private void SetDefaultViewBagValues()
        {
            ViewBag.TotalOrdersToday = 0;
            ViewBag.TotalMenuItems = 0;
            ViewBag.ActiveCustomers = 0;
            ViewBag.RevenueToday = "$0.00";
            ViewBag.RevenueTodayRaw = 0;
            ViewBag.OrderTrend = 0;
            ViewBag.RecentActivities = new List<ActivityItem>();
            ViewBag.Alerts = new List<AlertItem>();
        }
        #endregion
    }
    #region Helper classes.
    public class ActivityItem
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TimeAgo { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }

    public class AlertItem
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
    #endregion
}