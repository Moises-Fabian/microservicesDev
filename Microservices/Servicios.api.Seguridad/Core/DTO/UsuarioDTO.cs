using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.DTO
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public int UserName { get; set; }
        public int Nombre { get; set; }
        public int Apellido { get; set; }
    }
}
