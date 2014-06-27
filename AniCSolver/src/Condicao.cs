using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniCSolver.Core
{
    public class Condicao
    {
        public Variavel Variavel { get; set; }
        public Valor Valor { get; set; }
        public Regra Regra { get; set; }

        internal bool Boolean { get; set; }
        public Status Status { get; set; }
        public override string ToString()
        {
            return "["+Status+"] " + String.Format("{0} = {1}", Variavel.Nome, (Valor == null ? (Boolean ? "Sim" : "Não") : Valor.Nome));

        }
        
    }

    public enum Status
    {
        Desconhecido, Aceita, Regeitada
    }
}
