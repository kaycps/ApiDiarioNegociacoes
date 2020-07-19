using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDiarioNegociacoes.Models
{
    public class Negociacao
    {

        public int Id { get; set; }

        public DateTime Data { get; set; }

        public string Ativo { get; set; } 
        
        public int IdEstrategia { get; set; }

        public  Estrategia Estrategia { get; set; }

        public string Operacao { get; set; }

        public double Lote { get; set; }

        public double PrecoEntrada { get; set; }

        public double PrecoSaida { get; set; }

        public string Encerramento { get; set; }

        public virtual int IdUser { get; set; }
        public virtual User User { get; set; }

    }
}
