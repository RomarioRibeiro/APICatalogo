using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {

        private readonly IProdutoRepository _produtoRepository;
        private readonly ILogger<ProdutosController> _logger; 

        public ProdutosController(IProdutoRepository produtoRepository, ILogger<ProdutosController> logger)
        {
            _produtoRepository = produtoRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoModel>> Get()
        {
            var produtos = _produtoRepository.GetAll();
            if (produtos is null)
            {
                _logger.LogWarning("Produtos não encontrados....");
                return NotFound("Produtos não encontrados...");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProdutos")]
        public ActionResult<ProdutoModel> Get(int id)
        {
            var produto =  _produtoRepository.GetById(p => p.ProdutoId == id);
            if (produto is null)
            {
                _logger.LogWarning($"Produto do codigo {id} não encontrado....");
               return NotFound(string.Format($"Produto do codigo {id} não encontrado....."));
            }
            return Ok(produto);
        }
        [HttpPost]
        public ActionResult Post(ProdutoModel produto)
        {
            if (produto is null)
            {
                _logger.LogWarning("Dados Invalidos..");
                return BadRequest("Dados Invalidos...");
            }
           var novoProduto =  _produtoRepository.Create(produto);
            return new CreatedAtRouteResult("ObterProdutos",new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoModel produto)
        {
            if (id != produto.ProdutoId)
            {
                _logger.LogWarning("Dados Invalidos..");
                return BadRequest("Dado Invalidos..");
            }

            var produtoAtualizado =  _produtoRepository.Update(produto);

           return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
          var deletado = _produtoRepository.GetById(p => p.ProdutoId == id);
           
            if (deletado is null)
                {
                    _logger.LogWarning($"Produto do codigo {id} não encontrado....");
                    return NotFound($"Produto do codigo {id} não encontrado.....");
                }
                _produtoRepository.Delete(deletado);
                return NoContent();
        }

        [HttpGet("categorias/{categoriaId:int}")]
        public ActionResult<IEnumerable<ProdutoModel>> GetProdutosPorCategoria(int categoriaId)
        {
            var produtos = _produtoRepository.GetProdutosPorCategoria(categoriaId);
            if (produtos is null)
            {
                _logger.LogWarning($"Produtos da categoria {categoriaId} não encontrados....");
                return NotFound($"Produtos da categoria {categoriaId} não encontrados.....");
            }
            return Ok(produtos);
        }

    }
}
