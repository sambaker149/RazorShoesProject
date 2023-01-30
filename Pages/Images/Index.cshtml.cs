using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.Images
{
    [Authorize(Roles = "Staff")]
    public class IndexModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public IndexModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Image> Image { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Images != null)
            {
                Image = await _context.Images
                .Include(p => p.Shoe).ToListAsync();
            }
        }
    }
}