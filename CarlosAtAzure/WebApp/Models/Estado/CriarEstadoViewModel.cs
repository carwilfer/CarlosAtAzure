using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Estado
{
    public class CriarEstadoViewModel
    {
        public IFormFile Foto { get; set; }
        public string UrlFoto { get; set; }
        public string Nome { get; set; }
        public Pais.ListarPaisViewModel Pais { get; set; }
        public List<string> Errors { get; set; }
    }
}
