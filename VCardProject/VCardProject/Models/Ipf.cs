using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    interface Ipf
    {
        int Idpf  { get; set; }
        string Cpf { get; set; }
        string Nascimento { get; set; }
        int IdEnd { get; set; }
        int IdPessoa { get; set; }
    }
}
