using Domains.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Contracts.DomainsServices {
    public interface IProductoService: IDomainService<Product, int>, IPageableService<Product> {
    }
}
