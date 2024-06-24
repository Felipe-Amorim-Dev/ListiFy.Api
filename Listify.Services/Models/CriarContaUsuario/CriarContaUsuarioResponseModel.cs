using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.CriarContaUsuario
{
    public class CriarContaUsuarioResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataCriacao { get; set; }
    }
}
