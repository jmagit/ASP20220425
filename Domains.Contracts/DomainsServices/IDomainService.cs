using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains.Contracts.DomainsServices {
    public interface IDomainService<E, K> {
        List<E> GetAll();
        E GetOne(K id);
        E Add(E item);
        E Modify(E item);
        void Delete(E item);
        void DeleteById(K id);
    }
}
