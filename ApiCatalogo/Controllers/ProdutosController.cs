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
            var produtos =  _produtoRepository.GetProdutos().ToList();
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
            var produto =  _produtoRepository.GetProduto(id);
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

            var atualizado =  _produtoRepository.Update(produto);

            if(atualizado)
            {                
                return Ok(produto);
            }
            else
            {
                _logger.LogWarning($"Produto do codigo {id} não encontrado....");
                return NotFound(string.Format($"Produto do codigo {id} não encontrado..."));
            }          
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
          var deletado = _produtoRepository.Delete(id);
            if(deletado)
            {
                return Ok($"Produto do codigo {id} deletado com sucesso...");
            }
            else
            {
                _logger.LogWarning($"Produto do codigo {id} não encontrado....");
                return NotFound($"Produto do codigo {id} não encontrado...");
            }
        }
    }
}
