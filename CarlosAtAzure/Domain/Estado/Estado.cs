using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Estado
{
    public class Estado
    {
        public Guid Id { get; set; }
        public string UrlFoto { get; set; }
        public string Nome { get; set; }
        public Pais.Pais Pais { get; set; } = new Pais.Pais();
    }
}
