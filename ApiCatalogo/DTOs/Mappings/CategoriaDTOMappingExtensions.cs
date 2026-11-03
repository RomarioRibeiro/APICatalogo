using ApiCatalogo.Models;
using System.Runtime.CompilerServices;

namespace ApiCatalogo.DTOs.Mappings
{
    public static class CategoriaDTOMappingExtensions
    {
        public static CategoriaDTO? ToCategoriaDTO(this CategoriaModel categoria)
        {
            if (categoria is null)
            {
                return null;
            }
            return new CategoriaDTO
            {
                CategoriaId = categoria.CategoriaId,
                Nome = categoria.Nome,
                ImagemUrl = categoria.ImagemUrl
            };
        }
        public static CategoriaModel? ToCategoria(this CategoriaDTO categoriaDTO)
        {
            if (categoriaDTO is null)
            {
                return null;
            }
            return new CategoriaModel
            {
                CategoriaId = categoriaDTO.CategoriaId,
                Nome = categoriaDTO.Nome,
                ImagemUrl = categoriaDTO.ImagemUrl
            };
        }

        public static IEnumerable<CategoriaDTO> ToCategoriaDTOList(this IEnumerable<CategoriaModel> categorias)
        {
            if (categorias is null || !categorias.Any())
            {
                return new List<CategoriaDTO>();
            }
            return categorias.Select(c => new CategoriaDTO
            {
                CategoriaId = c.CategoriaId,
                Nome = c.Nome,
                ImagemUrl = c.ImagemUrl
            }).ToList();
        }
    }
}
