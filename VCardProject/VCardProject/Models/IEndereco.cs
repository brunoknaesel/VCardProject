using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    interface IEndereco
    {
        int IdEnd { get; set; }
        string Logradouro { get; set; }
        string Numero { get; set; }
        string Bairro { get; set; }
        string Cep { get; set; }
        string Cidade { get; set; }
        string Estado { get; set; }
        string Pais { get; set; }
        string Complemento { get; set; }
    }
}
