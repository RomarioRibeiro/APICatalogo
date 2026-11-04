using ApiCatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTOs
{
    public class UtilsDTOMappingProfile : Profile
    {

        public UtilsDTOMappingProfile()
        {
            CreateMap<ProdutoModel, ProdutoDTO>().ReverseMap();
            CreateMap<CategoriaModel, CategoriaDTO>().ReverseMap();
        }
    }
}
