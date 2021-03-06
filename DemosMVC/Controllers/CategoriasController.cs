using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domains.Entities;
using Infraestructure.UoW;

namespace DemosMVC.Controllers {
    public class CategoriaDTO {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase {
        private readonly TiendaDbContext _context;

        public CategoriasController(TiendaDbContext context) {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetProductCategories() {
            return await _context.ProductCategories.Select(item => new CategoriaDTO() { Id = item.ProductCategoryId, Nombre = item.Name }).ToListAsync();
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id) {
            var productCategory = await _context.ProductCategories.Include(i => i.ProductSubcategories).FirstOrDefaultAsync(item => item.ProductCategoryId == id);

            if (productCategory == null) {
                return NotFound();
            }

            return productCategory;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory) {
            if (id != productCategory.ProductCategoryId) {
                return this.Problem(detail: "No coinciden los idetificadores", statusCode: 400);
                //return BadRequest();
            }

            _context.Entry(productCategory).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ProductCategoryExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categorias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory) {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCategory", new { id = productCategory.ProductCategoryId }, productCategory);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductCategory(int id) {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null) {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductCategoryExists(int id) {
            return _context.ProductCategories.Any(e => e.ProductCategoryId == id);
        }
    }
}
