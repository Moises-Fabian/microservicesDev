using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Servicios.api.Seguridad.Core.DTO;
using Servicios.api.Seguridad.Core.Entities;
using Servicios.api.Seguridad.Core.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servicios.api.Seguridad.Core.Application
{
    public class Register
    {
        public class UsuarioRegisterCommand : IRequest<UsuarioDTO>
        {
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsuarioRegisterHundler : IRequestHandler<UsuarioRegisterCommand, UsuarioDTO>
        {
            private readonly SeguridadContexto _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IMapper _mapper;

            public UsuarioRegisterHundler(SeguridadContexto context, UserManager<Usuario> userManager, IMapper mapper)
            {
                _context = context;
                _userManager = userManager;
                _mapper = mapper;
            }
            public async Task<UsuarioDTO> Handle(UsuarioRegisterCommand request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(x => x.Email == request.Email).AnyAsync();
                if (existe)
                {
                    throw new Exception("El Email ya existe en la base de datos");
                }

                var existeUsuario = await _context.Users.Where(x => x.UserName == request.Username).AnyAsync();
                if (existeUsuario)
                {
                    throw new Exception("El username del usuario ya existe en la base de datos");
                }

                var usuario = new Usuario
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellido,
                    Email = request.Email,
                    UserName = request.Username
                };

                var resultado = await _userManager.CreateAsync(usuario, request.Password);
                if (resultado.Succeeded)
                {
                    var usuarioDTO = _mapper.Map<Usuario, UsuarioDTO>(usuario);
                    return usuarioDTO;
                }

                throw new Exception("No se pudo registrar el usuario");
            }
        }
    }
}
