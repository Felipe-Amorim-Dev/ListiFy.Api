using Listify.Domain.Entities;
using Listify.Domain.Interfaces.Repositories;
using Listify.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Infra.Data.Repositories
{
    public class ItemFotoRepository : BaseRepository<ItemFoto>, IItemFotoRepository
    {
        public async Task<List<ItemFoto>> GetAsync(Guid itemId)
        {
            using (var context = new DataContext())
            {
                return context.ItemFoto.Where(f => f.ItemId.Equals(itemId)).ToList();
            }
        }
    }
}
