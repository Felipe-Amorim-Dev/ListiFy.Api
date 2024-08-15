using Listify.Domain.Entities;
using Listify.Domain.Interfaces.Repositories;
using Listify.Domain.Interfaces.Services;
using Listify.Services.Models.AtualizarItem;
using Listify.Services.Models.CadastrarItem;
using Listify.Services.Models.ConsultarItemById;
using Listify.Services.Models.ConsultarItems;
using Listify.Services.Models.RemoverItem;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Listify.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListifyItemController : ControllerBase
    {        
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IItemFotoRepository _itemFotoRepository;
        private readonly IItemDomainService _itemDomainService;
        private readonly IUsuarioRepository _usuarioRepository;

        public ListifyItemController(IWebHostEnvironment webHostEnvironment, IItemFotoRepository itemFotoRepository, IItemDomainService itemDomainService, IUsuarioRepository usuarioRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _itemFotoRepository = itemFotoRepository;
            _itemDomainService = itemDomainService;
            _usuarioRepository = usuarioRepository;
        }

        [Route("cadastrar-item")]
        [HttpPost]
        [ProducesResponseType(typeof(CadastrarItemResponseModel), 200)]
        public async Task<IActionResult> CadastrarItem([FromForm] CadastrarItemRequestModel model, Guid usuarioID, List<IFormFile> galeria)
        {

            try
            {
                if (galeria.Count > 5)
                {
                    return StatusCode(400, new { Message = "Você pode enviar no máximo 5 fotos." });
                }

                var item = new Item
                {
                    UsuarioID = usuarioID,
                    Id = Guid.NewGuid(),
                    Titulo = model.Titulo,
                    Descricao = model.Descricao,
                    Categoria = model.Categoria,
                    Tipo = model.Tipo,
                    DataLancamento = model.DataLancamento,
                    DataCriacao = DateTime.Now,
                    Galeria = new List<ItemFoto>()
                };

                await _itemDomainService?.CadastrarItem(item, usuarioID);

                foreach (var file in galeria)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        var itemFoto = new ItemFoto
                        {
                            Id = Guid.NewGuid(),
                            ItemId = item.Id,
                            Foto = memoryStream.ToArray()
                        };
                        item.Galeria.Add(itemFoto);

                        await _itemFotoRepository.CreateAsync(itemFoto);
                    }
                }

                var response = new CadastrarItemResponseModel
                {                    
                    Titulo = item.Titulo,
                    DataCriacao = item.DataCriacao,
                    Galeria = item.Galeria
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

        [Route("atualizar-item")]
        [HttpPut]
        [ProducesResponseType(typeof(AtualizarItemResponseModel), 200)]
        public async Task<IActionResult> AtualizarItem([FromForm] AtualizarItemRequestModel model, Guid usuarioID, Guid itemId, List<IFormFile> galeria)
        {
            try
            {
                var item = new Item();

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

                var response = new AtualizarItemResponseModel
                {
                    DataAlteracao = DateTime.Now
                };

                await _itemDomainService?.AtualizarItem(item, usuarioID, itemId, model.Titulo, model.Descricao, model.Categoria, model.Tipo, model.Galeria);
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
        public async Task<IActionResult> RemoverItem([FromQuery] Guid id, Guid usuarioID)
        {
            try
            {                
                var response = await _itemDomainService?.DeletarItem(id, usuarioID);
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
        public async Task<IActionResult> ConsultarItems([FromQuery] Guid usuarioID)
        {
            try
            {   
                var response = await _itemDomainService?.ConsultarItems(usuarioID);
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

        [Route("consultar-item-id")]
        [HttpGet]
        [ProducesResponseType(typeof(ConsultarItemByIdResponseModel), 200)]
        public async Task<IActionResult> ConsultarItemById(Guid usuarioId, Guid itemId)
        {
            try
            {
                var item = await _itemDomainService?.ConsultarItemById(usuarioId, itemId);

                if (item == null)
                {
                    return NotFound(new { Message = "Item não encontrado." });
                }

                var response = new ConsultarItemByIdResponseModel
                {
                    Titulo = item.Titulo,
                    Descricao = item.Descricao,
                    Categoria = item.Categoria,
                    Tipo = item.Tipo,
                    DataLancamento = item.DataLancamento,
                    Galeria = item.Galeria?.ToList()                    
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

        [Route("deletar-foto")]
        [HttpDelete]
        [ProducesResponseType(typeof(AtualizarItemResponseModel), 200)]
        public async Task<IActionResult> DeletarFoto(Guid itemId, Guid fotoId)
        {
            try
            {
               await _itemDomainService?.DeletarFoto(itemId, fotoId);                

               return StatusCode(200, new { Message = $"Foto {fotoId} deletada com sucesso." });

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

        [Route("deletar-fotos")]
        [HttpDelete]
        [ProducesResponseType(typeof(AtualizarItemResponseModel), 200)]
        public async Task<IActionResult> DeletarFotos(Guid itemId)
        {
            try
            {
                await _itemDomainService?.DeletarFotos(itemId);

                return StatusCode(200, new { Message = "Galeria deletada com sucesso." });

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
