using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Core.Entities
{
    public class PaginationEntity<TDocument>
    {
        /*numero de paginas*/
        public int PageSize { get; set; }
        /**/
        public int Page { get; set; }
        /*Ordenamiento de la pagina*/
        public string Sort { get; set; }
        /*ordenamieto ascendente o descendiente*/
        public string SortDirection { get; set; }
        /*Filtro*/
        public string Filter { get; set; }
        /*filtro para que traiga datos en mayusculas o minusculas*/
        public FilterValueClass FilterValue {get; set;}

        public int PagesQuantity { get; set; }
        /*devuelve datos genericos*/
        public IEnumerable<TDocument> Data { get; set; }

        public int totalRows { get; set; }
    }
}
