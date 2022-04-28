using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domains.Entities;
using Infraestructure.UoW;
using Domains.Contracts.DomainsServices;

namespace DemosMVC.Controllers {
    public class ProductsController : Controller {
        private readonly TiendaDbContext _context;
        private readonly IProductoService srv;

        public ProductsController(TiendaDbContext context, IProductoService servicio) {
            _context = context;
            srv = servicio;
        }

        // GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var tiendaDbContext = _context.Products.Include(p => p.ProductModel).Include(p => p.ProductSubcategory).Include(p => p.SizeUnitMeasureCodeNavigation).Include(p => p.WeightUnitMeasureCodeNavigation);
        //    return View(await tiendaDbContext.ToListAsync());
        //}

        // GET: Products
        public IActionResult Index(int num, int size = 20) {
            var listado = srv.GetAll(num, size);
            ViewBag.Paginas = (int)Math.Ceiling((decimal)_context.Products.Count() / size);
            return View(listado);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create() {
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name");
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategories, "ProductSubcategoryId", "Name");
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode");
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,Ventas")] Product product) {
            if (ModelState.IsValid) {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategories, "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null) {
                return NotFound();
            }
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategories, "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
            return View("create", product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,ProductNumber,MakeFlag,FinishedGoodsFlag,Color,SafetyStockLevel,ReorderPoint,StandardCost,ListPrice,Size,SizeUnitMeasureCode,WeightUnitMeasureCode,Weight,DaysToManufacture,ProductLine,Class,Style,ProductSubcategoryId,ProductModelId,SellStartDate,SellEndDate,DiscontinuedDate,Rowguid,ModifiedDate,Ventas")] Product product) {
            if (id != product.ProductId) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!ProductExists(product.ProductId)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductModelId"] = new SelectList(_context.ProductModels, "ProductModelId", "Name", product.ProductModelId);
            ViewData["ProductSubcategoryId"] = new SelectList(_context.ProductSubcategories, "ProductSubcategoryId", "Name", product.ProductSubcategoryId);
            ViewData["SizeUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", product.SizeUnitMeasureCode);
            ViewData["WeightUnitMeasureCode"] = new SelectList(_context.UnitMeasures, "UnitMeasureCode", "UnitMeasureCode", product.WeightUnitMeasureCode);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null) {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null) {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id) {
            return _context.Products.Any(e => e.ProductId == id);
        }
        //[NonAction]
        public async Task<IActionResult> Photo(int? id) {
            if (id == null) {
                return NotFound();
            }

            var foto = await _context.ProductProductPhotos.Where(p => p.ProductId == id)
                .Select(p => p.ProductPhoto.LargePhoto).FirstOrDefaultAsync();
            if (foto == null) {
                return NotFound();
            }
            return File(foto, "image/gif");
        }

    }
}
