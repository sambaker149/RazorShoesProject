using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.Images
{
    [Authorize(Roles = "Staff")]
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
            return Page();
        }

        [BindProperty]
        public Image Image { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Images == null || Image == null)
            {
                return Page();
            }

            _context.Images.Add(Image);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}