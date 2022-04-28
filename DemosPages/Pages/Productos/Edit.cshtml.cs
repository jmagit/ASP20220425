using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domains.Entities;
using Infraestructure.UoW;

namespace DemosPages.Pages.Productos
{
    public class EditModel : PageModel
    {
        private readonly Infraestructure.UoW.TiendaDbContext _context;

        public EditModel(Infraestructure.UoW.TiendaDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Products
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation).FirstOrDefaultAsync(m => m.ProductId == id);

            if (Product == null)
            {
                return NotFound();
            }
           ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name");
           ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategories, "ProductSubcategoryId", "Name");
           ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode");
           ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode");
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

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.ProductId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
