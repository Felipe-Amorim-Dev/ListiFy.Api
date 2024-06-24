using System.ComponentModel.DataAnnotations;

namespace Listify.Services.Models.ConsultarUsuario
{
    public class ConsultarUsuarioResponseModel
    {
        public string? Nome { get; set; }
   
        public string? Sobrenome { get; set; }
       
        public string? Email { get; set; }              
       
        public DateTime? DataNascimento { get; set; }
        
        public string? Telefone { get; set; }

        public byte[]? FotoPerfil { get; set; }
    }
}
