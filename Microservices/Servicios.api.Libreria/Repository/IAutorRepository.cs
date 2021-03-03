using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{
    public interface IAutorRepository
    {
        /*método para que me devuelva una lista de autores*/
        Task<IEnumerable<Autor>> GetAutores();
    }
}
