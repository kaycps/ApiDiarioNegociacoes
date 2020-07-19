using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDiarioNegociacoes.Models
{
    public class User
    {
        public User()
        {
            Estrategias = new HashSet<Estrategia>();
            Negociacaos = new HashSet<Negociacao>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Estrategia> Estrategias { get; set; }
        public virtual ICollection<Negociacao> Negociacaos { get; set; }

    }
}
