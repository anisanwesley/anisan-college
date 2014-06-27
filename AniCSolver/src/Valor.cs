using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniCSolver.Core
{
    public class Valor
    {
        internal int Codigo { get; set; }
        public string Nome { get; set; }
        public Variavel Variavel { get; set; }
        internal int Posicao { get; set; }

        public override string ToString()
        {
            return Nome;
        }

        internal void AddVariavel(Variavel variavel)
        {
            Variavel = variavel;
            Variavel.Valores.Add(Nome,this);
        }
    }
}
