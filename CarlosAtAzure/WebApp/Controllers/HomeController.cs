using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.ApiServices;
using WebApp.Models;
using WebApp.Models.Home;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAmigoApi _amigoApi;
        private readonly IPaisApi _paisApi;
        private readonly IEstadoApi _estadoApi;

        public HomeController(ILogger<HomeController> logger, IAmigoApi amigoApi, IPaisApi paisApi, IEstadoApi estadoApi)
        {
            _logger = logger;
            _amigoApi = amigoApi;
            _paisApi = paisApi;
            _estadoApi = estadoApi;
        }

        public async Task<IActionResult> Index()
        {
            var paginaInicial = new PaginaInicialViewModel();

            var quatidadeDePaises = await _paisApi.GetAsync();
            var quantidadeDeEstados = await _estadoApi.GetAsync();
            var quantidadeDeAmigos = await _amigoApi.GetAsync();


            paginaInicial.QuatidadeDePaises = quatidadeDePaises.Count;
            paginaInicial.QuantidadeDeEstados = quantidadeDeEstados.Count;
            paginaInicial.QuantidadeDeAmigos = quantidadeDeAmigos.Count;

            return View(paginaInicial);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
