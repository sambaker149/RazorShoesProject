﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorShoesProject.Models;

namespace RazorShoesProject.Pages.Customers
{
    [AllowAnonymous]
    public class EditModel : PageModel
    {
        private readonly RazorShoesProject.Data.ApplicationDbContext _context;

        public EditModel(RazorShoesProject.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customers == null)
            {
                return NotFound();
            }

            //var customer = await _context.Customers.FirstOrDefaultAsync(m => m.AddressId == id);
            //if (customer == null)
            //{
                //return NotFound();
            //}
            //Customer = customer;
            //ViewData["AddressID"] = new SelectList(_context.Addresses, "AddressId", "HouseNumber", "Postcode");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!CustomerExists(Customer.AddressId))
                //{
                    //return NotFound();
                //}
                //else
                //{
                    //throw;
                //}
            }

            return RedirectToPage("./Index");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}