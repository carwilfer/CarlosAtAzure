// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

using Domain;
using System;

namespace WebApiAmigo.ResourceControllers.AmigoResource
{
    public class AmigosDoAmigoResponse
    {
        public Guid Id { get; set; }
        public string ConviteId { get; set; }
        public Amigo Amigo { get; set; }
    }
}
