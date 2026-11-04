using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProdutosController> _logger; 
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, ILogger<ProdutosController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get()
        {
            var produtos = _unitOfWork.ProdutoRepository.GetAll();
            if (produtos is null)
            {
                _logger.LogWarning("Produtos não encontrados....");
                return NotFound("Produtos não encontrados...");
            }
            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProdutos")]
        public ActionResult<ProdutoDTO> Get(int id)
        {
            var produto =  _unitOfWork.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
            {
                _logger.LogWarning($"Produto do codigo {id} não encontrado....");
               return NotFound(string.Format($"Produto do codigo {id} não encontrado....."));
            }
            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produto);
        }
        [HttpPost]
        public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null)
            {
                _logger.LogWarning("Dados Invalidos..");
                return BadRequest("Dados Invalidos...");
            }
            var produto = _mapper.Map<ProdutoModel>(produtoDTO);
            var novoProduto =  _unitOfWork.ProdutoRepository.Create(produto);
            _unitOfWork.Commit();   

            var novoProdutoDTO = _mapper.Map<ProdutoDTO>(novoProduto);
            return new CreatedAtRouteResult("ObterProdutos",new { id = novoProdutoDTO.ProdutoId }, novoProdutoDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDTO)
        {
            var produtoID = _unitOfWork.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if (id != produtoDTO.ProdutoId)
            {
                _logger.LogWarning($"O codigo do produto {produtoDTO.ProdutoId} esta diferento do corpo da requição {id} ");
                return BadRequest($"O codigo do produto {produtoDTO.ProdutoId} esta diferento do corpo da requição {id} ");
            }else if (produtoID is null)
            {
                _logger.LogWarning($"Codigo do produto {id} não encontrado.");
                return BadRequest($"Codigo do produto {id} não encontrado.");
            }

           var produto =  _mapper.Map<ProdutoModel>(produtoDTO);

            var produtoAtualizado =  _unitOfWork.ProdutoRepository.Update(produto);
            _unitOfWork.Commit();

            var produtoAtualizadoDTO = _mapper.Map<ProdutoDTO>(produtoAtualizado);
            return Ok(produtoAtualizadoDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<ProdutoDTO> Delete(int id)
        {
          var deletado = _unitOfWork.ProdutoRepository.GetById(p => p.ProdutoId == id);
           
            if (deletado is null)
                {
                    _logger.LogWarning($"Produto do codigo {id} não encontrado....");
                    return NotFound($"Produto do codigo {id} não encontrado.....");
                }
                _unitOfWork.ProdutoRepository.Delete(deletado);
                _unitOfWork.Commit();
            return NoContent();
        }

        [HttpGet("categorias/{categoriaId:int}")]
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorCategoria(int categoriaId)
        {
            var produtos = _unitOfWork.ProdutoRepository.GetProdutosPorCategoria(categoriaId);
            if (produtos is null)
            {
                _logger.LogWarning($"Produtos da categoria {categoriaId} não encontrados....");
                return NotFound($"Produtos da categoria {categoriaId} não encontrados.....");
            }

            var produtosDTO = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
            return Ok(produtosDTO);
        }

        [HttpPatch("{id:int}/UpdatePartial")]
        public ActionResult<ProdutoDTOUpdateResponse> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
        {
            if (patchProdutoDTO is null || id <= 0)
            {
                _logger.LogWarning("Dados Invalidos..");
                return BadRequest("Dados Invalidos...");
            }
            
            var produto = _unitOfWork.ProdutoRepository.GetById(p => p.ProdutoId == id);

            if(produto is null)
            {
                _logger.LogWarning($"Produto do codigo {id} não encontrado....");
                return NotFound($"Produto do codigo {id} não encontrado.....");
            }

            var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);

            patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);
            if(!ModelState.IsValid || !TryValidateModel(produtoUpdateRequest))
            {
                _logger.LogWarning("Dados Invalidos..");
                return BadRequest(ModelState);
            }

            _mapper.Map(produtoUpdateRequest, produto);
            _unitOfWork.ProdutoRepository.Update(produto);
            return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
        }

    }
}
