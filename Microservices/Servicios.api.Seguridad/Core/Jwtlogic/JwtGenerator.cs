using Microsoft.IdentityModel.Tokens;
using Servicios.api.Seguridad.Core.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Jwtlogic
{
    public class JwtGenerator : IJwtGenerator
    {
        public string createToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim("username", usuario.UserName),
                new Claim("nombre", usuario.Nombre),
                new Claim("apellido", usuario.Apellido)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ojGC9wZrBAeTHfDEFwNj69lf8Aq0JQIA"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials = credential
            };

            var tokenHundler = new JwtSecurityTokenHandler();
            var token = tokenHundler.CreateToken(tokenDescription);

            return tokenHundler.WriteToken(token);
        }
    }
}
