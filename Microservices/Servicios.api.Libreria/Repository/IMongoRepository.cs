using MongoDB.Driver;
using Servicios.api.Libreria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Repository
{
    /*metodos para procesar las clases que vienen de document*/

    /*Representa el valor de una clase que se tiene que condicionar (IDocument)*/
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {

        /*Alistar todo los documentos de la collection
-Cuando se ejecute al imongo repository puede instanciar cualquier
collection de la data*/

        Task<IEnumerable<TDocument>> GetAll();
        /*OPERACIONES GENERICAS QUE TODA ENTIDAD DEBE TENER*/

        /*método que me permite devolver un documento solo con el id*/
        Task<TDocument> GetbyId(string id);
        /*método para insertar un nuevo documento*/
        Task InsertDocument(TDocument document);
        /*actualizar un documento*/
        Task UpdateDocument(TDocument document);
        /*eliminar un documento*/
        Task DeleteByID(string id);

        Task<PaginationEntity<TDocument>> PaginationBy(
            Expression<Func<TDocument, bool>> filterExpression,
            PaginationEntity<TDocument> pagination
            );

        Task<PaginationEntity<TDocument>> PaginationByFilter(
          PaginationEntity<TDocument> pagination
          );
    }

}
