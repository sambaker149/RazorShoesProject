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
    public class DetailsModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public DetailsModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}