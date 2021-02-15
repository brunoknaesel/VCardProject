using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    public class Pf : Pessoa, Ipf
    {
        public Pf(int idpf, string cpf, string nascimento, int idEnd, int idPessoa)
        {
            Idpf = idpf;
            Cpf = cpf;
            Nascimento = nascimento;
            IdEnd = idEnd;
            IdPessoa = idPessoa;
        }

        public Pf()
        {
          
        }

        public int Idpf { get; set; }
        public string Cpf { get; set; }
        public string Nascimento { get; set; }
        public int IdEnd { get; set; }  
        public int IdPessoa { get; set; }
    }
}
