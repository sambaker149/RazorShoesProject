using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Data;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages
{
    [Authorize]
    public class OrderEditModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public OrderEditModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public Shoe Shoe { get; set; } = default!;

        [BindProperty]
        public OrderItem OrderItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return RedirectToPage("NotFound");
            }

            OrderItem = await _context.OrderItems.FirstOrDefaultAsync(m => m.OrderItemId == id);
            var order = GetOrderAsync(OrderItem.OrderId).Result;
            var shoe = await _context.Shoes.FirstOrDefaultAsync(m => m.ShoeId == OrderItem.ShoeId);

            if (order == null || shoe == null || order.IsPaid)
            {
                return RedirectToPage("NotFound");
            }

            else if (User.Identity.Name != Order.Customer.Email)
            {
                return RedirectToPage("Unauthorized");
            }

            shoe.Images = await _context.Images.Where(m => m.ShoeId == shoe.ShoeId).ToListAsync();
            shoe = shoe;

            Order = order;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.OrderItems == null || _context.Orders == null
                || OrderItem.Quantity < 1)
            {
                return await OnGetAsync(OrderItem.OrderItemId);
            }

            Order.Customer.Email = User.Identity?.Name;
            OrderItem.Order = Order;
            OrderItem.Shoe = Shoe;
            _context.Attach(Order).State = EntityState.Modified;
            _context.Attach(OrderItem).State = EntityState.Modified;

            // Saves the data to the database
            await _context.SaveChangesAsync();

            return RedirectToPage("Checkout", new { id = Order.OrderId });
        }

        // Get Order through ID for editing purposes
        private async Task<Order> GetOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return null;
            }

            var customer = await _context.Customers.FirstOrDefaultAsync(m => m.CustomerId == order.CustomerId);
            //customer.Address = await _context.Addresses.FirstOrDefaultAsync(m => m.AddressId == customer.AddressId);
            order.OrderItems = await _context.OrderItems.Where(m => m.OrderId == order.OrderId)
                .Include(a => a.Shoe)
                .ThenInclude(a => a.Name)
                .ToListAsync();

            order.Customer = customer;

            return order;

        }
    }
}