using Listify.Domain.Entities;
using Listify.Domain.Helpers;
using Listify.Domain.Interfaces.Repositories;
using Listify.Domain.Interfaces.Security;
using Listify.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Services
{
    public class UsuarioDomainServices : IUsuarioDomainService
    {
        private readonly IUsuarioRepository? _usuarioRepository;
        private readonly IItemRepository? _itemRepository;
        private readonly IItemFotoRepository? _itemFotoRepository;
        private readonly ITokenSecurity? _tokenSecurity;

        public UsuarioDomainServices(IUsuarioRepository? usuarioRepository, ITokenSecurity? tokenSecurity, IItemRepository? itemRepository, IItemFotoRepository? itemFotoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _tokenSecurity = tokenSecurity;
            _itemRepository = itemRepository;
            _itemFotoRepository = itemFotoRepository;
        }

        public async Task<Usuario> AtualizarDados(Guid? usuarioID, string nome, string sobrenome, string email, string telefone, byte[]? fotoPerfil)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync((Guid)usuarioID);

            if(usuario == null)
            {
                throw new ApplicationException("Usuario não encontrado.");
            }

            var dadosAtualizados = false;

            if (!string.IsNullOrWhiteSpace(nome) && usuario.Nome != nome)
            {
                usuario.Nome = nome;
                dadosAtualizados = true;
            }

            if (!string.IsNullOrWhiteSpace(sobrenome) && usuario.Sobrenome != sobrenome)
            {
                usuario.Sobrenome = sobrenome;
                dadosAtualizados = true;
            }

            if (!string.IsNullOrWhiteSpace(telefone) && usuario.Telefone != telefone)
            {
                usuario.Telefone = telefone;
                dadosAtualizados = true;
            }

            if (!string.IsNullOrWhiteSpace(email) && usuario.Email != email)
            {
                usuario.Email = email;
                dadosAtualizados = true;
            }

            if (fotoPerfil != null && !fotoPerfil.SequenceEqual(usuario.FotoPerfil))
            {
                usuario.FotoPerfil = fotoPerfil;
                dadosAtualizados = true;
            }

            if (dadosAtualizados)
            {
               await _usuarioRepository?.UpdateAsync(usuario);
            }
            else
            {
                throw new ApplicationException("Não foi feita atualização dos dados");
            }

            return usuario;
        }        

        public async Task<Usuario> AtualizarSenha(Guid? usuarioID, string senha)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync((Guid)usuarioID);

            if(usuario == null)
            {
                throw new ApplicationException("Usuário não encontrado.");
            }

            var senhaAtualizada = false;

            if (!string.IsNullOrWhiteSpace(senha))
            {
                usuario.Senha = MD5Helper.Encrypt(senha);
                senhaAtualizada = true;
            }

            if (senhaAtualizada)
            {
               await _usuarioRepository?.UpdateAsync(usuario);
            }
            else
            {
                throw new ApplicationException("A senha não foi atualizada.");
            }

            return usuario;
        }

        public async Task<Usuario> Autenticar(string email, string senha)
        {
            var usuario = await _usuarioRepository?.GetAsync(email, MD5Helper.Encrypt(senha));

            if(usuario == null)
            {
                throw new ApplicationException("Usuário não encontrado.");
            }

            usuario.AccessToken = _tokenSecurity?.GenerateToken(usuario);

            return usuario;
        }

        public async Task CriarContaUsuario(Usuario usuario)
        {
            if(await _usuarioRepository?.GetAsync(usuario.Email) != null)
            {
                throw new ApplicationException("O email informado já está cadastrado.");
            }

            usuario.Senha = MD5Helper.Encrypt(usuario.Senha);

            await _usuarioRepository?.CreateAsync(usuario);            
        }        

        public async Task DeletarUsuario(Guid usuarioID)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync(usuarioID);            

            if (usuario != null)
            {
                var items = await _itemRepository?.GetAllAsync();
                var itemUsuario = items.Where(i => i.UsuarioID == usuarioID).ToList();

                foreach (var item in itemUsuario)
                {
                    var fotos = await _itemFotoRepository?.GetAllAsync();
                    var fotosItem = fotos.Where(f => f.ItemId == item.Id).ToList();

                    foreach (var foto in fotosItem)
                    {
                        await _itemFotoRepository.DeleteAsync(foto);
                    }

                    await _itemRepository.DeleteAsync(item);
                }

                await _usuarioRepository.DeleteAsync(usuario);                
            }
            else
            {
                throw new ApplicationException("Usuário não encontrado.");
            }
            
        }

        public async Task<Usuario> Usuario(Guid usuarioID)
        {
            var usuario = await _usuarioRepository?.GetByIdAsync((Guid)usuarioID);

            if (usuario == null)
            {
                throw new ApplicationException("Usuário não encontrado.");
            }
            
            await _usuarioRepository?.UpdateAsync(usuario);
            
            return usuario;
        }
    }
}
