using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorShoesProject.Data;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.OrderItems
{
    public class CreateModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public CreateModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ShoeId"] = new SelectList(_context.Shoes, "ShoeId", "Name");
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId");
            return Page();
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderItems.Add(OrderItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}