using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infraestructure.UoW;
using Domains.Contracts.Repositories;
using Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories {
    public class ProductoRepository : IProductoRepository {
        private readonly TiendaDbContext _context;

        public ProductoRepository(TiendaDbContext context) {
            _context = context;
        }


        public List<Product> GetAll() {
            return _context.Products.ToList();
        }
        public List<Product> GetAll(int page, int rows = 20) {
            return _context.Products.Where(item=>!item.DiscontinuedDate.HasValue).Skip(page * rows).Take(rows).ToList();

        }

        public Product GetOne(int id) {
            return _context.Products
                .Include(p => p.ProductModel)
                .Include(p => p.ProductSubcategory)
                .Include(p => p.SizeUnitMeasureCodeNavigation)
                .Include(p => p.WeightUnitMeasureCodeNavigation)
                .FirstOrDefault(m => m.ProductId == id);
        }

        public Product Add(Product item) {
            throw new NotImplementedException();
        }

        public void Delete(Product item) {
            item.DiscontinuedDate = DateTime.Now;
            _context.Update(item);
             _context.SaveChanges();
        }

        public void DeleteById(int id) {
            throw new NotImplementedException();
        }

        public Product Modify(Product item) {
            throw new NotImplementedException();
        }
    }
    public class ProductoRepositoryMock : IProductoRepository {
        public List<Product> GetAll() {
            List<Product> rslt = new() {
                new Product() { ProductNumber = "uno", Name = "producto 1", ProductId = 1 },
                new Product() { ProductNumber = "dos", Name = "producto 2", ProductId = 2 },
                new Product() { ProductNumber = "tres", Name = "producto 3", ProductId = 3 }
            };
            return rslt;
        }
        public List<Product> GetAll(int page = 0, int rows = 20) {
            return GetAll();

        }

        public Product GetOne(int id) {
            return new Product() { ProductNumber = "demo", Name = "producto demo", ProductId = id };
        }

        public Product Add(Product item) {
            throw new NotImplementedException();
        }

        public void Delete(Product item) {
            throw new NotImplementedException();
        }

        public void DeleteById(int id) {
            throw new NotImplementedException();
        }

        public Product Modify(Product item) {
            throw new NotImplementedException();
        }
    }
}
