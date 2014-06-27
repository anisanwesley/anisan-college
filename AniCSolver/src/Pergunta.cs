using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniCSolver.Core
{
   public class Pergunta
    {
       internal Variavel Variavel { get; set; }
       internal string Descricao { get; set; }

       internal string Motivo
       {
           get
           {
               return String.IsNullOrWhiteSpace(_motivo)
                   ?"Preciso descobrir o valor de "+Variavel.Nome 
                   :_motivo;
           }
           set
           {
               _motivo = value;
           }
       }

       private string _motivo;

       public override string ToString()
       {
           return Descricao;
       }

    }
}
