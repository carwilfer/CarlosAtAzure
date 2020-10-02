using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Pais
{
    public class CriarPaisViewModel
    {
        public IFormFile Foto { get; set; }
        public string UrlFoto { get; set; }
        public string Nome { get; set; }
        public List<string> Errors { get; set; }
    }
}
