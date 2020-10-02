using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain
{
    public class Amigo
    {
        public Guid Id { get; set; }
        public string UrlFoto { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }

        public Pais.Pais Pais { get; set; } = new Pais.Pais();
        public Estado.Estado Estado { get; set; } = new Estado.Estado();

        [JsonIgnore]
        public virtual List<AmigosDoAmigo> AmigosDoAmigo { get; set; } = new List<AmigosDoAmigo>();
    }
}
