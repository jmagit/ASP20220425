using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domains.Entities;
namespace Domains.Contracts.Repositories {
    public interface IProductoRepository : IRepository<Product, int> {
        List<Product> GetAll(int page, int rows = 20);

    }
}
