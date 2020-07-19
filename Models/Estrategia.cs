using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDiarioNegociacoes.Models
{
    public class Estrategia
    {
        public Estrategia()
        {
            Negociacoes = new HashSet<Negociacao>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public  ICollection<Negociacao> Negociacoes { get; set; }

        public virtual int IdUser { get; set; }
        public virtual User User { get; set; }
    }
}
