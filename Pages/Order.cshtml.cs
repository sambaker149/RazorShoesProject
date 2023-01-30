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
    public class OrderModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public OrderModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        private Order Order = new Order();
        private OrderItem OrderItem = new OrderItem();
        private int orderId = 0;

        [BindProperty]
        public Shoe Shoe { get; set; } = default!;

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        [BindProperty]
        public Address Address { get; set; } = default!;

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public int Night { get; set; }

        [BindProperty]
        public int Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shoes == null)
            {
                return RedirectToPage("NotFound");
            }

            var shoe = await _context.Shoes.FirstOrDefaultAsync(m => m.ShoeId == id);
            if (shoe == null)
            {
                return RedirectToPage("NotFound");
            }

            shoe.Images = await _context.Images.Where(m => m.ShoeId == shoe.ShoeId).ToListAsync();
            Shoe = shoe;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Shoes == null || Shoe == null
                        || Customer == null || Address == null)
            {
                return await OnGetAsync(Shoe.ShoeId);
            }

            AddCustomerInfo();

            AddOrderInfo();

            await _context.SaveChangesAsync();

            return RedirectToPage("Checkout", new { id = Order.OrderId });
        }

        public void AddCustomerInfo()
        {
            _context.Addresses.Add(Address);
            Customer.Email = User.Identity?.Name;
            //Customer.Address = Address;
            _context.Customers.Add(Customer);
        }

        public void AddOrderInfo()
        {
            Order.Customer = Customer;
            _context.Orders.Add(Order);

            OrderItem.Order = Order;
            OrderItem.ShoeId = Shoe.ShoeId;
            OrderItem.Shoe = default!;

            _context.OrderItems.Add(OrderItem);
        }
    }
}