using Listify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Interfaces.Repositories
{
    public interface IItemFotoRepository : IBaseRepository<ItemFoto>
    {
        Task<List<ItemFoto>> GetAsync(Guid itemId);
    }
}
