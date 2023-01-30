using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Data;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.OrderItems
{
    public class EditModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public EditModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }

            var item = await _context.OrderItems.FirstOrDefaultAsync(m => m.OrderItemId == id);
            if (item == null)
            {
                return NotFound();
            }
            OrderItem = item;
            ViewData["ShoeId"] = new SelectList(_context.Shoes, "ShoeId", "Name");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(OrderItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Order_HolidayExists(OrderItem.OrderItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool Order_HolidayExists(int id)
        {
            return _context.OrderItems.Any(e => e.OrderItemId == id);
        }
    }
}
