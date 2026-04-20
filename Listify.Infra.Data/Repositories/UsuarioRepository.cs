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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DataContext context) : base(context)
        {
        }

        public async Task<Usuario> GetAsync(string email)
        {
            return await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<Usuario> GetAsync(string email, string senha)
        {
            return await _context.Usuario
                .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Senha.Equals(senha));
        }
    }
}
