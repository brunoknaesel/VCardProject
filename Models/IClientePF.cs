using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    interface IClientePF
    {
        string Nome { get; set; }
        string Cpf { get; set; }
        string Nascimento { get; set; }
        string Email { get; set; }
        string RamoAtividade1 { get; set; }
        string RamoAtividade2 { get; set; }
        string RamoAtividade3 { get; set; }
        string Fone { get; set; }
        string Fone2 { get; set; }
        string RedesSociais { get; set; }
        int TempoExperiencia { get; set; }
        int Status { get; set; }

    }
}
