// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Domain;
using System.Collections.Generic;

namespace WebApiAmigo.ResourceControllers.AmigoResource
{
    public class AmigosDoAmigoRequest
    {
        public string ConviteId { get; set; }
        public Amigo Amigo { get; set; }
        public List<string> Errors()
        {
            var listErro = new List<string>();

            if (string.IsNullOrEmpty(Amigo.Id.ToString()))
            {
                listErro.Add("Nome precisa ser preenchido.");
            }

            return listErro;
        }

    }
}
