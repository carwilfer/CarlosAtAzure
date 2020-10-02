using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiAmigo.ResourceControllers.AmigoResource
{
    [Route("api/amigos")]
    [ApiController]
    public class AmigosController : ControllerBase
    {
        private readonly WebApiAmigoContext _context;
        private readonly IMapper _mapper;

        public AmigosController(WebApiAmigoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/<AmigosController>
        [HttpGet]
        public ActionResult Get()
        {
            var list = BuscarAmigos();

            return Ok(list); //200
        }

        // GET api/<AmigosController>/5
        [HttpGet("{id}")]
        public ActionResult Get([FromRoute] Guid id)
        {
            var response = BuscarAmigoPorId(id);
            if (Response == null)
                return NotFound();

            return Ok(response); //200
        }

        [HttpGet("deletaramigosDoAmigos/{amigosDoAmigosid}")]
        public ActionResult GetAmigosDoAmigoById([FromRoute] Guid amizadeid)
        {
            var response = BuscarAmigosDoAmigoPorId(amizadeid);

            if (response == null)
                return NotFound();

            return Ok(response);
        }

        //[HttpGet("{id}/deletarAmizades")]
        //public ActionResult GetAmigosDoAmigo([FromRoute] Guid id)
        //{
        //    var list = BuscarAmigosDoAmigo(id);

        //    return Ok(list);
        //}

        [HttpGet("{id}/amigosDoAmigos")]
        public ActionResult GetAmigosDoAmigo([FromRoute] Guid id)
        {
            var amigo = _context.Amigos.Find(id);
            if (amigo == null)
                return NotFound(); //404 
            var amigosDoAmigo = _context.AmigosDosAmigos.Include(x => x.Amigo)
                                                        .Include(x => x.Amigo.Pais)
                                                        .Include(x => x.Amigo.Estado).Where(x => x.ConviteId == id.ToString()).ToList();
            var response = _mapper.Map<List<AmigosDoAmigoResponse>>(amigosDoAmigo);

            return Ok(response);
        }

        // POST api/<AmigosController>
        [HttpPost]
        public ActionResult Post([FromBody] AmigoRequest amigoRequest)
        {
            var erros = amigoRequest.Erros();
            if (erros.Any())
                return UnprocessableEntity(erros); //422
            var response = CriarAmigo(amigoRequest);
            return CreatedAtAction(nameof(Get), new { response.Id }, response); //201
        }

        [HttpPost("{id}/amigosDoAmigos")]
        public ActionResult PostAmigosDoAmigo([FromRoute] Guid id, [FromBody] AmigosDoAmigoRequest request)
        {
            var error = request.Errors();

            if (error.Any())
                return UnprocessableEntity(error);

            var response = CriarAmigosDoAmigo(id, request);

            return CreatedAtAction(nameof(PostAmigosDoAmigo), new { response.Id }, response); //201
        }

        // PUT api/<AmigosController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromRoute] Guid id, [FromBody] AmigoRequest request)
        {
            //procura pelo Id, se não achar NoFound
            var response = BuscarAmigoPorId(id);
            if (Response == null)
                return NotFound(); //404
            //se achar realiza o método abaixo
            AlterarAmigo(id, request);

            //Faz a alteração e não retorna nada para esta requisição
            return Ok(response); //204
        }

        // DELETE api/<AmigosController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            //procura pelo Id, se não achar NoFound
            var response = BuscarAmigoPorId(id);
            if (response == null)
                return NotFound(); //404
            //se achar realiza o método abaixo
            ExcluirAmigo(id);

            //Faz a alteração e não retorna nada para esta requisição
            return NoContent(); //204
        }

        [HttpDelete("deletaramigosDoAmigos/{amigosDoAmigosid}")]
        public ActionResult DeleteAmigoDosAmigos([FromRoute] Guid amizadeid)
        {
            var response = BuscarAmigosDoAmigoPorId(amizadeid);

            if (response == null)
                return NotFound();

            ExcluirAmigoDosAmigos(amizadeid);

            return NoContent();
        }
        private void ExcluirAmigoDosAmigos(Guid id)
        {
            var membro = _context.AmigosDosAmigos.Find(id);

            if (membro == null)
                return;

            _context.AmigosDosAmigos.Remove(membro);
            _context.SaveChanges();
        }
        private void ExcluirAmigo(Guid id)
        {
            var amigo = _context.Amigos.Find(id);
            if (amigo == null)
                return;
            _context.Amigos.Remove(amigo);
            _context.SaveChanges();
        }
        private IEnumerable<AmigosDoAmigoResponse> BuscarAmigosDoAmigo(Guid id)
        {
            var listAmizades = _context.AmigosDosAmigos.Include(x => x.Amigo)
                                                       .Include(x => x.Amigo.Pais)
                                                       .Include(x => x.Amigo.Estado)
                                                       .Where(x => x.ConviteId == id.ToString()).ToList();

            return _mapper.Map<IEnumerable<AmigosDoAmigoResponse>>(listAmizades);
        }
        private IEnumerable<AmigoResponse> BuscarAmigos()
        {
            var amigo = _context.Amigos.ToList();
            return _mapper.Map<IEnumerable<AmigoResponse>>(amigo);
        }


        private AmigosComAmigos BuscarAmigoPorId(Guid id)
        {
            var amizades = _context.AmigosDosAmigos.Include(x => x.Amigo)
                                                   .Where(x => x.ConviteId == id.ToString()).ToList();

            var amigo = _context.Amigos.Include(x => x.Pais)
                                       .Include(x => x.Estado)
                                       .FirstOrDefault(x => x.Id == id);

            var estado = _context.Estado.Include(x => x.Pais)
                                        .FirstOrDefault(x => x.Id == amigo.Estado.Id);

            List<string> nomeAmigo = new List<string>();

            foreach (var item in amizades)
            {
                nomeAmigo.Add(item.Amigo.Nome);
            }

            amigo.Estado = estado;

            if (amigo == null)
                return null;

            AmigosComAmigos amigoResponse = new AmigosComAmigos { Id = amigo.Id, 
                                                                Amigo = nomeAmigo, 
                                                                DataNascimento = amigo.DataNascimento, 
                                                                Estado = amigo.Estado.Nome, 
                                                                Email = amigo.Email, 
                                                                Nome = amigo.Nome, 
                                                                Pais = amigo.Pais.Nome, 
                                                                Sobrenome = amigo.Sobrenome, 
                                                                Telefone = amigo.Telefone, 
                                                                UrlFoto = amigo.UrlFoto 
            };


            return _mapper.Map<AmigosComAmigos>(amigoResponse);
        }

        private AmigosDoAmigoResponse BuscarAmigosDoAmigoPorId(Guid id)
        {
            var amizade = _context.AmigosDosAmigos.Include(x => x.Amigo)
                                                  .Include(x => x.Amigo.Pais)
                                                  .Include(x => x.Amigo.Estado).FirstOrDefault(x => x.Id == id);

            return _mapper.Map<AmigosDoAmigoResponse>(amizade);
        }

        //Partes destas ações quem faz é o framwork
        private AmigoResponse BuscarAmigoPor(Guid id)
        {
            var amigo = _context.Amigos.Find(id);
            if (amigo == null)
                return null;
            //Map serve para ajudar a mapear dados entre objetos
            return _mapper.Map<AmigoResponse>(amigo);
            
        }

        private AmigoResponse CriarAmigo(AmigoRequest amigoRequest)
        {
            amigoRequest.Pais = _context.Pais.FirstOrDefault(x => x.Id == amigoRequest.Pais.Id);
            amigoRequest.Estado = _context.Estado.FirstOrDefault(x => x.Id == amigoRequest.Estado.Id);

            var amigo = _mapper.Map<Amigo>(amigoRequest);
            amigo.Id = Guid.NewGuid();

            _context.Amigos.Add(amigo);
            _context.SaveChanges();
            return _mapper.Map<AmigoResponse>(amigo);
        }
        private void AlterarAmigo(Guid id, AmigoRequest request)
        {
            var amigo = _context.Amigos.Find(id);

            var estado = amigo.Estado;
            var pais = amigo.Pais;

            amigo = _mapper.Map(request, amigo);

            amigo.Estado = estado;
            amigo.Pais = pais;

            _context.Amigos.Update(amigo);
            _context.SaveChanges();
        }

        private AmigosDoAmigoResponse CriarAmigosDoAmigo([FromRoute] Guid id, [FromBody] AmigosDoAmigoRequest request)
        {
            request.ConviteId = _context.Amigos.Include(x => x.Pais)
                                               .Include(x => x.Estado)
                                               .FirstOrDefault(x => x.Id == id).Id.ToString();
            request.Amigo = _context.Amigos.Include(x => x.Pais)
                                           .Include(x => x.Estado)
                                           .FirstOrDefault(x => x.Id == request.Amigo.Id);

            var amigo = _mapper.Map<AmigosDoAmigo>(request);
            amigo.Id = new Guid();

            _context.AmigosDosAmigos.Add(amigo);
            _context.SaveChanges();

            return _mapper.Map<AmigosDoAmigoResponse>(amigo);

        }

    }
}
