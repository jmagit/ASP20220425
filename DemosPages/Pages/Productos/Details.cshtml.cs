using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Domains.Entities;
using Infraestructure.UoW;

namespace DemosPages.Pages.Productos
{
    public class DetailsModel : PageModel
    {
        private readonly Infraestructure.UoW.TiendaDbContext _context;

        public DetailsModel(Infraestructure.UoW.TiendaDbContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
