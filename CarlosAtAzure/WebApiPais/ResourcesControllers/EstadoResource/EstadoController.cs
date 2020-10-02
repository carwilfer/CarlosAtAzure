using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Estado;
using AutoMapper;
using WebApiPais.Models;
using Repository;

namespace WebApiPais.ResourcesControllers.EstadoResource
{
    [Route("api/estados")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly WebApiAmigoContext _context;
        private readonly IMapper _mapper;

        public EstadoController(WebApiAmigoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Estado
        [HttpGet]
        public ActionResult Get()
        {
            var listaEstado = BuscarEstados();

            return Ok(listaEstado);
        }

        private IEnumerable<EstadoResponse> BuscarEstados()
        {
            var listaEstados = _context.Estado.ToList();

            return _mapper.Map<IEnumerable<EstadoResponse>>(listaEstados);
        }

        // GET: api/Estado/5
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] Guid id)
        {
            var response = BuscarEstadoPor(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        private EstadoResponse BuscarEstadoPor(Guid id)
        {
            var estado = _context.Estado.Include(x => x.Pais).FirstOrDefault(x => x.Id == id);

            if (estado == null)
                return null;

            EstadoResponse estadoResponse = new EstadoResponse { Id = estado.Id, 
                                                                 Nome = estado.Nome, 
                                                                 UrlFoto = estado.UrlFoto, 
                                                                 Pais = estado.Pais.Nome 
            };

            return _mapper.Map<EstadoResponse>(estadoResponse);
        }

        // PUT: api/Estado/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] EstadoRequest estadoRequest)
        {
            var response = BuscarEstadoPor(id);

            if (response == null)
            {
                return NotFound();
            }

            AlterarEstado(id, estadoRequest);

            return NoContent();
        }

        private void AlterarEstado(Guid id, EstadoRequest estadoRequest)
        {
            var estado = _context.Estado.Find(id);

            var aux = estado.Pais;

            estado = _mapper.Map(estadoRequest, estado);

            estado.Pais = aux;

            _context.Estado.Update(estado);
            _context.SaveChanges();
        }

        // POST: api/Estado
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult Post([FromBody] EstadoRequest estadoRequest)
        {
            var error = estadoRequest.Errors();

            if (error.Any())
            {
                return UnprocessableEntity(error);
            }

            var response = CriarEstado(estadoRequest);

            return CreatedAtAction(nameof(Get), new { response.Id }, response);
        }

        private EstadoResponse CriarEstado(EstadoRequest estadoRequest)
        {
            estadoRequest.Pais = _context.Pais.FirstOrDefault(x => x.Id == estadoRequest.Pais.Id);
            var estado = _mapper.Map<Estado>(estadoRequest);
            estado.Id = Guid.NewGuid();

            _context.Estado.Add(estado);
            _context.SaveChanges();

            return _mapper.Map<EstadoResponse>(estado); ;
        }

        // DELETE: api/Estado/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            var response = BuscarEstadoPor(id);

            if (response == null)
            {
                return NotFound();
            }

            ExcluirEstado(id);

            return NoContent();
        }

        private void ExcluirEstado(Guid id)
        {
            var estado = _context.Estado.Find(id);

            if (estado == null)
                return;

            _context.Estado.Remove(estado);
            _context.SaveChanges();
        }

        private bool EstadoExists(Guid id)
        {
            return _context.Estado.Any(e => e.Id == id);
        }
    }
}
