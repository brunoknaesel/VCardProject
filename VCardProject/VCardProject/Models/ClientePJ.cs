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
    //NAO ESTAMOS MAIS USANDO ESTA CLASSE
    public class ClientePJ : Endereco, IClientePJ // NAO ESTAMOS MAIS USANDO ESTA CLASSE
    {
        public ClientePJ(string nome, string cNPJ, string dataFundacao, string email, string foneWhats, string fone2, string ramoAtividade1, string ramoAtividade2, string ramoAtividade3,int status)
        {
            Nome = nome;
            CNPJ = cNPJ;
            DataFundacao = dataFundacao;
            Email = email;
            FoneWhats = foneWhats;
            Fone2 = fone2;
            RamoAtividade1 = ramoAtividade1;
            RamoAtividade2 = ramoAtividade2;
            RamoAtividade3 = ramoAtividade3;
            Status = status;
        }

        public ClientePJ()
        {

        }

        public string Nome { get; set; }
        public string CNPJ { get; set; }
        public string DataFundacao { get; set; }
        public string Email { get; set; }
        public string FoneWhats { get; set; }
        public string Fone2 { get; set; }
        public string RamoAtividade1 { get; set; }
        public string RamoAtividade2 { get; set; }
        public string RamoAtividade3 { get; set; }
        public int Status { get; set; }
    }
}
