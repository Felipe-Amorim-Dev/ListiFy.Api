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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listify.Application.Interfaces
{
    public interface IListifyAppService
    {
        Task<CriarContaUsuarioResponseModel> CriarContaUsuario(CriarContaUsuarioRequestModel model);

        Task<AutenticarResponseModel> Autenticar(AutenticarRequestModel model);

        Task<AtualizarDadosResponseModel> AtualizarDados(AtualizarDadosRequestModel model, string? email);

        Task<AtualizarSenhaResponseModel> AtualizarSenha(AtualizarSenhaRequestModel model, string? email);

        Task<AtualizarEmailResponseModel> AtualizarEmail(AtualizarEmailRequestModel model, string? email);

        Task<CadastrarItemResponseModel> CadastrarItem(CadastrarItemRequestModel model);

        Task<AtualizarItemResponseModel> AtualizarItem(AtualizarItemRequestModel model, string? titulo);

        Task<RemoverItemResponseModel> RemoverItem(RemoverItemRequestModel model, string? titulo);

        Task<ConsultarItemsResponseModel> ConsultarItems(ConsultarItemsRequestModel model, Guid? usuarioID);
    }
}
