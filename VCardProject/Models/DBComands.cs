using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoVCardMVC.Models
{
    public static class DBComands
    {

        /// <summary>
        /// Funcao Builder para conection string
        /// </summary>
        static SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
        {
            DataSource = "testeandre.database.windows.net",
            UserID = "testeandre",
            Password = "Database@1234",
            InitialCatalog = "testeAndre"
        };

        public static SqlConnection conn = new SqlConnection(builder.ConnectionString);
        //public static SqlConnection conn = new SqlConnection(@"Source=testeandre.database.windows.net;Initial Catalog=testeAndre;User ID=testeandre;Password=********;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //public static MySqlConnection conn = new MySqlConnection(@"Data Source=testeandre.database.windows.net;Initial Catalog=testeAndre;Persist Security Info=False;User ID=testeandre;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False");
        //public static MySqlConnection conn = new MySqlConnection(@"Server=tcp:testeandre.database.windows.net,1433;Initial Catalog=testeAndre;Persist Security Info=False;User ID=testeandre;Password=Database@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        //public static SqlConnection conn = new SqlConnection(@"Server=tcp:testeandre.database.windows.net,1433;Initial Catalog=testeAndre;Persist Security Info=False;User ID=testeandre;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        public static SqlCommand cmd;
        public static SqlDataReader dr;



        /// <summary>
        /// Esta funcao faz o Insert do Cliente PJ no BD apos o cadastro inicial... 
        /// O cadastro este parametrizado para entrar como Status 1, que significa pendente..
        /// As variaveis abaixo ja preenchidas servem apenas para servir de teste... Devemos comentar
        /// estas variaveis quando recebermos de fato o input do usuario
        /// </summary>
        /// <param name = "elementos" ></ param >
        /// < returns ></ returns >
        public static bool InsertClientePF(string nome, string email, string cpf, string dataNasc, string fone, string cep, string cidade, string rua, string bairro, string tempoExp, string ramo)
        {
            int status = 1;
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            string teste = "pf444444";
            nome = teste;
            cpf = teste;
            email = teste;
            dataNasc = teste;
            fone = teste;
            cep = teste;
            cidade = teste;
            rua = teste;
            bairro = teste;
            tempoExp = "9";
            ramo = "Pintor";


            string insert = $"Insert into dbo.endereco (Cep, Cidade, Rua, Bairro) values ('{cep}','{cidade}','{rua}','{bairro}')";
            cmd = new SqlCommand(insert, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            insert = $"Insert into dbo.pessoa (Nome, Email, Fone, TempoExperiencia, RamoAtiv1, Status) values ('{nome}','{email}','{fone}','{Convert.ToInt32(tempoExp)}', '{ramo}', {status} )";
            cmd = new SqlCommand(insert, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int idEnd = 0;
            int idPessoa = 0;

            string select = "Select Top(1) * FROM dbo.endereco ORDER BY idend DESC";
            cmd = new SqlCommand(select, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idEnd = Convert.ToInt32(dr["idend"]);
            }
            dr.Close();
            conn.Close();

            select = "Select Top(1) * FROM dbo.pessoa ORDER BY idtable1 DESC";
            cmd = new SqlCommand(select, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idPessoa = Convert.ToInt32(dr["idtable1"]);
            }
            dr.Close();
            conn.Close();

            insert = $"Insert into dbo.pf (CPF, Nascimento, end_idend, pessoa_idtable1) values ('{cpf}','{dataNasc}',{idEnd}, {idPessoa} )";
            cmd = new SqlCommand(insert, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return true;
        }

        /// <summary>
        /// Esta funcao retorna uma lista com os dados preenchidos do ultimo cliente quando ele clicar em voltar 
        /// para alterar algumma informacao... Apos executar essa funcao, retornar os valores, entao o cliente podera
        /// alterar e entao enivar uma novo funcao para alterar, chanada de UpdateOpcaoVoltarInsertClientePF
        /// </summary>
        /// <param name="listaPf"></param>
        /// <returns></returns>
        public static bool OpcaoVoltarInsertClientePF(out List<Pf> listaPf)
        {
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            //listapessoa = new List<Pessoa>();
            bool temp = false;
            listaPf = new List<Pf>();
            //Pessoa pessoa = new Pessoa();
            Pf pessoaFisica = new Pf();
            string nome, email, cpf, dataNasc, fone, cep, cidade, rua, bairro, tempoExp, ramo;
            //string select = $"Select Top(1) a.*, b.*, c.* FROM dbo.pessoa a, dbo.endereco b, dbo.pf c ORDER BY idtable1 DESC";
            //string select = $"Select a.*, b.* FROM dbo.pessoa a, dbo.endereco b WHERE  a.RamoAtiv1 = '{ramoativ}' ORDER BY nome DESC";
            try
            {
                string select = $"Select (top 1) * FROM dbo.pessoa ORDER BY nome DESC";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    pessoaFisica.Nome = Convert.ToString(dr["nome"]);
                    pessoaFisica.Email = Convert.ToString(dr["Email"]);
                    pessoaFisica.Fone = Convert.ToString(dr["Fone"]);
                    pessoaFisica.TempoExperiencia = Convert.ToInt32(dr["TempoExperiencia"]);
                    pessoaFisica.RamoAtividade1 = Convert.ToString(dr["RamoAtiv1"]);
                }
                dr.Close();
                conn.Close();
                temp = true;

                if (temp)
                {
                    temp = false;
                    try
                    {
                        select = "Select Top(1) * FROM dbo.endereco ORDER BY idend DESC";
                        cmd = new SqlCommand(select, conn);
                        conn.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            // idEnd = Convert.ToInt32(dr["idend"]);

                            pessoaFisica.Cep = Convert.ToString(dr["Cep"]);
                            pessoaFisica.Cidade = Convert.ToString(dr["Cidade"]);
                            pessoaFisica.Logradouro = Convert.ToString(dr["Rua"]);
                            pessoaFisica.Bairro = Convert.ToString(dr["Bairro"]);
                        }
                        dr.Close();
                        conn.Close();
                        temp = true;
                        if (temp)
                        {
                            temp = false;
                            try
                            {
                                select = "Select Top(1) * FROM dbo.pf ORDER BY idtable1 DESC";
                                cmd = new SqlCommand(select, conn);
                                conn.Open();
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    // idPessoa = Convert.ToInt32(dr["idtable1"]);
                                    pessoaFisica.Nascimento = Convert.ToString(dr["DataNasc"]);
                                    pessoaFisica.Cpf = Convert.ToString(dr["CPF"]);
                                }
                                dr.Close();
                                conn.Close();
                                temp = true;
                            }
                            catch (FormatException)
                            {
                                temp = false;
                            }
                            catch (SqlException)
                            {
                                Console.WriteLine("BD Error... Press enter to continue     ");
                                Console.ReadLine();
                                Console.Clear();
                                temp = false;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("*Error Unkown...  Press enter to continue     *");
                                Console.ReadLine();
                                Console.Clear();
                                temp = false;
                            }
                        }
                    }
                    catch (FormatException)
                    {
                        temp = false;
                    }
                    catch (SqlException)
                    {
                        Console.WriteLine("BD Error... Press enter to continue     ");
                        Console.ReadLine();
                        Console.Clear();
                        temp = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("*Error Unkown...  Press enter to continue     *");
                        Console.ReadLine();
                        Console.Clear();
                        temp = false;
                    }

                }
            }
            catch (FormatException)
            {
                temp = false;
            }
            catch (SqlException)
            {
                Console.WriteLine("BD Error... Press enter to continue     ");
                Console.ReadLine();
                Console.Clear();
                temp = false;
            }
            catch (Exception)
            {
                Console.WriteLine("*Error Unkown...  Press enter to continue     *");
                Console.ReadLine();
                Console.Clear();
                temp = false;
            }

            if (temp)
            {
                listaPf.Add(pessoaFisica);
            }

            return temp;
        }

        public static bool UpdateOpcaoVoltarInsertClientePF(string nome, string email, string cpf, string dataNasc, string fone, string cep, string cidade, string rua, string bairro, string tempoExp, string ramo)
        {
            int status = 1;
            bool temp = false;
            int idFisica = 0;
            int IdPessoa = 0;
            int idEndereco = 0;
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            string select = "";
            try
            {
                temp = false;
                select = "Select Top(1) * FROM dbo.pf ORDER BY idtable1 DESC";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idFisica = Convert.ToInt32(dr["idpf"]);
                    IdPessoa = Convert.ToInt32(dr["pessoa_idtable1"]);
                    idEndereco = Convert.ToInt32(dr["end_idend"]);
                }
                dr.Close();
                conn.Close();
                temp = true;
                
                if (temp)
                {
                    temp = false;
                    string update = $"Update dbo.endereco Set Cep = '{cep}', Cidade = '{cidade}', Rua = '{rua}', Bairro = '{bairro}'  WHERE  idend = {idEndereco}";
                    cmd = new SqlCommand(update, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    update = $"Update dbo.pf Set CPF = '{cpf}', Nascimento = '{dataNasc}' WHERE  idpf = {idFisica}";
                    cmd = new SqlCommand(update, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    update = $"Update dbo.pessoa Set nome = '{nome}', Email = '{email}', RamoAtiv1 = '{ramo}', Fone = '{fone}', TempoExperiencia = '{tempoExp}'  WHERE  idtable1 = {IdPessoa}";
                    cmd = new SqlCommand(update, conn);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    temp = true;
                }
            }
            catch (FormatException)
            {
                temp = false;
            }
            catch (SqlException)
            {
                Console.WriteLine("BD Error... Press enter to continue     ");
                Console.ReadLine();
                Console.Clear();
                temp = false;
            }
            catch (Exception)
            {
                Console.WriteLine("*Error Unkown...  Press enter to continue     *");
                Console.ReadLine();
                Console.Clear();
                temp = false;
            }


            return temp;
        }

        /// <summary>
        /// Esta funcao faz o Insert do Cliente PJ no BD apos o cadastro inicial... 
        /// O cadastro este parametrizado para entrar como Status 1, que significa pendente..
        /// As variaveis abaixo ja preenchidas servem apenas para servir de teste... Devemos comentar
        /// estas variaveis quando recebermos de fato o input do usuario
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="cnpj"></param>
        /// <param name="dataFund"></param>
        /// <param name="fone"></param>
        /// <param name="cep"></param>
        /// <param name="cidade"></param>
        /// <param name="rua"></param>
        /// <param name="bairro"></param>
        /// <param name="tempoExp"></param>
        /// <param name="ramo"></param>
        /// <returns></returns>
        public static bool InsertClientePJ(string nome, string email, string cnpj, string dataFund, string fone, string cep, string cidade, string rua, string bairro, string tempoExp, string ramo)
        {
            int status = 1;
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            string teste = "pj555555";
            nome = teste;
            cnpj = teste;
            email = teste;
            dataFund = teste;
            fone = teste;
            cep = teste;
            cidade = teste;
            rua = teste;
            bairro = teste;
            tempoExp = "8";
            ramo = "Pintor";

            string insert = $"Insert into dbo.endereco (Cep, Cidade, Rua, Bairro) values ('{cep}','{cidade}','{rua}','{bairro}')";
            cmd = new SqlCommand(insert, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            insert = $"Insert into dbo.pessoa (Nome, Email, Fone, TempoExperiencia, RamoAtiv1, Status) values ('{nome}','{email}','{fone}','{Convert.ToInt32(tempoExp)}', '{ramo}', {status} )";
            cmd = new SqlCommand(insert, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            int idEnd = 0;
            int idPessoa = 0;

            string select = "Select Top(1) * FROM dbo.endereco ORDER BY idend DESC";
            cmd = new SqlCommand(select, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idEnd = Convert.ToInt32(dr["idend"]);
            }
            dr.Close();
            conn.Close();

            select = "Select Top(1) * FROM dbo.pessoa ORDER BY idtable1 DESC";
            cmd = new SqlCommand(select, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                idPessoa = Convert.ToInt32(dr["idtable1"]);
            }
            dr.Close();
            conn.Close();

            insert = $"Insert into dbo.pj (CNPJ, DataFund, end_idend, pessoa_idtable1) values ('{cnpj}','{dataFund}',{idEnd}, {idPessoa} )";
            cmd = new SqlCommand(insert, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            return true;
        }



        /// <summary>
        /// Funcao retorna lista dos clientes selecionados por Ramo de Atividade..
        /// Esta funciona precisa de um string como parametro de entrada com a palavra do Ramo de Atividade
        /// selecionado.. EX: Pedreiro, Pintor, eletricista, Etc
        /// </summary>
        /// <param name="ramoativ"></param>
        /// <param name="listapessoa"></param>
        /// <returns></returns>
        public static bool SelectClienteView(string ramoativ, out List<Pessoa> listapessoa) //string ramoativ, out List<Pessoa> listapessoa
        {
            bool temp = true;
            int idEnd = 0;
            //string ramoativ = "Eletricista";
            // List<Pessoa> listapessoa = new List<Pessoa>();
            listapessoa = new List<Pessoa>();
            string select = $"Select a.*, b.* FROM dbo.pessoa a, dbo.endereco b WHERE  a.RamoAtiv1 = '{ramoativ}' ORDER BY nome DESC";
            cmd = new SqlCommand(select, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Nome = Convert.ToString(dr["nome"]);
                pessoa.Fone = Convert.ToString(dr["Fone"]);
                pessoa.TempoExperiencia = Convert.ToInt32(dr["TempoExperiencia"]);
                pessoa.RamoAtividade1 = Convert.ToString(dr["RamoAtiv1"]);
                pessoa.Bairro = Convert.ToString(dr["Bairro"]);
                int idPessoa = Convert.ToInt32(dr["idtable1"]);

                //string select2 = $"Select * FROM dbo.pf WHERE  pessoa_idtable1 = {idPessoa}";
                //cmd = new SqlCommand(select2, conn);
                //dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                //    idEnd = Convert.ToInt32(dr["idend"]);
                //}

                //string select3 = $"Select * FROM dbo.endereco WHERE  idend = {idEnd}";
                //cmd = new SqlCommand(select3, conn);
                //dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                //    pessoa.Bairro = Convert.ToString(dr["Bairro"]);
                //}
                //string idEnd = Convert.ToString(dr["Bairro"]);
                //pessoa.Bairro = Convert.ToString(dr["Bairro"]);

                listapessoa.Add(pessoa);
            }
            dr.Close();
            conn.Close();

            string truncate = "TRUNCATE TABLE  dbo.TesteSelectAndre";
            cmd = new SqlCommand(truncate, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();

            foreach (var item in listapessoa)
            {
                string insert = $"Insert into dbo.TesteSelectAndre (Nome, Fone, TempExp, Bairro) values ('{item.Nome}','{item.Fone}',{item.TempoExperiencia}, '{item.Bairro}' )";
                //string insert = $"Insert into dbo.TesteSelectAndre (Nome, Fone, TempExp) values ('{item.Nome}','{item.Fone}',{item.TempoExperiencia} )";
                cmd = new SqlCommand(insert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return temp;
        }

        /// <summary>
        /// Funcao que retorna lista com informacoes do PreviewCard.. Ou seja, o cliente vai ver estas informacoes 
        /// apos finalizar o cadastro...
        /// Se precisarmos adicionais mais inf a lista, eh simples, basta adicionar variaveis no dr reader
        /// </summary>
        /// <param name="listapessoa"></param>
        /// <returns></returns>
        public static bool PreviwaVCard(out List<Pessoa> listapessoa)
        {
            bool temp = true;
            listapessoa = new List<Pessoa>();
            //string select = $"Select * FROM dbo.pessoa WHERE  RamoAtiv1 = '{ramoativ}' ORDER BY nome DESC";
            string select = $"Select Top(1) * FROM dbo.pessoa ORDER BY idtable1 DESC";
            cmd = new SqlCommand(select, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Nome = Convert.ToString(dr["nome"]);
                pessoa.Fone = Convert.ToString(dr["Fone"]);
                pessoa.TempoExperiencia = Convert.ToInt32(dr["TempoExperiencia"]);
                //pessoa.Bairro = Convert.ToString(dr["Bairro"]);

                listapessoa.Add(pessoa);
            }
            dr.Close();
            conn.Close();

            //string truncate = "TRUNCATE TABLE  dbo.TesteSelectAndre";
            //cmd = new SqlCommand(truncate, conn);
            //conn.Open();
            //cmd.ExecuteNonQuery();
            //conn.Close();

            //foreach (var item in listapessoa)
            //{
            //    //string insert = $"Insert into dbo.TesteSelectAndre (Nome, Fone, TempExp, Bairro) values ('{item.Nome}','{item.Fone}',{item.TempoExperiencia}, '{item.Bairro}' )";
            //    string insert = $"Insert into dbo.TesteSelectAndre (Nome, Fone, TempExp) values ('{item.Nome}','{item.Fone}',{item.TempoExperiencia} )";
            //    cmd = new SqlCommand(insert, conn);
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    conn.Close();
            //}

            return temp;
        }

        /// <summary>
        /// Funcao retorna lista dos clientes pendentes de confirmacao do Adm
        /// Esta funcao nao executa nada... para alterar o Status devemos chamar a funcao UpdateClientesPendentes
        /// </summary>
        /// <param name="listapessoa"></param>
        /// <returns></returns>
        public static bool SelectClientesPendentes(out List<Pessoa> listapessoa)
        {
            listapessoa = new List<Pessoa>();

            string select = $"Select * FROM dbo.pessoa WHERE  Status = 1 ORDER BY nome ASC";
            cmd = new SqlCommand(select, conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Pessoa pessoa = new Pessoa();
                pessoa.Id = Convert.ToInt32(dr["idtable1"]);
                pessoa.Nome = Convert.ToString(dr["nome"]);
                pessoa.Fone = Convert.ToString(dr["Fone"]);
                pessoa.TempoExperiencia = Convert.ToInt32(dr["TempoExperiencia"]);
                pessoa.RamoAtividade1 = Convert.ToString(dr["RamoAtiv1"]);
                pessoa.Status = Convert.ToInt32(dr["Status"]);

                listapessoa.Add(pessoa);
            }
            dr.Close();
            conn.Close();

            return true;
        }

        /// <summary>
        /// Esta funcao recebe a id da pessoa digitada pelo usuario e um Int escolha, caso queira inativa 
        /// receber numero 0, caso queira ativar receber numero 2
        /// </summary>
        /// <param name="idPessoa"></param>
        /// <param name="escolha"></param>
        /// <returns></returns>
        public static bool UpdateClientesPendentes(int idPessoa, int escolha)
        {
            string update = $"UPDATE dbo.pessoa SET Status = {escolha}  WHERE  idtable1 = {idPessoa}";
            cmd = new SqlCommand(update, conn);
            conn.Open();
            dr = cmd.ExecuteReader();

            return true;
        }


        /// <summary>
        /// Funcao com o objetivo de validar login e senha do admin para entrar em sua conta
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senhaInput"></param>
        /// <returns></returns>
        public static bool AcessoAdmin(string login, string senhaInput)
        {
            string senhaEmpresa = "";
            bool temp = true;
            try
            {
                string select = $"Select Senha FROM dbo.Empresa WHERE  Login = '{login}'";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    senhaEmpresa = Convert.ToString(dr["Senha"]);
                }
                dr.Close();
                conn.Close();

                if (senhaInput == senhaEmpresa)
                {
                    temp = true;
                }
                else
                {
                    temp = false;
                }
            }
            catch (FormatException)
            {
                temp = false;
            }
            catch (SqlException)
            {
                Console.WriteLine("BD Error... Press enter to continue     ");
                Console.ReadLine();
                Console.Clear();
                temp = false;
            }
            catch (Exception)
            {
                Console.WriteLine("*Error Unkown...  Press enter to continue     *");
                Console.ReadLine();
                Console.Clear();
                temp = false;
            }

            return temp;
        }

    }
}
