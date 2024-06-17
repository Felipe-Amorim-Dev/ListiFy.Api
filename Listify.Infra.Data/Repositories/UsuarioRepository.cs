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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public async Task<Usuario> GetAsync(string email)
        {
            using (var context = new DataContext())
            {
                return context.Usuario.FirstOrDefault(u => u.Email.Equals(email));
            }
        }

        public async Task<Usuario> GetAsync(string email, string senha)
        {
            using (var context = new DataContext())
            {
                return context.Usuario.FirstOrDefault(u => u.Email.Equals(email) && u.Senha.Equals(senha));
            } 
        }
    }
}
