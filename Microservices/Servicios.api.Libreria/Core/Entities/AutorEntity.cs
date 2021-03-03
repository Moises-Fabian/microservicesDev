using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{
    /*Al hacer esta herencia automaticamente se implemente las propiedades de id y
     * detetime(creacion del documento)*/
    [BsonCollection("Autor")]/*match para compatibilizar la bd*/
    public class AutorEntity:Document
    {
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("apellido")]
        public string Apellido { get; set; }
        [BsonElement("gradoAcademico")]
        public string GradoAcademico { get; set; }
    }
}
