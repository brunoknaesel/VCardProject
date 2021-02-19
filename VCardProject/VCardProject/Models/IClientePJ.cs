using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    interface IClientePJ
    {
        string Nome { get; set; }
        string CNPJ { get; set; }
        string DataFundacao { get; set; }
        string Email { get; set; }
        string FoneWhats { get; set; }
        string Fone2 { get; set; }
        string RamoAtividade1 { get; set; }
        string RamoAtividade2 { get; set; }
        string RamoAtividade3 { get; set; }
        int Status { get; set; }
    }
}
