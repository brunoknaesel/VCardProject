using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    public class Pj : Pessoa, Ipj
    {
        public int idpj { get; set; }
        public string CNPJ { get; set; }

        public int idEnd { get; set; }

        public int idPessoa { get; set; }
    }
}
