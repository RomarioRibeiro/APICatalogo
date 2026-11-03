using ApiCatalogo.Context;
using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(IUnitOfWork unitOfWork, ILogger<CategoriasController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }   

        [HttpGet]
        [ServiceFilter(typeof(ApiLogginFilter))]
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {         
            var categorias = _unitOfWork.CategoriaRepository.GetAll();
            if (categorias is null)
                {
                _logger.LogWarning($"Categorias não encontradas...");
                    return NotFound("Categorias não encontradas...");
                }
            var categoriasDTO = categorias.ToCategoriaDTOList();
            return Ok(categoriasDTO);                  
        }


        [HttpGet("{id:int}", Name = "ObterCategorias")]
        public ActionResult<CategoriaDTO> Get(int id)
        {
            var categoria =  _unitOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound("Categoria não encontrada");
            }

            var categoriaDTO = categoria.ToCategoriaDTO();
            return Ok(categoriaDTO);
        }

        [HttpPost]
        public ActionResult Post(CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null)
            {
                _logger.LogWarning($"Categoria inválida...");
                return BadRequest("Dados Inválidos");
            }

            var categoria = categoriaDTO.ToCategoria();
            var categoriaCriada = _unitOfWork.CategoriaRepository.Create(categoria);
            _unitOfWork.Commit();

            var categoriaCriadaDTO = categoria.ToCategoriaDTO();  
            return new CreatedAtRouteResult("ObterCategorias", new { id = categoriaCriadaDTO.CategoriaId }, categoriaCriadaDTO);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, CategoriaDTO categoriaDTO)
        {
            if (id != categoriaDTO.CategoriaId)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return BadRequest("Dados Inválidos");
            }
            var categoria = categoriaDTO.ToCategoria();
            var categoriaAlterada = _unitOfWork.CategoriaRepository.Update(categoria);
            _unitOfWork.Commit();

            var categoriaAlteradaDTO = categoria.ToCategoriaDTO();
            return Ok(categoriaAlteradaDTO);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _unitOfWork.CategoriaRepository.GetById(c => c.CategoriaId == id);
            if (categoria is null)
            {
                _logger.LogWarning($"Categoria com id={id} não encontrada...");
                return NotFound($"Categoria com id: {id} não encontrada...");
            }
            _unitOfWork.CategoriaRepository.Delete(categoria);
            _unitOfWork.Commit();
            var categoriaDTO = categoria.ToCategoriaDTO();
            return NoContent(); //Ok(categoria);  
        }
    }
}
    