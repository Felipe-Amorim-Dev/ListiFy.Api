using Listify.Application.Interfaces;
using Listify.Application.Models.AtualizarDados;
using Listify.Application.Models.AtualizarEmail;
using Listify.Application.Models.AtualizarSenha;
using Listify.Application.Models.Autenticar;
using Listify.Application.Models.CriarContaUsuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Listify.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListiFyUsuarioController : ControllerBase
    {
        private readonly IListifyAppService _listifyAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ListiFyUsuarioController(IListifyAppService listifyAppService, IWebHostEnvironment webHostEnvironment)
        {
            _listifyAppService = listifyAppService;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("criar-conta-usuario")]
        [HttpPost]
        [ProducesResponseType(typeof(CriarContaUsuarioResponseModel), 200)]
        public async Task<IActionResult> CriarContaUsuario([FromForm] CriarContaUsuarioRequestModel model, IFormFile fotoPerfil)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await fotoPerfil.CopyToAsync(memoryStream);
                    model.FotoPerfil = memoryStream.ToArray();
                }

                var response = await _listifyAppService?.CriarContaUsuario(model);
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
                var response = await _listifyAppService.Autenticar(model);
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

                if (fotoPerfil != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fotoPerfil.CopyToAsync(memoryStream);
                        model.FotoPerfil = memoryStream.ToArray();
                    }
                }
                var response = await _listifyAppService.AtualizarDados(model, email);
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

                var response = await _listifyAppService.AtualizarEmail(model, email);
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

                var response = await _listifyAppService.AtualizarSenha(model, email);
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
    }
}
