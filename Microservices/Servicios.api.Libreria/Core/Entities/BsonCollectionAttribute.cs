using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*Esta entidad representa los nombres de la coleccion para cada documento generico*/

namespace Servicios.api.Libreria.Core.Entities
{
    /*notacion para indicar el attribute*/
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]

    /*Se realiza esto ya que se trabaja con documentos genericos*/
    public class BsonCollectionAttribute : Attribute //método para ingresar el nombre del documento a trabajar
    {
        /*Propiedad*/
        public string CollectionName { get; }

        /*Constructor para inyectar el CollectioName*/
        public  BsonCollectionAttribute(string collectionName)
        {
            CollectionName = collectionName;
        }
    }
}
