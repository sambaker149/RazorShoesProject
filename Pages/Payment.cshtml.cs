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
    public class PaymentModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public PaymentModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; }

        [BindProperty]
        public Payment Payment { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
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
            Order = GetOrderAsync((int)id).Result;
            var expiryDate = Payment.ExpiryDate.Date;
            var today = DateTime.Today;

            if (!ModelState.IsValid || _context.Shoes == null || Order == null
            || Payment == null)
            {
                return await OnGetAsync(Order.OrderId);
            }
            else if (expiryDate <= today && !(expiryDate.Month == today.Month && expiryDate.Year == today.Year))
            {
                ModelState.AddModelError("CardPayment.ExpiryDate", "Expiry Date is invalid! Please input a valid date.");
                return await OnGetAsync(Order.OrderId);
            }

            var cardNumber = Payment.CardNumber;
            var cvvNumber = Payment.CVVNumber;

            Order.Payment = Payment;
            Order.IsPaid = true;

            _context.Attach(Order).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(Order.OrderId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Confirmation", new { id = Order.OrderId });
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
                .ThenInclude(a => a.Name)
                .ToListAsync();

            return order;

        }
    }
}