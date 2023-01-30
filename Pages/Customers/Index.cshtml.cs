using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.Customers
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public IndexModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Customer> Customer { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Customers != null)
            {
                Customer = await _context.Customers.ToListAsync();
                //.Include(c => c.Address).ToListAsync();
            }
        }
    }
}