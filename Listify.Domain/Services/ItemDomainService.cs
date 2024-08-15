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

        public async Task<Item> AtualizarItem(Item item, Guid usuarioID, Guid itemId, string titulo, string descricao, string categoria, string tipo, List<ItemFoto> galeria)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync(usuarioID);
            
                if (usuario != null)
                {
                    var getItem = await _itemRepository?.GetByIdAsync(itemId);

                    if (getItem != null || getItem.UsuarioID == usuarioID)
                    {                        

                        var itemAtualizado = false;

                        if (!string.IsNullOrWhiteSpace(titulo))
                        {
                            getItem.Titulo = titulo;
                            itemAtualizado = true;
                        }

                        if (!string.IsNullOrWhiteSpace(descricao))
                        {
                            getItem.Descricao = descricao;
                            itemAtualizado = true;
                        }

                        if (!string.IsNullOrWhiteSpace(categoria))
                        {
                            getItem.Categoria = categoria;
                            itemAtualizado = true;
                        }

                        if (!string.IsNullOrWhiteSpace(tipo))
                        {
                            getItem.Tipo = tipo;
                            itemAtualizado = true;
                        }

                        if (galeria != null && galeria.Any())
                        {
                            foreach (var foto in galeria)
                            {
                                foto.ItemId = item.Id;
                                await _itemFotoRepository?.CreateAsync(foto);
                            }

                            getItem.Galeria = galeria;
                            itemAtualizado = true;
                        }

                        if (itemAtualizado)
                        {
                            await _itemRepository?.UpdateAsync(getItem);
                        }

                        return getItem;
                    }
                    else
                    {
                        throw new ApplicationException("Item não encontrado ou não pertence ao usuário.");
                    }
                }
                else
                {
                    throw new ApplicationException("Usuário não encontrado.");
                }                                              
        }

        public async Task<Item> DeletarItem(Guid id, Guid usuarioID)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync(usuarioID);
            var item = await _itemRepository?.GetByIdAsync(id);            

            var itemUsuario = item.Equals(item.UsuarioID == usuarioID);

            if (itemUsuario == null)
            {
                throw new ApplicationException("O título não existe.");
            }

            if (usuario != null)
            {
                if (itemUsuario == null)
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
            else
            {
                throw new ApplicationException("Usuário não encontrado.");
            }                        
        }

        public async Task CadastrarItem(Item item, Guid usuarioID)
        {
            if(item.UsuarioID == usuarioID)
            {
                try
                {                   
                    if (await _itemRepository?.GetAsync(item.Titulo) == null)
                    {                        

                    await _itemRepository?.CreateAsync(item);
                    }
                    else
                    {
                        throw new ApplicationException("O titulo já está cadastrado.");
                    }                    
                }
                catch (Exception e) 
                {
                    throw new ApplicationException($"Erro ao cadastrar o item: {e.Message}");
                }
            }
            else
            {
                throw new ApplicationException("Usuário não encontrado.");
            }                                
        }

        public async Task<List<Item>> ConsultarItems(Guid usuarioID)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync(usuarioID);
            if (usuario == null)
            {
                throw new ApplicationException("Usuário não encontrado.");
            }

            var items = await _itemRepository?.GetAllAsync();
            var fotos = await _itemFotoRepository.GetAllAsync();

            var itemsUsuario = items?.Where(i => i.UsuarioID == usuarioID).ToList();

            if (itemsUsuario == null || !itemsUsuario.Any())
            {
                throw new ApplicationException("Você não possui itens em seu catálogo.");
            }

            foreach (var item in itemsUsuario)
            {
                item.Galeria = fotos?.Where(f => f.ItemId == item.Id).ToList();
            }

            return itemsUsuario;

        }

        public async Task<Item> ConsultarItemById(Guid usuarioID, Guid itemId)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync(usuarioID);
            if (usuario == null)
            {
                throw new ApplicationException("Usuário não encontrado.");
            }

            var item = await _itemRepository?.GetByIdAsync(itemId);
            if (item == null || item.UsuarioID != usuarioID)
            {
                throw new ApplicationException("Item não encontrado ou você não tem permissão para acessá-lo.");
            }

            var fotos = await _itemFotoRepository.GetAllAsync();
            item.Galeria = fotos?.Where(f => f.ItemId == item.Id).ToList();

            return item;
        }

        public async Task DeletarFoto(Guid itemId, Guid fotoId)
        {
            var item = await _itemRepository?.GetByIdAsync(itemId);
            if (item == null)
            {
                throw new ApplicationException("Item não encontrado.");
            }

            var itemFoto = await _itemFotoRepository?.GetByIdAsync(fotoId);
            if (itemFoto == null || itemFoto.ItemId != itemId)
            {
                throw new ApplicationException("Foto não encontrada ou você não tem permissão para acessá-la.");
            }
            
            await _itemFotoRepository.DeleteAsync(itemFoto);                       
        }

        public async Task DeletarFotos(Guid itemId)
        {
            var item = await _itemRepository?.GetByIdAsync(itemId);
            if (item == null)
            {
                throw new ApplicationException("Item não encontrado.");
            }
            
            var itemFotos = await _itemFotoRepository?.GetAllAsync();
            var fotosDoItem = itemFotos?.Where(f => f.ItemId == itemId).ToList();

            if (fotosDoItem == null || !fotosDoItem.Any())
            {
                throw new ApplicationException("O item não possui fotos.");
            }

            foreach (var foto in fotosDoItem)
            {
                await _itemFotoRepository.DeleteAsync(foto);
            }

            item.Galeria = new List<ItemFoto>();

            await _itemRepository.UpdateAsync(item);
        }        
    }
}
