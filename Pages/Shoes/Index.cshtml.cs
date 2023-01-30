using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.Shoes
{
    [Authorize(Roles = "Staff")]
    public class IndexModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public IndexModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Shoe> Shoe { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Shoes != null)
            {
                Shoe = await _context.Shoes.ToListAsync();
            }
        }
    }
}
