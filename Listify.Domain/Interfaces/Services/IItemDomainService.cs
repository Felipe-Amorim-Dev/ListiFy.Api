using Listify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Interfaces.Services
{
    public interface IItemDomainService
    {
        Task<Guid> CadastrarItem(Item item);

        Task<Item> AtualizarItem(           
            string? titulo,
            string descricao,
            string categoria,
            string tipo,
            List<ItemFoto> galeria
            );

        Task<Item> DeletarItem(string titulo);

        Task<List<Item>> ConsultarItems(Guid? usuarioID);
    }
}
