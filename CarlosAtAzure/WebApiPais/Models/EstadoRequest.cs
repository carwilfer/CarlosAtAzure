using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPais.Models
{
    public class EstadoRequest
    {
        public string Nome { get; set; }
        public string UrlFoto { get; set; }
        public Domain.Pais.Pais Pais { get; set; } = new Domain.Pais.Pais();
        public List<string> Errors()
        {
            var listErro = new List<string>();

            if (string.IsNullOrEmpty(Nome))
            {
                listErro.Add("Nome precisa ser preenchido.");
            }

            return listErro;
        }
    }
}
