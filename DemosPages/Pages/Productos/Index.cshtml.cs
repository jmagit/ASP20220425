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
    public class IndexModel : PageModel
    {
        private readonly Infraestructure.UoW.TiendaDbContext _context;

        public IndexModel(Infraestructure.UoW.TiendaDbContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Products
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation).ToListAsync();
        }
    }
}
