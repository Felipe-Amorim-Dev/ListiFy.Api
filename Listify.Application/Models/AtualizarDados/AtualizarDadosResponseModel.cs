using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Application.Models.AtualizarDados
{
    public class AtualizarDadosResponseModel
    {
        public Guid? Id { get; set; }
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Email { get; set; }                
        public DateTime? DataHoraAlteracao { get; set; }
    }
}
