using Listify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Application.Models.AtualizarItem
{
    public class AtualizarItemResponseModel
    {
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Categoria { get; set; }
        public string? Tipo { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public List<ItemFoto>? Galeria { get; set; }
    }
}
