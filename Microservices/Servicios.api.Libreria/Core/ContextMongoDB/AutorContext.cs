using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.ContextMongoDB
{
    /*Contexto para poder realizar las acciones en la base de datos mongodb y me va a devolver la lista de autores*/
    public class AutorContext : IAutorContext
    {
        /*-variable que representa la base de datos de mongo
         -acceso a la base de datos*/
        private readonly IMongoDatabase _db;

        /*en el constructor se llama a la clase que tiene las cadenas de conexión*/
        public AutorContext(IOptions<MongoSettings> options) {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        /*Para que devuelva la lista de autores*/
        public IMongoCollection<Autor> Autores => _db.GetCollection<Autor>("Autor");
    }
}
