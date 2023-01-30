using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Data;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.OrderItems
{
    public class DeleteModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public DeleteModel(RazorShoesProject.Data.ApplicationDbContext context)
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
            else
            {
                OrderItem = item;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.OrderItems == null)
            {
                return NotFound();
            }
            var item = await _context.OrderItems.FindAsync(id);

            if (item != null)
            {
                OrderItem = item;
                _context.OrderItems.Remove(OrderItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}