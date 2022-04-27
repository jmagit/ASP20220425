using Domains.Contracts.DomainsServices;
using Domains.Contracts.Repositories;
using Domains.Entities;
using System;
using System.Collections.Generic;

namespace Domains.Services {
    public class ProductoService : IProductoService {
        private readonly IProductoRepository dao;

        public ProductoService(IProductoRepository dao) {
            this.dao = dao;
        }
        public Product Add(Product item) {
            if (item.IsInvalid)
                throw new Exception("Error");
            return dao.Add(item);
        }

        public void Delete(Product item) {
            if (item.IsInvalid)
                throw new Exception("Error");
            dao.Delete(item);
        }

        public void DeleteById(int id) {
            dao.DeleteById(id);
        }

        public List<Product> GetAll() {
            return dao.GetAll();
        }

        public List<Product> GetAll(int page, int rows = 20) {
            return dao.GetAll(page, rows);
        }

        public Product GetOne(int id) {
            return dao.GetOne(id);
        }

        public Product Modify(Product item) {
            if (item.IsInvalid)
                throw new Exception("Error");
            return dao.Modify(item);
        }

        // Métodos propios del dominio
    }
}
