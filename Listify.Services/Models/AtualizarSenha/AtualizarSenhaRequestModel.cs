using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.AtualizarSenha
{
    public class AtualizarSenhaRequestModel
    {              
        public string? Senha { get; set; }

        [Compare("Senha", ErrorMessage = "Confirme a senha de usuário.")]
        public string? SenhaConfirmacao { get; set; }
    }
}
