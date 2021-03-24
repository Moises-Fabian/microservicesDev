using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Jwtlogic
{
    public interface IJwtGenerator
    {
        string createToken(Usuario usuario);
    }
}
