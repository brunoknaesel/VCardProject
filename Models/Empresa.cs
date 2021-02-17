using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    public class Empresa
    {
        public Empresa(int idEmpresa, string nomeRazaoSocial, string nomeFantasia, string cNPJ, string dataFundacao, string email, string ramosAtividades, string fone1, string fone2, string nomeContato, string login, string senha)
        {
            IdEmpresa = idEmpresa;
            NomeRazaoSocial = nomeRazaoSocial;
            NomeFantasia = nomeFantasia;
            CNPJ = cNPJ;
            DataFundacao = dataFundacao;
            Email = email;
            RamosAtividades = ramosAtividades; // NAO ESTAMOS USANDO ESTA VARIAVEL... CRIEI UM BD APENAS PARA GERENCIAR ISTO
            Fone1 = fone1;
            Fone2 = fone2;
            NomeContato = nomeContato;
            Login = login;
            Senha = senha;
        }

        public Empresa()
        {
          
        }

        public int IdEmpresa { get; set; }
        public string NomeRazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNPJ { get; set; }
        public string DataFundacao { get; set; }
        public string Email { get; set; }
        public string RamosAtividades { get; set; }
        public string Fone1 { get; set; }
        public string Fone2 { get; set; }
        public string NomeContato { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
