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
        Task CadastrarItem(Item item, Guid usuarioID);

        Task<Item> AtualizarItem(
            Item item,
            Guid usuarioID,
            Guid itemId,
            string titulo,
            string descricao,
            string categoria,
            string tipo,
            List<ItemFoto> galeria
            );

        Task<Item> DeletarItem(string titulo, Guid usuarioID);

        Task<List<Item>> ConsultarItems(Guid usuarioID);

        Task<Item> ConsultarItemById(Guid usuarioID, Guid ItemId);

        Task DeletarFoto(Guid itemId, Guid fotoId);

        Task DeletarFotos(Guid itemId);
    }
}
