using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages
{
    [Authorize]
    public class ConfirmationModel : PageModel
    {

        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public ConfirmationModel(RazorShoesProject.Data.ApplicationDbContext context)
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
            if (order == null)
            {
                return RedirectToPage("NotFound");
            }
            else if (!order.IsPaid)
            {
                return RedirectToPage("Checkout", new { id = order.OrderId });
            }

            Order = GetOrderAsync(order.OrderId).Result;

            if (User.Identity.Name != Order.Customer.Email)
            {
                return RedirectToPage("Unauthorized");
            }


            return Page();
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
                .ThenInclude(a => a.Name)
                .ToListAsync();

            order.Customer = customer;

            return order;

        }
    }
}