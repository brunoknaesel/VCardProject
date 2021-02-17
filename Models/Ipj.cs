using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    interface Ipj
    {
        int idpj { get; set; }
        string CNPJ { get; set; }
        string DataFund { get; set; }

        int idEnd { get; set; }

        int idPessoa { get; set; }
    }

}
