using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Pais
{
    public class Pais
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string UrlFoto { get; set; }

        [JsonIgnore]
        public List<Estado.Estado> ListarEstado { get; set; } = new List<Estado.Estado>();
    }
}
