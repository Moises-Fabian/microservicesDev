using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servicios.api.Libreria.Core.Entities;
using Servicios.api.Libreria.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.api.Libreria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaServicioController : ControllerBase
    {
        /*Inyeccion*/
        private readonly IAutorRepository _autorRepository;
        /*Inyeccion*/
        private readonly IMongoRepository<AutorEntity> _autorGeneroRepository;
        /*Inyeccion*/
        public LibreriaServicioController(IAutorRepository autorRepository, IMongoRepository<AutorEntity> autorGenericoRepository)
        {
            _autorRepository = autorRepository;
            _autorGeneroRepository = autorGenericoRepository;
        }

        /*Método para que el cliente pueda consumirlo*/
        [HttpGet("autorGenerico")]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAutorGenerico()
        {
            var autores = await _autorGeneroRepository.GetAll();
            return Ok(autores);
        }

        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            var autores = await _autorRepository.GetAutores();
            return Ok(autores);
        }
    }
}
