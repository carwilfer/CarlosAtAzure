using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Estado
{
    public class EditarEstadoViewModel
    {
        public Guid Id { get; set; }
        public IFormFile Foto { get; set; }
        public string Name { get; set; }
        public string UrlFoto { get; set; }
        public Pais.ListarPaisViewModel Pais { get; set; }
        public List<string> Errors { get; set; }
    }
}
