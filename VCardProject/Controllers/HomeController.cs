using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using ProjetoVCardMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //{ ////executar comandos para teste aqui
            //  //DBComands.TesteUpdate();
            //    DBComands.InsertClientePF("", "", "", "", "", "", "", "", "", "", "");
            //    DBComands.InsertClientePJ("", "", "", "", "", "", "", "", "", "", "");
            //    List<Pessoa> listapessoa = new List<Pessoa>();
            //    bool teste1 = DBComands.SelectClienteView("Eletricista", out listapessoa);
            //    ViewBag.SelectCliente = listapessoa;
            //    bool teste2 = DBComands.PreviwaVCard(out listapessoa);
            //    ViewBag.Preview = listapessoa;
            //    bool teste3 = DBComands.SelectClientesPendentes(out listapessoa);
            //    ViewBag.SelectClientesPendentes = listapessoa;
            //    bool teste4 = DBComands.UpdateClientesPendentes(49, 0);

            //}




            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //ADM TELAS ==================================================================
        public IActionResult Adm()
        {
            return View();
        }

        //VISITANTE TELAS ==================================================================
        public IActionResult Visit()
        {
            return View();
        }
        //TELAS RELACIONADAS A OPÇÃO DE CADASTRO==================================================
        public IActionResult OpCad()
        {
            return View();
        }
        public IActionResult CadPj()
        {
            return View();
        }
        public IActionResult CadPf()
        {
            return View();
        }
        public IActionResult CadProf()
        {
            return View();
        }
        //TELAS RELACIONADAS A OPÇÃO DE SELECIONAR SERVIÇO==================================================
        public IActionResult ChooseWork()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public static SqlConnection conn = new SqlConnection(@"Source=testeandre.database.windows.net;Initial Catalog=testeAndre;User ID=testeandre;Password=********;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //public static SqlConnection conn = new SqlConnection(@"Data Source=testeandre.database.windows.net;Initial Catalog=testeAndre;User ID=testeandre;Password=********;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //public static SqlConnection conn = new SqlConnection(@"Server=tcp:testeandre.database.windows.net,1433;Initial Catalog=testeAndre;Persist Security Info=False;User ID=testeandre;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        public static MySqlConnection conn = new MySqlConnection(@"Data Source=testeandre.database.windows.net;Initial Catalog=testeAndre;Persist Security Info=False;User ID=testeandre;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False");
        public static MySqlCommand cmd;
        public static MySqlDataReader dr;



        //public bool ConfereDadosEssenciaisPF(string nome, string cpf, string whats, string ramo)
        //{
        //    bool temp = true;
        //    if (nome == "" || cpf == "" || ramo == "" || whats == "")
        //    {
        //        temp = false;
        //        Console.WriteLine("Todos os campos devem ser preenchidos!!!"); // esta mensagem precisa ser alocada no front
        //    }
        //    else
        //    {
        //        //confere se já existe o cpf... aqui tenos que ajustar conexao com o banco para terminar a função
        //        //deixamos uma função pré definida.... falta finalizar com os parametros do banco
        //        if (cpf.Length != 11)
        //        {
        //            Console.WriteLine("Digite somente números... O CPF possui 11 digitos!");

        //            //continuar daqui... conferir se já existe o cpf na base
        //        }
        //    }
        //    return temp;
        //}


        //public bool ConfereDadosEssenciaisPJ(string nome, string cnpj, string telefone, string ramo)
        //{
        //    bool temp = true;
        //    if (nome == "" || cnpj == "" || ramo == "" || telefone == "")
        //    {
        //        temp = false;
        //        Console.WriteLine("Todos os campos devem ser preenchidos!!!"); // esta mensagem precisa ser alocada no front
        //    }
        //    else
        //    {
        //        //confere se já existe o cnpj... aqui tenos que ajustar conexao com o banco para terminar a função
        //        //deixamos uma função pré definida.... falta finalizar com os parametros do banco
        //        if (cnpj.Length != 14)
        //        {
        //            Console.WriteLine("Digite somente números... O CNPJ possui 14 digitos!");

        //            //continuar daqui... conferir se já existe o cnpj na base
        //        }
        //    }
        //    return temp;
        //}



        //public static bool TesteInsertBD()
        //{
        //    bool temp = true;
        //    string nome = "Andreeee";
        //    string cpf = "047489";
        //    //string insert = "INSERT INTO  `ClientePF`(`Nome`, `CPF`) VALUES ('AndreCamp' , '12345')";

        //    string insert = $"INSERT into ClientePF (Nome, CPF) values ('{nome}','{cpf}')";

        //    cmd = new MySqlCommand(insert, conn);
        //    conn.Open();
        //    cmd.ExecuteNonQuery();
        //    conn.Close();

        //    return temp;
        //}



        //public static bool TesteUpdate()
        //{
        //    bool temp = true;

        //    //string insert = "INSERT INTO  `ClientePF`(`Nome`, `CPF`) VALUES ('AndreCamp' , '12345')";
        //    //UPDATE `Cliente` SET `IdCliente`=[value - 1],`Nome`=[value - 2],`Idade`=[value - 3],`CPF`=[value - 4] WHERE 1

        //    string nome = "Teste01";
        //    string nome2 = "testandooo";

        //    string update = $"UPDATE dbo.ClientePF SET Nome = 'andreee' WHERE Nome = 'Teste01'";
        //    cmd = new MySqlCommand(update, conn);
        //    conn.Open();
        //    cmd.ExecuteNonQuery();
        //    conn.Close();


        //    return temp;
        //}





    }
}
