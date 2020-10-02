using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using WebApp.ApiServices;
using WebApp.Models.Pais;

namespace WebApp.Controllers
{
    public class PaisController : Controller
    {
        private readonly IPaisApi _paisApi;
        public PaisController(IPaisApi paisApi)
        {
            _paisApi = paisApi;
        }

        // GET: PaisController
        public async Task<ActionResult> Index()
        {
            var list = await _paisApi.GetAsync();

            return View(list);
        }

        // GET: PaisController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var list = await _paisApi.GetAsync();

            return View(list);
        }

        // GET: PaisController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CriarPaisViewModel criarPaisViewModel)
        {
            try
            {
                var urlFoto = UploadFotoPais(criarPaisViewModel.Foto);
                criarPaisViewModel.UrlFoto = urlFoto.Result;

                await _paisApi.PostAsync(criarPaisViewModel);

                return Redirect("/");
            }
            catch
            {
                return View();
            }
        }


        // GET: PaisController/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var pais = await _paisApi.GetPaisByIdAsync(id);
            return View(pais);
        }

        // POST: PaisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, EditarPaisViewModel editarPaisViewModel)
        {
            try
            {
                var urlFoto = UploadFotoPais(editarPaisViewModel.Foto);
                editarPaisViewModel.UrlFoto = urlFoto.Result;
                await _paisApi.PutAsync(id, editarPaisViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PaisController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var pais = await _paisApi.GetPaisByIdAsync(id);

            return View(pais);
        }

        // POST: PaisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _paisApi.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private async Task<string> UploadFotoPais(IFormFile foto)
        {
            var reader = foto.OpenReadStream();
            var cloudStoragteAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");
            var blobClaint = cloudStoragteAccount.CreateCloudBlobClient();
            var container = blobClaint.GetContainerReference("fotos-pais");
            await container.CreateIfNotExistsAsync();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            await blob.UploadFromStreamAsync(reader);
            var destinoDaImagemNuvem = blob.Uri.ToString();
            return destinoDaImagemNuvem;

        }
    }
}
