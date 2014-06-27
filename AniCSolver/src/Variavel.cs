using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniCSolver.Core
{
    public class Variavel
    {
        internal int Codigo { get; set; }
        public string Nome { get; set; }
        internal bool IsObjetivo { get; set; }
        public Valor Valor { get; set; }
        internal Dictionary<string, Valor> Valores = new Dictionary<string, Valor>();

        public override string ToString()
        {
            return Nome + (IsObjetivo ? " [Objetivo]" : "");
        }
    }
}
