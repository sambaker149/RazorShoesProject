using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Data;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages
{
    public class ShoeModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration Configuration;

        public string PriceSort { get; set; }
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [BindProperty]
        public List<Shoe> Shoes { get; set; }

        public ShoeModel(ILogger<IndexModel> logger, ApplicationDbContext context, IConfiguration configuration)
        {
            _logger = logger;
            _context = context;
            Configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync(string sortOrder,
            string currentFilter, string searchString)
        {
            var shoes = await _context.Shoes.ToListAsync();
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

            CurrentFilter = currentFilter;
            SearchString = searchString;

            CurrentSort = sortOrder;
            PriceSort = String.IsNullOrEmpty(sortOrder) ? "price" : "";
            PriceSort = sortOrder == "price" ? "price_desc" : "price";

            if (!String.IsNullOrEmpty(searchString))
            {
                Shoes = Shoes.Where(s => s.Name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "price_desc":
                    Shoes = Shoes.OrderByDescending(s => s.Price).ToList();
                    break;
                case "price":
                    Shoes = Shoes.OrderBy(s => s.Price).ToList();
                    break;
                default:
                    Shoes = Shoes.OrderBy(s => s.Name).ToList();
                    break;
            }

            switch (currentFilter)
            {
                case "trainer":
                    Shoes = Shoes.Where(s => s.Type == "Trainer").ToList();
                    break;
                case "sneaker":
                    Shoes = Shoes.Where(s => s.Type == "Sneaker").ToList();
                    break;
                case "running":
                    Shoes = Shoes.Where(s => s.Type == "Running Shoes").ToList();
                    break;
                default:
                    break;
            }

            return Page();
        }
    }
}