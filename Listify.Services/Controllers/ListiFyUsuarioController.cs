using Listify.Domain.Entities;
using Listify.Domain.Interfaces.Services;
using Listify.Domain.Services;
using Listify.Services.Models.AtualizarDados;
using Listify.Services.Models.AtualizarSenha;
using Listify.Services.Models.Autenticar;
using Listify.Services.Models.CriarContaUsuario;
using Listify.Services.Models.SendEmail;
using Listify.Services.Models.Usuario;
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
        private readonly SendEmailDomainService _emailService;

        public ListiFyUsuarioController(IUsuarioDomainService usuarioDomainService, IWebHostEnvironment webHostEnvironment, SendEmailDomainService emailService)
        {
            _usuarioDomainService = usuarioDomainService;
            _webHostEnvironment = webHostEnvironment;
            _emailService = emailService;
        }

        [Route("criar-conta-usuario")]
        [HttpPost]
        [ProducesResponseType(typeof(CriarContaUsuarioResponseModel), 200)]
        public async Task<IActionResult> CriarContaUsuario([FromForm] CriarContaUsuarioRequestModel model, IFormFile? fotoPerfil = null)
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
                    DataCriacao = DateTime.Now
                };

                if (fotoPerfil != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fotoPerfil.CopyToAsync(memoryStream);
                        usuario.FotoPerfil = memoryStream.ToArray();
                    }
                }
                
                await _usuarioDomainService.CriarContaUsuario(usuario);

                var response = new CriarContaUsuarioResponseModel
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    DataCriacao = usuario.DataCriacao                    
                };

                var sendEmailRequest = new SendEmailRequest
                {
                    ToEmail = usuario.Email,
                    Subject = "ListiFy - Criação de conta."
                };
                
                string htmlBody = $@"
                <html>
                   <head>
                        <style>body {{font-family: 'Roboto', sans-serif;background-color: #f5f5f5;color: #2f2f2f;margin: 0;padding: 0;line-height: 1.6;}}.container {{width: 100%;max-width: 600px;margin: 0 auto;padding: 20px;background-color: #ffffff;border-radius: 8px;box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);}}.header {{text-align: center;padding: 20px 0;background-color: #07342C;border-radius: 8px 8px 0 0;}}.header h1 {{margin: 0;color: #ffffff;}}.content {{padding: 20px;}}.content h2 {{color: #07342C;}}.content p {{margin-bottom: 20px;}}.button {{display: inline-block;padding: 10px 20px;background-color: #FFAB07;color: #ffffff;text-decoration: none;border-radius: 5px;font-weight: bold;transition: background-color 0.3s ease;}}.button:hover {{background-color: #AD7301;}}.footer {{text-align: center;padding: 20px;background-color: #f5f5f5;color: #2f2f2f;font-size: 14px;border-radius: 0 0 8px 8px;}}
                        </style>
                   </head>
    
                   <body>
                       <div class=""container"">
                           <div class=""header"">
                               <h1>Bem-vindo(a) ao ListiFy!</h1>
                           </div>
                           <div class=""content"">
                               <h2>Olá, {usuario.Nome}!</h2>
                               <p>Estamos muito felizes por você ter se juntado a nós. Sua conta foi criada com sucesso e agora você pode acessar todas as funcionalidades que o ListiFy tem a oferecer.</p>
                               <p>Para começar, clique no botão abaixo e faça seu login:</p>
                               <p>
                                   <a href=""[http://www.listify.com.br/login]"" class=""button"">Acessar Minha Conta</a>
                               </p>
                               <p>Obrigado por escolher o ListiFy!</p>
                               <p>Essa é uma mensagem automática, Porfavor não responda.</p>
                           </div>
                           <div class=""footer"">
                               <p>© 2024 ListiFy. Todos os direitos reservados.</p>
                           </div>
                       </div>
                   </body>
                </html>";


                await _emailService.SendEmailAsync(sendEmailRequest.ToEmail, sendEmailRequest.Subject, htmlBody);

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

                 var usuario = await _usuarioDomainService.Autenticar(model.Email, model.Senha);

                var response = new AutenticarResponseModel
                {
                    Id = (Guid)usuario.Id,
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
        
        [Route("atualizar-dados")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarDadosResponseModel), 200)]
        public async Task<IActionResult> AtualizarDados([FromQuery] Guid usuarioID, [FromForm] AtualizarDadosRequestModel? model, IFormFile? fotoPerfil)
        {            
            try
            {                               
                if (fotoPerfil != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await fotoPerfil.CopyToAsync(memoryStream);
                        model.FotoPerfil = memoryStream.ToArray();
                    }
                }

                await _usuarioDomainService.AtualizarDados(usuarioID, model?.Nome, model?.Sobrenome, model?.Email, model?.Telefone, model?.FotoPerfil);

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
        
        [Route("atualizar-senha")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarSenhaResponseModel), 200)]
        public async Task<IActionResult> AtualizarSenha([FromQuery] Guid usuarioID,[FromForm] AtualizarSenhaRequestModel model)
        {
            try
            {
                var senhaAtualizada = new AtualizarSenhaRequestModel
                {
                    Senha = model.Senha
                };

                await _usuarioDomainService.AtualizarSenha(usuarioID, senhaAtualizada.Senha);

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
        
        [Route("deletar-usuario")]
        [HttpDelete]
        [ProducesResponseType(typeof(string), 200)]
        public async Task<IActionResult> DeletarUsuario(Guid usuarioID)
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

        [Route("usuario")]
        [HttpGet]
        [ProducesResponseType(typeof(UsuarioResponseModel), 200)]
        public async Task<IActionResult> Usuario([FromQuery] Guid usuarioID)
        {
            try
            {                
                var usuario = await _usuarioDomainService.Usuario(usuarioID);                

                var response = new UsuarioResponseModel
                {
                    Nome = usuario.Nome,
                    Sobrenome = usuario.Sobrenome,
                    Email = usuario.Email,
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
    }
}
