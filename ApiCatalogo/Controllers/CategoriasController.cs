using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ICategoriaRepository categoriaRepository, ILogger<CategoriasController> logger)
        {
            _categoriaRepository = categoriaRepository;
            _logger = logger;
        }   

        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public ActionResult<IEnumerable<CategoriaModel>> Get()
        {         
                var categorias = _categoriaRepository.GetCategorias();
            if (categorias is null)
                {
                _logger.LogWarning($"Categorias não encontradas...");
                    return NotFound("Categorias não encontradas...");
                }
                return Ok(categorias);                  
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaModel>> GetCategoriaProdutos()
        {
            var categorias =  _categoriaRepository.GetCategoriaProduto();

            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategorias")]
        public ActionResult<CategoriaModel> Get(int id)
        {
            var categoria =  _categoriaRepository.GetCategoriaById(id);
            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound("Categoria não encontrada");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(CategoriaModel categoria)
        {
            if (categoria is null)
            {
                _logger.LogWarning($"Categoria inválida...");
                return BadRequest("Dados Inválidos");
            }
            var categoriaCriada = _categoriaRepository.Create(categoria);
            return new CreatedAtRouteResult("ObterCategorias", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CategoriaModel categoria)
        {
            if (id != categoria.CategoriaId)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return BadRequest("Dados Inválidos");
            }
            var categoriaAlterada = _categoriaRepository.Uptade(categoria);
            return Ok(categoriaAlterada);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _categoriaRepository.GetCategoriaById(id);
            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound($"Categoria com id: {id} não encontrada...");
            }
            _categoriaRepository.Delete(id);
            return NoContent(); //Ok(categoria);  
        }
    }
}
    