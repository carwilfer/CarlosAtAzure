using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using WebApp.ApiServices;
using WebApp.Models.Amigo;

namespace WebApp.Controllers
{
    public class AmigoController : Controller
    {
        private readonly IAmigoApi _amigoApi;
        private readonly IPaisApi _paisApi;
        private readonly IEstadoApi _estadoApi;

        public AmigoController(IAmigoApi amigoApi, IPaisApi paisApi, IEstadoApi estadoApi)
        {
            _amigoApi = amigoApi;
            _paisApi = paisApi;
            _estadoApi = estadoApi;
        }

        // GET: AmigoController
        public async Task<ActionResult> Index()
        {
            var amigos = await _amigoApi.GetAsync();
            return View(amigos);
        }

        // GET: AmigoController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var amigo = await _amigoApi.GetDetailsAmigoAsync(id);

            return View(amigo);
        }

        // GET: AmigoController/Create
        public async Task<ActionResult> Create()
        {
            var listaAmigos = await _amigoApi.GetAsync();
            ViewBag.Amigos = listaAmigos;

            var listaPais = await _paisApi.GetAsync();
            ViewBag.Paises = listaPais;

            var listaEstados = await _estadoApi.GetAsync();
            ViewBag.Estados = listaEstados;

            return View();
        }

        // POST: AmigoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarAmigoViewModel criarAmigoViewModel)
        {
            var urlFoto = UploadFotoAmigo(criarAmigoViewModel.Foto);
            criarAmigoViewModel.UrlFoto = urlFoto.Result;

            await _amigoApi.PostAsync(criarAmigoViewModel);

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        

        // GET: AmigoController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var viewModel = await _amigoApi.GetAsync();
            return View(viewModel);
        }

        // POST: AmigoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditarAmigoViewModel editarAmigoViewModel)
        {
            var urlFoto = UploadFotoAmigo(editarAmigoViewModel.Foto);
            editarAmigoViewModel.UrlFoto = urlFoto.Result;
            await _amigoApi.PutAsync(id, editarAmigoViewModel);
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AmigoController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var amigo = await _amigoApi.GetAmigoByIdAsync(id);
            return View(amigo);
        }

        // POST: AmigoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _amigoApi.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost("amigos/DeletarAmigosDoAmigos/{amigosdoamigosId}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletarAmizade(Guid amigosdoamigosId, IFormCollection keyValues)
        {
            try
            {
                await _amigoApi.DeleteAmigosDoAmigoAsync(amigosdoamigosId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet("/amigos/{id}/amigosdoamigos")]
        public async Task<ActionResult> AmigosAdicionados(Guid id)
        {
            var viewModel = await _amigoApi.GetAsync();
            ViewBag.Amigos = viewModel;

            return View();
        }

        [HttpGet("amigos/DeleteAmigosDoAmigos/{amigosDoAmigosId}")]
        public async Task<ActionResult> DeletarAmigosDoAmigo(Guid amigosDoAmigosId)
        {
            var amizade = await _amigoApi.GetAmigosDoAmigoByIdAsync(amigosDoAmigosId);

            return View(amizade);
        }

        [HttpGet("amigos/{id}/DeleteAmigosDoAmigos")]
        public async Task<ActionResult> DeleteAmigosDoAmigo(Guid id)
        {
            var listaAmigos = await _amigoApi.GetAmigosDoAmigoAsync(id);
            ViewBag.Amigos = listaAmigos;

            return View();
        }

        [HttpPost("/amigos/{id}/amigosdoamigos")]
        public async Task<ActionResult> AmigosAdicionados(Guid id, CriarAmigosDoAmigoViewModels viewModel)
        {
            try
            {
                await _amigoApi.PostAmigosDoAmigoAsync(id, viewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private async Task<string> UploadFotoAmigo(IFormFile foto)
        {
            var reader = foto.OpenReadStream();
            var cloudStoragteAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var blobClaint = cloudStoragteAccount.CreateCloudBlobClient();
            var container = blobClaint.GetContainerReference("fotos-amigo");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            await blob.UploadFromStreamAsync(reader);
            var destinoDaImagemNuvem = blob.Uri.ToString();
            return destinoDaImagemNuvem;

        }
    }
}
