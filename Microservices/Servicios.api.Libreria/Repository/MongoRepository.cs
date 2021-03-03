using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Servicios.api.Libreria.Core;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{
    public class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : IDocument
    {
        /*INYECCION*/
        private readonly IMongoCollection<TDocument> _collection;
        /*INYECCION*/
        public MongoRepository(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var db = client.GetDatabase(options.Value.Database);

            _collection = db.GetCollection<TDocument>(GetCollectionName(typeof(TDocument)));
        }

        private protected string GetCollectionName(Type documentType)
        {
            return ((BsonCollectionAttribute)documentType.GetCustomAttributes(typeof(BsonCollectionAttribute), true).FirstOrDefault()).CollectionName;
        }

        /*Me retorna todos los registros de la collection*/
        public async Task<IEnumerable<TDocument>> GetAll()
        {
            return await _collection.Find(p=>true).ToListAsync();
        }
        /*Obtener un documento por id*/
        public async Task<TDocument> GetbyId(string id)
        {
            /*Busqueda de documento en mi coleccion por el id*/
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            /*busqueda por el filtro agregado anteriormente devolviendo un solo documento si existe*/
            return await _collection.Find(filter).SingleOrDefaultAsync();
        }
        /*Insertar nuevos documentos*/
        public async Task InsertDocument(TDocument document)
        {
            await _collection.InsertOneAsync(document);
        }
        /*actualizar documento*/
        public async Task UpdateDocument(TDocument document)
        {
            /*buscar el documento // se le pasa el id y el documento del id que se esta ingresando*/
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, document.Id);
            /*buscar y reemplazar documento*/
            await _collection.FindOneAndReplaceAsync(filter, document);
        }
        /*eliminar documento*/
        public async Task DeleteByID(string id)
        {
            var filter = Builders<TDocument>.Filter.Eq(doc => doc.Id, id);
            await _collection.FindOneAndDeleteAsync(filter);
        }
        /*metodo que devuelve un objeto tipo paginacion con ordenamiento*/
        public async Task<PaginationEntity<TDocument>> PaginationBy(Expression<Func<TDocument, bool>> filterExpression, PaginationEntity<TDocument> pagination)
        {
            var sort = Builders<TDocument>.Sort.Ascending(pagination.Sort);
            if (pagination.SortDirection == "desc")
            {
                /*metodo que va a organizar la collection*/
                sort = Builders<TDocument>.Sort.Descending(pagination.Sort);
            }

            if (string.IsNullOrEmpty(pagination.Filter))
            {
                pagination.Data = await _collection.Find(p => true)
                                   .Sort(sort)
                                   .Skip((pagination.Page - 1) * pagination.PageSize)/*permite fijar una posicion en la lista de collection y tomar elemente que vienen, y los anteriores los desechan*/
                                   .Limit(pagination.PageSize)/*se indica cuantos elemtos se quiere extraer del skip (consulta)*/
                                   .ToListAsync();
            }
            else
            {
                pagination.Data = await _collection.Find(filterExpression)
                                   .Sort(sort)
                                   .Skip((pagination.Page - 1) * pagination.PageSize)/*permite fijar una posicion en la lista de collection y tomar elemente que vienen, y los anteriores los desechan*/
                                   .Limit(pagination.PageSize)/*se indica cuantos elemtos se quiere extraer del skip (consulta)*/
                                   .ToListAsync();
            }
            /*se obtiene la cantidad de datos de una collection (total de records)*/
            long totalDocument = await _collection.CountDocumentsAsync(FilterDefinition<TDocument>.Empty);
            var totalPages = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(totalDocument / pagination.PageSize)));

            pagination.PagesQuantity = totalPages;

            return pagination;
        }

        public async Task<PaginationEntity<TDocument>> PaginationByFilter(PaginationEntity<TDocument> pagination)
        {
            var sort = Builders<TDocument>.Sort.Ascending(pagination.Sort);
            if (pagination.SortDirection == "desc")
            {
                /*metodo que va a organizar la collection*/
                sort = Builders<TDocument>.Sort.Descending(pagination.Sort);
            }

            var totalDocument = 0;
            if (pagination.FilterValue == null)
            {
                pagination.Data = await _collection.Find(p => true)
                                   .Sort(sort)
                                   .Skip((pagination.Page - 1) * pagination.PageSize)/*permite fijar una posicion en la lista de collection y tomar elemente que vienen, y los anteriores los desechan*/
                                   .Limit(pagination.PageSize)/*se indica cuantos elemtos se quiere extraer del skip (consulta)*/
                                   .ToListAsync();

                totalDocument = (await _collection.Find(p => true).ToListAsync()).Count();
            }
            else
            {
                /*para buscar cualquier valor que el usuario escriba mayuscula o minuscula*/
                var valueFilter = ".*" + pagination.FilterValue.valor + ".*";
                var filter = Builders<TDocument>.Filter.Regex(pagination.FilterValue.Propiedad, new MongoDB.Bson.BsonRegularExpression(valueFilter,"i"));
                pagination.Data = await _collection.Find(filter)
                                   .Sort(sort)
                                   .Skip((pagination.Page - 1) * pagination.PageSize)/*permite fijar una posicion en la lista de collection y tomar elemente que vienen, y los anteriores los desechan*/
                                   .Limit(pagination.PageSize)/*se indica cuantos elemtos se quiere extraer del skip (consulta)*/
                                   .ToListAsync();

                totalDocument = (await _collection.Find(filter).ToListAsync()).Count();
            }
            /*se obtiene la cantidad de datos de una collection (total de records)*/
            //long totalDocument = await _collection.CountDocumentsAsync(FilterDefinition<TDocument>.Empty);
            var rounded = Math.Ceiling(totalDocument / Convert.ToDecimal(pagination.PageSize));

            var totalPages = Convert.ToInt32(rounded);
            /*transformar a valor entero*/
            pagination.PagesQuantity = totalPages;
            /*representa la cantidad de records de la consulta*/
            pagination.totalRows = Convert.ToInt32(totalDocument);

            return pagination;
        }
    }
}
