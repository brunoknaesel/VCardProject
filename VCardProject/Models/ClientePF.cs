using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    //NAO ESTAMOS MAIS USANDO ESTA CLASSE
    //NAO ESTAMOS MAIS USANDO ESTA CLASSE
    //NAO ESTAMOS MAIS USANDO ESTA CLASSE
    //NAO ESTAMOS MAIS USANDO ESTA CLASSE
    //NAO ESTAMOS MAIS USANDO ESTA CLASSE
    //NAO ESTAMOS MAIS USANDO ESTA CLASSE

    public class ClientePF : Endereco, IClientePF    //NAO ESTAMOS MAIS USANDO ESTA CLASSE
    {
        public ClientePF(string nome, string cpf, string nascimento, string email, string ramoAtividade1, string ramoAtividade2, string ramoAtividade3, string fone, string fone2, int tempoExperiencia, string redesSociais, int status)
        {
            Nome = nome; //obrigatorio
            Cpf = cpf; //obrigatorio
            Nascimento = nascimento;
            Email = email;
            RamoAtividade1 = ramoAtividade1; //obrigatorio
            RamoAtividade2 = ramoAtividade2; //caso alterarmos para mais ramos para o mesmo cadastro
            RamoAtividade3 = ramoAtividade3; //caso alterarmos para mais ramos para o mesmo cadastro
            Fone = fone;
            Fone2 = fone2;
            TempoExperiencia = tempoExperiencia;
            RedesSociais = redesSociais;
            Status = status;
            
        }

        public ClientePF()
        {

        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Nascimento { get; set; }
        public string Email { get; set; }
        public string RamoAtividade1 { get; set; }
        public string RamoAtividade2 { get; set; }
        public string RamoAtividade3 { get; set; }
        public string Fone { get; set; }
        public string Fone2 { get; set; }
        public string RedesSociais { get; set; }
        public int TempoExperiencia { get; set; }
        public int Status { get; set; }
    }
}
