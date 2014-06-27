using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniCSolver.Core
{
    public class Regra
    {
        internal int Codigo { get; set; }
        public string Nome { get; set; }
        public int Posicao { get; set; }
        public Condicao Assertiva { get; set; }
        public bool Regeitada { get; set; }
        public bool Ativa { get { return !Regeitada; } }

       public override string ToString()
       {
           return (Regeitada?"[Regeitada] ":"")+Posicao+" "+Nome+" ["+Assertiva+"]";
       }

       public void AddCondicao(Condicao condicao)
       {
           condicao.Regra = this;
       }
    }
}
