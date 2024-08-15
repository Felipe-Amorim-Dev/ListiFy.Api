namespace Listify.Services.Models.Usuario
{
    public class UsuarioResponseModel
    {        
        public string? Nome { get; set; }
        public string? Sobrenome { get; set; }
        public string? Email { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public byte[] FotoPerfil { get; set; }
    }
}
