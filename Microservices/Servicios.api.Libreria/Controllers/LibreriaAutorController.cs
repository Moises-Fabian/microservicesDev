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
    public class LibreriaAutorController : ControllerBase
    {
        /*inyeccion objeto IMongoR>epository y la paso el nombre que quiero trabajar(AutprEntity)*/
        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository;
        /*contructor de la clase*/
        public LibreriaAutorController(IMongoRepository<AutorEntity> autorGenericoRepository)
        {
            _autorGenericoRepository = autorGenericoRepository;
        }

        [HttpGet]
        /*devuelve todas las lista de autores base de datos*/
        public async Task<ActionResult<IEnumerable<AutorEntity>>> Get()
        {
            return Ok(await _autorGenericoRepository.GetAll());

        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AutorEntity>> GetById(string id)
        {
            var autor = await _autorGenericoRepository.GetbyId(id);
            return Ok(autor);
        }

        [HttpPost]

        public async Task Post(AutorEntity autor)
        {
            await _autorGenericoRepository.InsertDocument(autor);
        }

        [HttpPut("{Id}")]
        public async Task Put(string Id, AutorEntity autor)
        {
            autor.Id = Id;
            await _autorGenericoRepository.UpdateDocument(autor);
        }

        [HttpDelete("{Id}")]
        public async Task Delete(string id)
        {
            await _autorGenericoRepository.DeleteByID(id);
        }

        [HttpPost ("pagination")]

        public async Task<ActionResult<PaginationEntity<AutorEntity>>> PostPagination(PaginationEntity<AutorEntity> pagination)
        {
            var resultados = await _autorGenericoRepository.PaginationByFilter(
                                     pagination
                );
            return Ok(resultados);
        }
    }
}
