using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    public class Endereco : IEndereco
    {
        public Endereco(int idEnd, string logradouro, string numero, string bairro, string cep, string cidade, string estado, string pais, string complemento)
        {

            IdEnd = idEnd;
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            Complemento = complemento;
        }

        public Endereco()
        {

        }

        public int IdEnd { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Complemento { get; set; }
    }
}
