using Listify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.ConsultarItems
{
    public class ConsultarItemsResponseModel
    {
        public List<ItemModel> Items { get; set; }
    }

    public class ItemModel
    {
        public Guid? Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public string? Categoria { get; set; }
        public string? Tipo { get; set; }
        public DateTime? DataLancamento { get; set; }
        public DateTime? DataCriacao { get; set; }
        public List<ItemFoto>? Galeria { get; set; }
    }
}

