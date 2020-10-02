using Domain.Estado;
using Domain.Pais;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiAmigo.ResourceControllers.AmigoResource
{
    public class AmigoRequest
    {
        public string UrlFoto { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public Pais Pais { get; set; } = new Pais();
        public Estado Estado { get; set; } = new Estado();

        public List<string> Erros()
        {
            var list = new List<string>();
            if (string.IsNullOrWhiteSpace(Nome))
                list.Add("Nome é obrigatorio!");

            return list;
        }
    }
}
