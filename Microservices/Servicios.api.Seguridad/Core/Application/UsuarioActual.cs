using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Servicios.api.Seguridad.Core.DTO;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.Jwtlogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    public class UsuarioActual
    {
        public class UsuarioActualCommand : IRequest<UsuarioDTO> { }

        public class UsuarioActualHundler : IRequestHandler<UsuarioActualCommand, UsuarioDTO>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IUsuarioSesion _usuarioSesion;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IMapper _mapper;

            public UsuarioActualHundler(UserManager<Usuario> userManager, IUsuarioSesion usuarioSesion, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _usuarioSesion = usuarioSesion;
                _jwtGenerator = jwtGenerator;
            }
            public async Task<UsuarioDTO> Handle(UsuarioActualCommand request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByNameAsync(_usuarioSesion.GetUsuarioSesion());

                if (usuario != null)
                {
                    var usuarioDTO = _mapper.Map<Usuario, UsuarioDTO>(usuario);
                    usuarioDTO.Token = _jwtGenerator.createToken(usuario);
                    return usuarioDTO;
                }

                throw new Exception("No se encontro el usuario");
            }
        }
    }
}
