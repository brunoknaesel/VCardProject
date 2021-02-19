using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    public class RamoAtividade
    {
        public RamoAtividade(string nome)
        {
            Nome = nome;
        }

        public RamoAtividade()
        {
        }

        public string Nome { get; set; }
    }
}
