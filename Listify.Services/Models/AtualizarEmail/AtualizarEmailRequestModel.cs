using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.AtualizarEmail
{
    public class AtualizarEmailRequestModel
    {
        public string? Email { get; set; }

        public string? NovoEmail { get; set; }
    }
}
