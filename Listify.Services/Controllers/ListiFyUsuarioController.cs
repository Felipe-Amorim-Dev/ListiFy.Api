using Listify.Domain.Entities;
using Listify.Domain.Interfaces.Services;
using Listify.Services.Models.AtualizarDados;
using Listify.Services.Models.AtualizarEmail;
using Listify.Services.Models.AtualizarSenha;
using Listify.Services.Models.Autenticar;
using Listify.Services.Models.ConsultarUsuario;
using Listify.Services.Models.CriarContaUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Listify.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListiFyUsuarioController : ControllerBase
    {        
        private readonly IUsuarioDomainService _usuarioDomainService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ListiFyUsuarioController(IUsuarioDomainService usuarioDomainService, IWebHostEnvironment webHostEnvironment)
        {
            _usuarioDomainService = usuarioDomainService;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("criar-conta-usuario")]
        [HttpPost]
        [ProducesResponseType(typeof(CriarContaUsuarioResponseModel), 200)]
        public async Task<IActionResult> CriarContaUsuario([FromForm] CriarContaUsuarioRequestModel model, IFormFile fotoPerfil)
        {
            try
            {
                var usuario = new Usuario
                {
                    Id = Guid.NewGuid(),
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Email = model.Email,
                    DataNascimento = model.DataNascimento,
                    Telefone = model.Telefone,
                    Senha = model.Senha,
                    FotoPerfil = model.FotoPerfil,
                    DataCriacao = DateTime.Now
                };

                using (var memoryStream = new MemoryStream())
                {
                    await fotoPerfil.CopyToAsync(memoryStream);
                    model.FotoPerfil = memoryStream.ToArray();
                }

                await _usuarioDomainService.CriarContaUsuario(usuario);

                var response = new CriarContaUsuarioResponseModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    DataCriacao = usuario.DataCriacao
                };                

                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [Route("autenticar")]
        [HttpPost]
        [ProducesResponseType(typeof(AutenticarResponseModel), 200)]
        public async Task<IActionResult> Autenticar([FromBody] AutenticarRequestModel model)
        {
            try
            {
                var autenticar = new AutenticarRequestModel
                {
                    Email = model.Email,
                    Senha = model.Senha
                };

                await _usuarioDomainService.Autenticar(model.Email, model.Senha);

                var response = new AutenticarResponseModel
                {
                    DataHoraAcesso = DateTime.Now                   
                };

                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize]
        [Route("atualizar-dados")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarDadosResponseModel), 200)]
        public async Task<IActionResult> AtualizarDados([FromForm] AtualizarDadosRequestModel model, IFormFile fotoPerfil)
        {
            try
            {
                var email = User.Identity.Name;

                var usuario = new AtualizarDadosRequestModel
                {
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone,
                    FotoPerfil = model.FotoPerfil
                };

                if (fotoPerfil != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fotoPerfil.CopyToAsync(memoryStream);
                        model.FotoPerfil = memoryStream.ToArray();
                    }
                }

                await _usuarioDomainService.AtualizarDados(email, usuario.Nome, usuario.Sobrenome, usuario.Telefone, usuario.FotoPerfil);

                var response = new AtualizarDadosResponseModel
                {
                    DataHoraAlteracao = DateTime.Now
                };

                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize]
        [Route("atualizar-email")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarEmailResponseModel), 200)]
        public async Task<IActionResult> AtualizarEmail([FromBody] AtualizarEmailRequestModel model)
        {
            try
            {
                var email = User.Identity.Name;

                var emailAtualizado = new AtualizarEmailRequestModel
                {
                    Email = model.Email
                };

                await _usuarioDomainService.AtualizarEmail(email, emailAtualizado.Email);

                var response = new AtualizarEmailResponseModel
                {
                    DataHoraAlteracao = DateTime.Now
                };

                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize]
        [Route("atualizar-senha")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarSenhaResponseModel), 200)]
        public async Task<IActionResult> AtualizarSenha([FromBody] AtualizarSenhaRequestModel model)
        {
            try
            {
                var email = User.Identity.Name;

                var senhaAtualizada = new AtualizarSenhaRequestModel
                {
                    Senha = model.Senha
                };

                await _usuarioDomainService.AtualizarSenha(email, senhaAtualizada.Senha);

                var response = new AtualizarSenhaResponseModel 
                {
                    DataHoraAlteracao = DateTime.Now
                };

                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        [Authorize]
        [Route("consultar-usuario")]
        [HttpGet]
        [ProducesResponseType(typeof(ConsultarUsuarioResponseModel), 200)]
        public async Task<IActionResult> GetUsuario([FromQuery] ConsultarUsuarioRequestModel model)
        {
            try
            {
                var email = User.Identity.Name;

                model.Email = email;

                var usuario = await _usuarioDomainService.GetUsuario(model.Email);

                var response = new ConsultarUsuarioResponseModel
                {
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
                    DataNascimento = usuario.DataNascimento,
                    Telefone = usuario.Telefone,
                    FotoPerfil = usuario.FotoPerfil
                };

                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
        
        [Route("deletar-usuario")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DeletarUsuario([FromQuery] Guid usuarioID)
        {
            try
            {                             
                await _usuarioDomainService.DeletarUsuario(usuarioID);

                return StatusCode(200, new { Message = "Sua conta foi excluida com sucesso." });
            }
            catch (ApplicationException e)
            {
                return StatusCode(400, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
