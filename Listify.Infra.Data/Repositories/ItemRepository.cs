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
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(DataContext context) : base(context)
        {
        }

        public async Task<Item> GetAsync(string titulo)
        {
            return await _context.Item
                .FirstOrDefaultAsync(i => i.Titulo.Equals(titulo));
        }
    }
}
