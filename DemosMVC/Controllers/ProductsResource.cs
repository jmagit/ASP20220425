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
    public class ProductoDTO {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ProductNumber { get; set; }

    }
    [Route("api/products")]
    [ApiController()]
    public class ProductsResource : ControllerBase {
        private readonly TiendaDbContext _context;

        public ProductsResource(TiendaDbContext context) {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> GetProducts(int page=0, int size = 20) {
            return await _context.Products
                .Skip(page*size).Take(size)
                .Select(p => new ProductoDTO() { ProductId=p.ProductId, Name=p.Name, ProductNumber=p.ProductNumber })
                .ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id) {
            var product = await _context.Products.FindAsync(id);

            if (product == null) {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product) {
            if (id != product.ProductId) {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) {
                if (!ProductExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product) {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) {
            var product = await _context.Products.FindAsync(id);
            if (product == null) {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id) {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
