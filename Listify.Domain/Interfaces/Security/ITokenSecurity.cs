using Listify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Interfaces.Security
{
    public interface ITokenSecurity
    {
        public string GenerateToken(Usuario usuario);
    }
}
