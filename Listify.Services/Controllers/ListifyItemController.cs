using Listify.Application.Interfaces;
using Listify.Application.Models.AtualizarItem;
using Listify.Application.Models.CadastrarItem;
using Listify.Application.Models.ConsultarItems;
using Listify.Application.Models.CriarContaUsuario;
using Listify.Application.Models.RemoverItem;
using Listify.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Listify.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListifyItemController : ControllerBase
    {
        private readonly IListifyAppService _listifyAppService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ListifyItemController(IListifyAppService listifyAppService, IWebHostEnvironment webHostEnvironment)
        {
            _listifyAppService = listifyAppService;
            _webHostEnvironment = webHostEnvironment;
        }

        [Route("cadastrar-item")]
        [HttpPost]
        [ProducesResponseType(typeof(CadastrarItemResponseModel), 200)]
        public async Task<IActionResult> CadastrarItem([FromForm] CadastrarItemRequestModel model, List<IFormFile> galeria)
        {
            try
            {
                model.Galeria = new List<ItemFoto>();

                foreach (var file in galeria)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var itemFoto = new ItemFoto
                        {                            
                            Foto = memoryStream.ToArray()
                        };
                        model.Galeria.Add(itemFoto);
                    }
                }


                var response = await _listifyAppService?.CadastrarItem(model);
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

        [Route("atualizar-item")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarItemResponseModel), 200)]
        public async Task<IActionResult> AtualizarItem([FromForm] AtualizarItemRequestModel model, List<IFormFile> galeria)
        {
            try
            {                
                model.Galeria = new List<ItemFoto>();

                foreach (var file in galeria)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var itemFoto = new ItemFoto
                        {
                            Foto = memoryStream.ToArray()
                        };
                        model.Galeria.Add(itemFoto);
                    }
                }


                var response = await _listifyAppService?.AtualizarItem(model, model.Titulo);
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

        [Route("remover-item")]
        [HttpDelete]
        [ProducesResponseType(typeof(RemoverItemResponseModel), 200)]
        public async Task<IActionResult> RemoverItem([FromForm] RemoverItemRequestModel model)
        {
            try
            {                
                var response = await _listifyAppService?.RemoverItem(model, model.Titulo);
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

        [Route("consultar-item")]
        [HttpGet]
        [ProducesResponseType(typeof(ConsultarItemsResponseModel), 200)]
        public async Task<IActionResult> ConsultarItems([FromForm] ConsultarItemsRequestModel model)
        {
            try
            {
                var response = await _listifyAppService?.ConsultarItems(model);
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
