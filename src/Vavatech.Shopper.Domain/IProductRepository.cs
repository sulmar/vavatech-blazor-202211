using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vavatech.Shopper.Domain
{

    public interface IProductRepository : IEntityRepository<Product>
    {      
        Task<IEnumerable<Product>> GetByColor(string color);
        Task<IEnumerable<Product>> GetByContent(string content);
        Task<VirtualizeResponse<Product>> Get(PagingParameters parameters);
    }
}
