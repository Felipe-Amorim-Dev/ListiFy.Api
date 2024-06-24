using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.Autenticar
{
    public class AutenticarResponseModel
    {      
        public DateTime? DataHoraAcesso { get; set; }
        public DateTime? DataHoraExpiracao { get; set; }
    }
}
