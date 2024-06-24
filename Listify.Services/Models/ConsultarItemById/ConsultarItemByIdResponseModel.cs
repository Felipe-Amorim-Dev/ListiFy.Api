using Listify.Domain.Entities;

namespace Listify.Services.Models.ConsultarItemById
{
    public class ConsultarItemByIdResponseModel
    {
        public string? Titulo {  get; set; }
        public string? Descricao { get; set; }
        public string? Categoria { get; set; }
        public string? Tipo { get; set; }
        public DateTime? DataLancamento { get; set; }
        public List<ItemFoto>? Galeria { get; set; }
    }
}
