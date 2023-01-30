using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.Customers
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public CreateModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["AddressId"] = new SelectList(_context.Addresses, "AddressId", "HouseNumber", "PostCode");
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}