using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core
{
    /*Esta clase se crea para agregar la cadena de conexion en la clase startup*/
    /*Representan la conexion desde json*/
    public class MongoSettings
    {
        public string ConnectionString { get; set; }

        public string Database { get; set; }
    }
}
