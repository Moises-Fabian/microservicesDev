using MongoDB.Driver;
using Servicios.api.Libreria.Core.ContextMongoDB;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{
    public class AutorRepository : IAutorRepository
    {
       /*Variable global*/
        private readonly IAutorContext _autorContext;
        /*inyectar método que se conecta a la bd extrayendo la data*/
        /*Para que me devuelva un arreglo de autores*/
        public AutorRepository(IAutorContext autorContext){
            _autorContext = autorContext;
        }
        
        public async Task<IEnumerable<Autor>> GetAutores()
        {
            /*Función para que devuelva todo*/
            return await _autorContext.Autores.Find(p => true).ToListAsync();
        }
    }
}
