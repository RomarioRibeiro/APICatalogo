using ApiCatalogo.Context;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
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
        private readonly AppDbContext _context;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(AppDbContext context, ILogger<CategoriasController> logger)
        {
            _context = context;
            _logger = logger;
        }   

        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public ActionResult<IEnumerable<CategoriaModel>> Get()
        {         
                var categorias = _context.Categorias.AsNoTracking().ToList();              
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
            return _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToList();
        }

        [HttpGet("{id:int}", Name = "ObterCategorias")]
        public ActionResult<CategoriaModel> Get(int id)
        {
            var categoria =  _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);         
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
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return new CreatedAtRouteResult("ObterCategorias", new { id = categoria.CategoriaId }, categoria);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CategoriaModel categoria)
        {
            if (id != categoria.CategoriaId)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return BadRequest("Dados Inválidos");
            }
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound("Categoria não encontrada...");
            }
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return NoContent(); //Ok(categoria);  
        }
    }
}
    