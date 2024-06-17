using Listify.Domain.Entities;
using Listify.Domain.Interfaces.Repositories;
using Listify.Infra.Data.Context;
using Listify.Infra.Data.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Infra.Data.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public async Task<Item> GetAsync(string titulo)
        {
            using (var context = new DataContext())
            {
                return context.Item.FirstOrDefault(i => i.Titulo.Equals(titulo));
            }            
        }
    }
}
