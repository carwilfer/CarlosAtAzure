using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiAmigo.ResourceControllers.AmigoResource
{
    public class AmigoResponse
    {

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        //public List<AmigosDoAmigoResponse> AmigosDoAmigo { get; internal set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
    }
}
