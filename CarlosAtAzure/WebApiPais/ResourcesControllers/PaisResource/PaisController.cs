using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Pais;
using AutoMapper;
using WebApiPais.Models;
using Repository;

namespace WebApiPais.ResourcesControllers.PaisResource
{
    [Route("api/pais")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly WebApiAmigoContext _context;
        private readonly IMapper _mapper;

        public PaisController(WebApiAmigoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Pais
        [HttpGet]
        public ActionResult Get()
        {
            var listPais = BuscarPaises();

            return Ok(listPais);
        }

        private IEnumerable<PaisResponse> BuscarPaises()
        {
            var listaPaises = _context.Pais.ToList();

            return _mapper.Map<IEnumerable<PaisResponse>>(listaPaises);
        }

        // GET: api/Pais/5
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] Guid id)
        {
            var response = BuscarPaisComEstadosPor(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        private PaisResponseComEstados BuscarPaisComEstadosPor(Guid id)
        {
            var estados = _context.Estado.Include(x => x.Pais).Where(x => x.Pais.Id == id).ToList();

            var pais = _context.Pais.Find(id);

            if (pais == null)
                return null;
            List<string> estadosNome = new List<string>();
            foreach (var item in estados)
            {
                estadosNome.Add(item.Nome);
            }
            PaisResponseComEstados paisResponseComEstados = new PaisResponseComEstados
            {
                Id = pais.Id,
                Nome = pais.Nome,
                UrlFoto = pais.UrlFoto,
                Estados = estadosNome
            };
            return _mapper.Map<PaisResponseComEstados>(paisResponseComEstados);
        }

        // PUT: api/Pais/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] PaisRequest paisRequest)
        {
            var response = BuscarPaisPor(id);

            if (response == null)
            {
                return NotFound();
            }

            AlterarPais(id, paisRequest);

            return NoContent();
        }

        private void AlterarPais(Guid id, PaisRequest paisRequest)
        {
            var pais = _context.Pais.Find(id);
            pais = _mapper.Map(paisRequest, pais);

            _context.Pais.Update(pais);
            _context.SaveChanges();
        }

        // POST: api/Pais
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult Post([FromBody] PaisRequest paisRequest)
        {
            var error = paisRequest.Errors();

            if (error.Any())
            {
                return UnprocessableEntity(error);
            }

            var response = CriarPais(paisRequest);

            return CreatedAtAction(nameof(Get), new { response.Id }, response);
        }

        private PaisResponse CriarPais(PaisRequest paisResquest)
        {
            var pais = _mapper.Map<Pais>(paisResquest);
            pais.Id = Guid.NewGuid();

            _context.Pais.Add(pais);
            _context.SaveChanges();

            return _mapper.Map<PaisResponse>(pais); ;
        }

        // DELETE: api/Pais/5
        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var response = BuscarPaisPor(id);

            if (response == null)
            {
                return NotFound();
            }

            ExcluirPais(id);

            return NoContent();
        }

        private void ExcluirPais(Guid id)
        {
            var pais = _context.Pais.Find(id);

            if (pais == null)
                return;

            _context.Pais.Remove(pais);
            _context.SaveChanges();
        }

        private PaisResponse BuscarPaisPor(Guid id)
        {
            var pais = _context.Pais.Find(id);

            if (pais == null)
                return null;

            return _mapper.Map<PaisResponse>(pais);
        }

        private bool PaisExists(Guid id)
        {
            return _context.Pais.Any(e => e.Id == id);
        }
    }
}
