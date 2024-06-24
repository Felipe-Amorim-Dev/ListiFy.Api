using Listify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Interfaces.Services
{
    public interface IUsuarioDomainService
    {
        Task CriarContaUsuario(Usuario usuario);

        Task<Usuario> Autenticar(string email, string senha);

        Task<Usuario> AtualizarDados(
            string? email,
            string nome,
            string sobrenome,
            string telefone,
            byte[]? fotoPerfil
            );

        Task<Usuario> AtualizarSenha(string? email, string senha);

        Task<Usuario> AtualizarEmail(string? email, string novoEmail);

        Task<Usuario> GetUsuario(string? email);

        Task DeletarUsuario(Guid usuarioID);
    }
}
