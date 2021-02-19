using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    interface IEmpresa
    {
        int IdEmpresa { get; set; }
        string NomeRazaoSocial { get; set; }
        string NomeFantasia { get; set; }
        string CNPJ { get; set; }
        string DataFundacao { get; set; }
        string Email { get; set; }
        string RamosAtividades { get; set; }
        string Fone1 { get; set; }
        string Fone2 { get; set; }
        string NomeContato { get; set; }
        string Login { get; set; }
        string Senha { get; set; }

    }
}
