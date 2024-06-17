using Listify.Domain.Entities;
using Listify.Domain.Interfaces.Repositories;
using Listify.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Services
{
    public class ItemDomainService : IItemDomainService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IItemFotoRepository _itemFotoRepository;

        public ItemDomainService(IItemRepository itemRepository, IItemFotoRepository itemFotoRepository = null, IUsuarioRepository usuarioRepository = null)
        {
            _itemRepository = itemRepository;
            _itemFotoRepository = itemFotoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Item> AtualizarItem(string? titulo, string descricao, string categoria, string tipo, List<ItemFoto> galeria)
        {
            var item = await _itemRepository?.GetAsync(titulo);

            if (item == null)
            {
                throw new ApplicationException("Titulo não encontrado.");
            }

            var itemAtualizado = false;

            if (!string.IsNullOrWhiteSpace(titulo))
            {
                item.Titulo = titulo;
                itemAtualizado = true;
            }

            if (!string.IsNullOrWhiteSpace(descricao))
            {
                item.Descricao = descricao;
                itemAtualizado = true;
            }

            if (!string.IsNullOrWhiteSpace(categoria))
            {
                item.Categoria = categoria;
                itemAtualizado = true;
            }

            if(!string.IsNullOrWhiteSpace(tipo)) 
            {
                item.Tipo = tipo;
                itemAtualizado = true;
            }

            if (galeria != null && galeria.Any())
            {                                
                foreach (var foto in galeria)
                {
                    foto.ItemId = item.Id;
                    await _itemFotoRepository?.CreateAsync(foto);
                }

                item.Galeria = galeria;
                itemAtualizado = true;
            }

            if (itemAtualizado)
            {
               await _itemRepository?.UpdateAsync(item);
            }

            return item;
        }

        public async Task<Item> DeletarItem(string titulo)
        {
            var item = await _itemRepository?.GetAsync(titulo);

            if (item == null)
            {
                throw new ApplicationException("O título não existe.");
            }

            var fotos = await _itemFotoRepository?.GetAsync(item.Id.Value);

            if (fotos != null)
            {
                foreach (var foto in fotos)
                {
                    await _itemFotoRepository?.DeleteAsync(foto);
                }
            }

            await _itemRepository?.DeleteAsync(item);

            return item;
        }

        public async Task<Guid> CadastrarItem(Item item)
        {
            if(await _itemRepository?.GetAsync(item.Titulo) != null)
            {
                throw new ApplicationException("O titulo já está cadastrado.");
            }

            await _itemRepository?.CreateAsync(item);

            if (item.Galeria != null && item.Galeria.Any())
            {
                foreach (var foto in item.Galeria)
                {
                    foto.ItemId = item.Id;
                    await _itemFotoRepository?.CreateAsync(foto);
                }
            }

            return item.Id.Value;

        }

        public async Task<List<Item>> ConsultarItems(Guid usuarioID)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync(usuarioID);
            var items = await _itemRepository?.GetAllAsync();
            
            var itemsUsuario = items.Where(i => i.UsuarioID == usuarioID).ToList();

            if (itemsUsuario == null || !itemsUsuario.Any())
            {
                throw new ApplicationException("Você não possui itens em seu catálogo.");
            }

            return itemsUsuario;

        }
    }
}
