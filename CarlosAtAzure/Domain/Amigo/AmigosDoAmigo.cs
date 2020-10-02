using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AmigosDoAmigo
    {
        public Guid Id { get; set; }
        public string ConviteId { get; set; }
        public Amigo Amigo { get; set; } = new Amigo();
    }
}
