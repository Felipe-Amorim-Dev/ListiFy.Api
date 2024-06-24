using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Entities
{
    public class Item
    {
        #region Atributos
        private Guid? _id;
        private string? _titulo;
        private string? _descricao;
        private string? _categoria;
        private string? _tipo;
        private DateTime? _dataLancamento;
        private DateTime? _dataCriacao;
        private List<ItemFoto> _galeria;        
        private Guid? _usuarioID;
        private Usuario? _usuario;
        #endregion
        #region Métodos
        public Guid? Id { get => _id; set => _id = value; }
        public string? Titulo { get => _titulo; set => _titulo = value; }
        public string? Descricao { get => _descricao; set => _descricao = value; }
        public string? Categoria { get => _categoria; set => _categoria = value; }
        public string? Tipo { get => _tipo; set => _tipo = value; }
        public DateTime? DataLancamento { get => _dataLancamento; set => _dataLancamento = value; }
        public DateTime? DataCriacao { get => _dataCriacao; set => _dataCriacao = value; }
        public List<ItemFoto>? Galeria { get => _galeria; set => _galeria = value; }
        public Guid? UsuarioID { get => _usuarioID; set => _usuarioID = value; }
        public Usuario? Usuario { get => _usuario; set => _usuario = value; }
        #endregion
    }
}
