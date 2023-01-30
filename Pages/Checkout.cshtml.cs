using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public CheckoutModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return RedirectToPage("NotFound");
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null || order.IsPaid)
            {
                return RedirectToPage("NotFound");
            }

            Order = GetOrderAsync(order.OrderId).Result;

            if (User.Identity.Name != Order.Customer.Email)
            {
                return RedirectToPage("Unauthorized");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            return await OnGetAsync(Order.OrderId);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var item in Order.OrderItems)
            {
                total += CalculateTotal(item);
            }
            return total;
        }

        public double CalculateTotal(OrderItem item)
        {
            double total;
            var shoe = item.Shoe;
            total = shoe.Price * item.Quantity;
            return total;
        }

        private bool OrderExists(int id)
        {
            return _context.Shoes.Any(e => e.ShoeId == id);
        }

        private async Task<Order> GetOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return null;
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == order.CustomerId);
            order.OrderItems = await _context.OrderItems.Where(m => m.OrderId == order.OrderId)
                .Include(a => a.Shoe)
                .ThenInclude(a => a.Price)
                .ToListAsync();

            order.Customer = customer;

            return order;

        }

        public async Task<IActionResult> OnGetRemoveHoliday(int? oh)
        {
            var item = await _context.OrderItems.FirstOrDefaultAsync(p => p.OrderItemId == oh);
            Order = GetOrderAsync(item.OrderId).Result;

            Order.OrderItems.Remove(item);

            _context.OrderItems.Remove(item);
            if (Order.OrderItems.Count == 0)
            {
                _context.Orders.Remove(Order);
                await _context.SaveChangesAsync();
                return RedirectToPage("Index");
            }
            _context.Attach(Order).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return await OnGetAsync(Order.OrderId);
        }

        public async Task<IActionResult> OnGetCancelOrder(int? id)
        {
            Order = GetOrderAsync((int)id).Result;
            foreach (var item in Order.OrderItems)
            {
                _context.OrderItems.Remove(item);
            }
            Order.OrderItems = null;
            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}