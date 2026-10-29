using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoriaModel> GetCategorias()
        {
           return _context.Categorias.ToList();
        }
        public CategoriaModel GetCategoriaById(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        }
        public CategoriaModel Create(CategoriaModel categoria)
        {
            if(categoria is null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }
        public CategoriaModel Uptade(CategoriaModel categoria)
        {
            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();
            return categoria;
        }
 
        public CategoriaModel Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));
            
            _context.Categorias.Remove(categoria);
            _context.SaveChanges(); 
            return categoria;
        }      
    }
}
