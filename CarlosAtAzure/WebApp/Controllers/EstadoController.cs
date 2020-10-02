using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using WebApp.ApiServices;
using WebApp.Models.Estado;

namespace WebApp.Controllers
{
    public class EstadoController : Controller
    {
        private readonly IPaisApi _paisApi;
        private readonly IEstadoApi _estadoApi;

        public EstadoController(IPaisApi paisApi, IEstadoApi estadoApi)
        {
            _paisApi = paisApi;
            _estadoApi = estadoApi;
        }

        // GET: EstadoController
        public async Task<ActionResult> Index()
        {
            var listaEstado = await _estadoApi.GetAsync();

            return View(listaEstado);
        }

        // GET: EstadoController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var estado = await _estadoApi.GetEstadoAsync(id);
            return View(estado);
        }

        // GET: EstadoController/Create
        public async Task<ActionResult> Create()
        {
            var listaPais = await _paisApi.GetAsync();
            ViewBag.Paises = listaPais;
            return View();
        }

        // POST: EstadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarEstadoViewModel criarEstadoViewModel)
        {
            try
            {
                var urlFoto = UploadFotoEstado(criarEstadoViewModel.Foto);
                criarEstadoViewModel.UrlFoto = urlFoto.Result;
                await _estadoApi.PostAsync(criarEstadoViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var estado = await _estadoApi.GetEstadoByIdAsync(id);

            var listaPais = await _paisApi.GetAsync();

            ViewBag.Paises = listaPais;

            return View(estado);
        }

        // POST: EstadoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditarEstadoViewModel editarEstadoViewModel)
        {
            try
            {
                var urlFoto = UploadFotoEstado(editarEstadoViewModel.Foto);
                editarEstadoViewModel.UrlFoto = urlFoto.Result;

                await _estadoApi.PutAsync(id, editarEstadoViewModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstadoController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var estado = await _estadoApi.GetEstadoByIdAsync(id);

            return View(estado);
        }

        // POST: EstadoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _estadoApi.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private async Task<string> UploadFotoEstado(IFormFile foto)
        {
            var reader = foto.OpenReadStream();
            var cloudStoragteAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var blobClaint = cloudStoragteAccount.CreateCloudBlobClient();
            var container = blobClaint.GetContainerReference("fotos-estado");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            await blob.UploadFromStreamAsync(reader);
            var destinoDaImagemNuvem = blob.Uri.ToString();
            return destinoDaImagemNuvem;

        }
    }
}
