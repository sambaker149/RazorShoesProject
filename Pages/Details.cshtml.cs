using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;


namespace RazorShoesProject.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public DetailsModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Shoe Shoe { get; set; } = default!;

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

            shoe.Images = await _context.Images.Where(m => m.ShoeId == shoe.ShoeId).ToListAsync(); ;
            Shoe = shoe;

            return Page();
        }
    }
}