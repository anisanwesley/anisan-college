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

       public string Descricao
       {
           get
           {
               return String.IsNullOrWhiteSpace(_motivo)
                   ? "Preciso descobrir o valor de " + Variavel.Nome
                   : _motivo;
           }
           set
           {
               _motivo = value;
           }
       }

       public string Motivo
       {
           get
           {
               return String.IsNullOrWhiteSpace(_descricao)
                   ?"Qual valor de "+Variavel.Nome+" ?"
                   : _descricao;
           }
           set
           {
               _descricao = value;
           }
       }

       private string _motivo;
       private string _descricao;

       public override string ToString()
       {
           return Descricao;
       }

    }
}
