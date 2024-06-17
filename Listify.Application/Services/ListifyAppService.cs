using Listify.Application.Interfaces;
using Listify.Application.Models.AtualizarDados;
using Listify.Application.Models.AtualizarEmail;
using Listify.Application.Models.AtualizarItem;
using Listify.Application.Models.AtualizarSenha;
using Listify.Application.Models.Autenticar;
using Listify.Application.Models.CadastrarItem;
using Listify.Application.Models.ConsultarItems;
using Listify.Application.Models.CriarContaUsuario;
using Listify.Application.Models.RemoverItem;
using Listify.Domain.Entities;
using Listify.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Application.Services
{
    public class ListifyAppService : IListifyAppService
    {
        private readonly IItemDomainService? _itemDomainservice;
        private readonly IUsuarioDomainService? _usuarioDomainService;

        public ListifyAppService(IItemDomainService? itemDomainservice, IUsuarioDomainService? usuarioDomainService)
        {
            _itemDomainservice = itemDomainservice;
            _usuarioDomainService = usuarioDomainService;
        }

        public async Task<AtualizarDadosResponseModel> AtualizarDados(AtualizarDadosRequestModel model, string email)
        {
            var usuario = await _usuarioDomainService?.AtualizarDados(email, model.Nome, model.Sobrenome, model.Telefone, model.FotoPerfil);

            var response = new AtualizarDadosResponseModel
            {
                Id = usuario.Id,
                Nome = model.Nome,
                Email = email,
                DataHoraAlteracao = DateTime.Now
            };

            return response;
        }

        public async Task<AtualizarEmailResponseModel> AtualizarEmail(AtualizarEmailRequestModel model, string email)
        {
            var usuario = await _usuarioDomainService?.AtualizarEmail(email, model.NovoEmail);

            var response = new AtualizarEmailResponseModel 
            {
                Id = usuario.Id,
                Email = email,
                DataHoraAlteracao = DateTime.Now
            };

            return response;

        }

        public async Task<AtualizarItemResponseModel> AtualizarItem(AtualizarItemRequestModel model, string titulo)
        {
            var item = await _itemDomainservice?.AtualizarItem(titulo, model.Descricao, model.Categoria, model.Tipo, model.Galeria);

            var response = new AtualizarItemResponseModel
            {
                Id = item.Id,
                Titulo = item.Titulo,
                Descricao = item.Descricao,
                Categoria = item.Categoria,
                Tipo = item.Tipo,
                Galeria = item.Galeria,
            };

            return response;
        }

        public async Task<AtualizarSenhaResponseModel> AtualizarSenha(AtualizarSenhaRequestModel model, string email)
        {
            var usuario = await _usuarioDomainService?.AtualizarSenha(email, model.Senha);

            var response = new AtualizarSenhaResponseModel
            {
                Id = usuario.Id,
                Email = email,
                Nome = usuario.Nome,
                DataHoraAlteracao = DateTime.Now
            };

            return response;
        }

        public async Task<AutenticarResponseModel> Autenticar(AutenticarRequestModel model)
        {
            var usuario = await _usuarioDomainService?.Autenticar(model.Email, model.Senha);

            var response = new AutenticarResponseModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                DataNascimento = usuario.DataNascimento,
                Telefone = usuario.Telefone,
                FotoPerfil = usuario.FotoPerfil,
                AccessToken = usuario.AccessToken,
                DataHoraAcesso = DateTime.Now,
                DataHoraExpiracao = DateTime.UtcNow.AddHours(2)
            };

            return response;
        }

        public async Task<CadastrarItemResponseModel> CadastrarItem(CadastrarItemRequestModel model)
        {
            var item = new Item 
            {
                Id = Guid.NewGuid(),
                Titulo = model.Titulo,
                Descricao = model.Descricao,
                Categoria = model.Categoria,
                Tipo = model.Tipo,
                DataLancamento = model.DataLancamento,
                DataCriacao = model.DataCriacao,
                Galeria = model.Galeria
            };

            await _itemDomainservice?.CadastrarItem(item);

            var response = new CadastrarItemResponseModel
            {
                Id = Guid.NewGuid(),
                Titulo = item.Titulo,
                Descricao = item.Descricao,
                Categoria = item.Categoria,
                Tipo = item.Tipo,
                DataCriacao = item.DataCriacao,
                Galeria= item.Galeria
            };

            return response;
        }

        public async Task<CriarContaUsuarioResponseModel> CriarContaUsuario(CriarContaUsuarioRequestModel model)
        {
            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Email = model.Email,
                Senha = model.Senha,
                DataNascimento = model.DataNascimento,                
                Telefone = model.Telefone,
                FotoPerfil = model.FotoPerfil,
                DataCriacao = DateTime.Now,
                DataAlteracao = DateTime.Now
            };

            await _usuarioDomainService?.CriarContaUsuario(usuario);

            var response = new CriarContaUsuarioResponseModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Sobrenome = usuario.Sobrenome,
                Email = usuario.Email,
                DataCriacao = usuario.DataCriacao
            };

            return response;
        }

        public async Task<RemoverItemResponseModel> RemoverItem(RemoverItemRequestModel model, string? titulo)
        {
            var item = await _itemDomainservice?.DeletarItem(titulo);

            var response = new RemoverItemResponseModel 
            {
                Id = item.Id,
                Titulo = titulo
            };

            return response;

        }

        public async Task<ConsultarItemsResponseModel> ConsultarItems(ConsultarItemsRequestModel model)
        {
            var items = await _itemDomainservice?.ConsultarItems();            

            return items;

        }
    }
}
