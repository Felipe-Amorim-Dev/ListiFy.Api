using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.AtualizarDados
{
    public class AtualizarDadosRequestModel
    {
        public string? Nome { get; set; }

        public string? Sobrenome { get; set; }

        public string? Email { get; set; }

        public string? Telefone { get; set; }

        public byte[]? FotoPerfil { get; set; }
    }
}
