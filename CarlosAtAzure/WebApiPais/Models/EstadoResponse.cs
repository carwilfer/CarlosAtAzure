using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPais.Models
{
    public class EstadoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UrlFoto { get; set; }
        public string Pais { get; set; }
    }
}
