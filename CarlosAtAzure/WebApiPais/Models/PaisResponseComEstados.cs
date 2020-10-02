using System;
using System.Collections.Generic;

namespace WebApiPais.Models
{
    public class PaisResponseComEstados
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UrlFoto { get; set; }
        public List<string> Estados { get; set; }
    }
}
