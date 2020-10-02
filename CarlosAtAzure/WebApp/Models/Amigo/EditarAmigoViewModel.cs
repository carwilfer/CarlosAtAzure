using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Amigo
{
    public class EditarAmigoViewModel
    {
        public Guid Id { get; set; }
        public IFormFile Foto { get; set; }
        public string UrlFoto { get; set; }
        public string Name { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public Pais.ListarPaisViewModel Pais { get; set; }
        public ListarAmigoViewModel Amigo { get; set; }
        public Estado.ListarEstadoViewModel Estado { get; set; }

        public List<string> Errors { get; set; }
    }
}
