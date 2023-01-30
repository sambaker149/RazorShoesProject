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
    public class IndexModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public IndexModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderItem> OrderItem { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OrderItems != null)
            {
                OrderItem = await _context.OrderItems
                .Include(o => o.Shoe)
                .Include(o => o.Order).ToListAsync();
            }
        }
    }
}