using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.Autenticar
{
    public class AutenticarRequestModel
    {
        [Required(ErrorMessage = "Informe o email de usuário.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Informe a senha de usuário.")]
        public string? Senha { get; set; }
    }
}
