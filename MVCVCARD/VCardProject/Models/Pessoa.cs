using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    public class Pessoa : Endereco, IPessoa
    {

        //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
        public Pessoa(int id, string nome, string email, string ramoAtividade1, 
            string ramoAtividade2, string ramoAtividade3, string fone, string fone2, 
            string redesSociais, int tempoExperiencia, int status, int idEnd, string logradouro, 
            string numero, string bairro, string cep, string cidade, string estado, string pais, 
            string complemento) : 
            base (idEnd, logradouro, numero, bairro, cep, cidade, estado, pais, complemento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            RamoAtividade1 = ramoAtividade1;
            RamoAtividade2 = ramoAtividade2;
            RamoAtividade3 = ramoAtividade3;
            Fone = fone;
            Fone2 = fone2;
            RedesSociais = redesSociais;
            TempoExperiencia = tempoExperiencia;
            Status = status;
            Bairro = bairro;

        }

        public Pessoa()
        {
         
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string RamoAtividade1 { get; set; }
        public string RamoAtividade2 { get; set; }
        public string RamoAtividade3 { get; set; }
        public string Fone { get; set; }
        public string Fone2 { get; set; }
        public string RedesSociais { get; set; }
        public int TempoExperiencia { get; set; }
        public int Status { get; set; }
        //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
    }
}
