using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.Shoes
{
    [Authorize(Roles = "Staff")]
    public class DeleteModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public DeleteModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Shoe Shoe { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Shoes == null)
            {
                return NotFound();
            }

            var shoe = await _context.Shoes.FirstOrDefaultAsync(m => m.ShoeId == id);

            if (shoe == null)
            {
                return NotFound();
            }
            else 
            {
                Shoe = shoe;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Shoes == null)
            {
                return NotFound();
            }
            var shoe = await _context.Shoes.FindAsync(id);

            if (shoe != null)
            {
                Shoe = shoe;
                _context.Shoes.Remove(Shoe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
