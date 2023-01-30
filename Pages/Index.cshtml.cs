using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Data;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _context;

    [BindProperty]
    public List<Shoe> Shoes { get; set; }

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var shoes = await _context.Shoes.Where(p => p.ShoeId <= 10 && p.ShoeId > 0).ToListAsync();
        if (shoes == null)
        {
            return NotFound();
        }

        foreach (var shoe in shoes)
        {
            shoe.Images = await _context.Images.Where(m => m.ShoeId == shoe.ShoeId)
                .ToListAsync();
        }

        Shoes = shoes;

        return Page();
    }
}