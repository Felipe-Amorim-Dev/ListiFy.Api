using Listify.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Services.Models.CadastrarItem
{
    public class CadastrarItemResponseModel
    {        
        public string? Titulo { get; set; }      
        public DateTime? DataCriacao { get; set; }
        public List<ItemFoto>? Galeria { get; set; }
    }
}
