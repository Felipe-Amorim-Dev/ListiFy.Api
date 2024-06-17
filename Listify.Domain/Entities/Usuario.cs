using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Domain.Entities
{
    public class Usuario
    {
        #region Atributos
        private Guid? _id;
        private string? _nome;
        private string? _sobrenome;
        private string? _email;
        private DateTime? _dataNascimento;
        private string? _telefone;
        private string? _senha;
        private byte[]? _fotoPerfil;
        private DateTime? _dataCriacao;
        private DateTime? _dataAlteracao;
        private List<Item> _items;
        private string? _accessToken;
        #endregion
        #region Métodos
        public Guid? Id { get => _id; set => _id = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public string? Sobrenome { get => _sobrenome; set => _sobrenome = value; }
        public string? Email { get => _email; set => _email = value; }
        public DateTime? DataNascimento { get => _dataNascimento; set => _dataNascimento = value; }
        public string? Telefone { get => _telefone; set => _telefone = value; }
        public string? Senha { get => _senha; set => _senha = value; }
        public byte[]? FotoPerfil { get => _fotoPerfil; set => _fotoPerfil = value; }
        public DateTime? DataCriacao { get => _dataCriacao; set => _dataCriacao = value; }
        public DateTime? DataAlteracao { get => _dataAlteracao; set => _dataAlteracao = value; }
        public List<Item> Items { get => _items; set => _items = value; }
        public string? AccessToken { get => _accessToken; set => _accessToken = value; }
        #endregion
    }
}
