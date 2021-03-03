using MongoDB.Driver;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.ContextMongoDB
{
    /*Contexto para poder realizar las acciones en la base de datos mongodb y me va a devolver la lista de autores*/
    public interface IAutorContext
    {
        /*Método que devulve la lista de autores creada anteriormente*/
        IMongoCollection<Autor> Autores { get; }
    }
}
