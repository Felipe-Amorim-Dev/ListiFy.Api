using Listify.Domain.Entities;
using Listify.Domain.Interfaces.Repositories;
using Listify.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Infra.Data.Repositories
{
    public class ItemFotoRepository : BaseRepository<ItemFoto>, IItemFotoRepository
    {
        public ItemFotoRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<ItemFoto>> GetAsync(Guid itemId)
        {
            return await _context.ItemFoto
                .Where(f => f.ItemId.Equals(itemId))
                .ToListAsync();
        }
    }
}
