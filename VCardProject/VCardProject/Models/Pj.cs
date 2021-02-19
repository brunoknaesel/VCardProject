using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    public class Pj : Pessoa, Ipj
    {
        public Pj(int idpj, string cNPJ, int idEnd, int idPessoa, string dataFund)
        {
            this.idpj = idpj;
            CNPJ = cNPJ;
            this.idEnd = idEnd;
            this.idPessoa = idPessoa;
            DataFund = dataFund;
        }


        public Pj()
        {
          
        }


        public int idpj { get; set; }
        public string CNPJ { get; set; }

        public int idEnd { get; set; }

        public int idPessoa { get; set; }

        public string DataFund { get; set; }
    }
   
}
