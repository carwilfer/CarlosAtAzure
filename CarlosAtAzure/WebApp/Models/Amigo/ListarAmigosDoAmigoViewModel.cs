using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models.Amigo
{
    public class ListarAmigosDoAmigoViewModel
    {
        public Guid Id { get; set; }
        public string AmigoSolicitacaoId { get; set; }
        public ListarAmigoViewModel Amigo { get; set; }
    }
}
