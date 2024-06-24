using Listify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.CadastrarItem
{
    public class CadastrarItemRequestModel
    {
        [Required(ErrorMessage = "Informe um Titulo.")]
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Informe a categoria.")]
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "Informe o tipo.")]
        public string? Tipo { get; set; }
        public DateTime? DataLancamento { get; set; }        
        public List<ItemFoto>? Galeria { get; set; }        
    }
}
