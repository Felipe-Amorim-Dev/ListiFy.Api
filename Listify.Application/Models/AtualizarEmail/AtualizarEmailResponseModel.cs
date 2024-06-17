using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Application.Models.AtualizarEmail
{
    public class AtualizarEmailResponseModel
    {
        public Guid? Id { get; set; }
        public string? Email { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
    }
}
