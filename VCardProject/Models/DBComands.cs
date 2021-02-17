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
            bool temp = false;

            try
            {
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


            return temp;
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
                string select = $"Select (top 1) * FROM dbo.pessoa ORDER BY idtable1 DESC";
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
                                select = "Select Top(1) * FROM dbo.pf ORDER BY idpf DESC";
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

        /// <summary>
        /// Funcao a ser executada quando o cliente alterar as informacoes apos clicar em voltar na tela de registro
        /// do VCard 
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="email"></param>
        /// <param name="cpf"></param>
        /// <param name="dataNasc"></param>
        /// <param name="fone"></param>
        /// <param name="cep"></param>
        /// <param name="cidade"></param>
        /// <param name="rua"></param>
        /// <param name="bairro"></param>
        /// <param name="tempoExp"></param>
        /// <param name="ramo"></param>
        /// <returns></returns>
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
                select = "Select Top(1) * FROM dbo.pf ORDER BY idpf DESC";
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
        ///   /// Esta funcao retorna uma lista com os dados preenchidos do ultimo cliente quando ele clicar em voltar 
        /// para alterar algumma informacao... Apos executar essa funcao, retornar os valores, entao o cliente podera
        /// alterar e entao enivar uma novo funcao para alterar, chanada de UpdateOpcaoVoltarInsertClientePJ
        /// </summary>
        /// <param name="listaPj"></param>
        /// <returns></returns>
        public static bool OpcaoVoltarInsertClientePJ(out List<Pj> listaPj)
        {
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            //listapessoa = new List<Pessoa>();
            bool temp = false;
            listaPj = new List<Pj>();
            //Pessoa pessoa = new Pessoa();
            Pj pessoaJuridica = new Pj();
            string nome, email, cnpj, dataFund, fone, cep, cidade, rua, bairro, tempoExp, ramo;
            //string select = $"Select Top(1) a.*, b.*, c.* FROM dbo.pessoa a, dbo.endereco b, dbo.pf c ORDER BY idtable1 DESC";
            //string select = $"Select a.*, b.* FROM dbo.pessoa a, dbo.endereco b WHERE  a.RamoAtiv1 = '{ramoativ}' ORDER BY nome DESC";
            try
            {
                string select = $"Select (top 1) * FROM dbo.pessoa ORDER BY idtable1 DESC";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    pessoaJuridica.Nome = Convert.ToString(dr["nome"]);
                    pessoaJuridica.Email = Convert.ToString(dr["Email"]);
                    pessoaJuridica.Fone = Convert.ToString(dr["Fone"]);
                    pessoaJuridica.TempoExperiencia = Convert.ToInt32(dr["TempoExperiencia"]);
                    pessoaJuridica.RamoAtividade1 = Convert.ToString(dr["RamoAtiv1"]);
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

                            pessoaJuridica.Cep = Convert.ToString(dr["Cep"]);
                            pessoaJuridica.Cidade = Convert.ToString(dr["Cidade"]);
                            pessoaJuridica.Logradouro = Convert.ToString(dr["Rua"]);
                            pessoaJuridica.Bairro = Convert.ToString(dr["Bairro"]);
                        }
                        dr.Close();
                        conn.Close();
                        temp = true;
                        if (temp)
                        {
                            temp = false;
                            try
                            {
                                select = "Select Top(1) * FROM dbo.pj ORDER BY idpj DESC";
                                cmd = new SqlCommand(select, conn);
                                conn.Open();
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    // idPessoa = Convert.ToInt32(dr["idtable1"]);
                                    pessoaJuridica.DataFund = Convert.ToString(dr["DataFund"]);
                                    pessoaJuridica.CNPJ = Convert.ToString(dr["CNPJ"]);
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
                listaPj.Add(pessoaJuridica);
            }

            return temp;
        }


        /// <summary>
        /// /// Funcao a ser executada quando o cliente alterar as informacoes apos clicar em voltar na tela de registro
        /// do VCard 
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
        public static bool UpdateOpcaoVoltarInsertClientePJ(string nome, string email, string cnpj, string dataFund, string fone, string cep, string cidade, string rua, string bairro, string tempoExp, string ramo)
        {
            int status = 1;
            bool temp = false;
            int idJuridica = 0;
            int IdPessoa = 0;
            int idEndereco = 0;
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            string select = "";
            try
            {
                temp = false;
                select = "Select Top(1) * FROM dbo.pj ORDER BY idpj DESC";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    idJuridica = Convert.ToInt32(dr["idpj"]);
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

                    update = $"Update dbo.pj Set CNPJ = '{cnpj}', DataFund = '{dataFund}' WHERE  idpj = {idJuridica}";
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
            bool temp = false;

            try
            {
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

            return temp;
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
            bool temp = false;
            int idEnd = 0;
            //string ramoativ = "Eletricista";
            // List<Pessoa> listapessoa = new List<Pessoa>();
            listapessoa = new List<Pessoa>();

            try
            {
                //string select = $"Select a.*, b.* FROM dbo.pessoa a, dbo.endereco b WHERE  a.RamoAtiv1 = '{ramoativ}' ORDER BY nome DESC";
                string select = $"Select * FROM dbo.pessoa WHERE  a.RamoAtiv1 = '{ramoativ}' ORDER BY nome DESC";
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
                    pessoa.Id = Convert.ToInt32(dr["idtable1"]);
                    //pessoa.Bairro = Convert.ToString(dr["Bairro"]);

                    listapessoa.Add(pessoa);
                }
                dr.Close();
                conn.Close();

                temp = true;


                //int i = 0;
                //select = $"Select * FROM dbo.endereco WHERE RamoAtiv1 = '{ramoativ}' ORDER BY nome DESC";
                //cmd = new SqlCommand(select, conn);
                //conn.Open();
                //dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{ 
                //    Pessoa pes = listapessoa[i];
                //    pes.Bairro = Convert.ToString(dr["Bairro"]);
                //    listapessoa.Insert(i, pes);
                //    listapessoa.RemoveAt(i + 1);
                //    i++;
                //}

                /// usar as duas funcoes abaixo para teste no BD Teste
                /// 
                //string truncate = "TRUNCATE TABLE  dbo.TesteSelectAndre";
                //cmd = new SqlCommand(truncate, conn);
                //conn.Open();
                //cmd.ExecuteNonQuery();
                //conn.Close();

                //foreach (var item in listapessoa)
                //{
                //    string insert = $"Insert into dbo.TesteSelectAndre (Nome, Fone, TempExp, Bairro) values ('{item.Nome}','{item.Fone}',{item.TempoExperiencia}, '{item.Bairro}' )";
                //    //string insert = $"Insert into dbo.TesteSelectAndre (Nome, Fone, TempExp) values ('{item.Nome}','{item.Fone}',{item.TempoExperiencia} )";
                //    cmd = new SqlCommand(insert, conn);
                //    conn.Open();
                //    cmd.ExecuteNonQuery();
                //    conn.Close();
                //}
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
        /// Funcao que retorna lista com informacoes do PreviewCard.. Ou seja, o cliente vai ver estas informacoes 
        /// apos finalizar o cadastro...
        /// Se precisarmos adicionais mais inf a lista, eh simples, basta adicionar variaveis no dr reader
        /// </summary>
        /// <param name="listapessoa"></param>
        /// <returns></returns>
        public static bool PreviewVCard(out List<Pessoa> listapessoa)
        {
            bool temp = false;
            listapessoa = new List<Pessoa>();
            try
            {
                Pessoa pessoa = new Pessoa();

                string select = $"Select Top(1) * FROM dbo.endereco ORDER BY idend DESC";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    pessoa.Bairro = Convert.ToString(dr["Bairro"]);
                    pessoa.Logradouro = Convert.ToString(dr["Rua"]);
                    pessoa.Cidade = Convert.ToString(dr["Cidade"]);
                    pessoa.Cep = Convert.ToString(dr["Cep"]);
                    pessoa.Numero = Convert.ToString(dr["NumeroRua"]);
                    pessoa.Complemento = Convert.ToString(dr["ComplementoRua"]);
                }
                dr.Close();
                conn.Close();
                temp = true;

                if (temp)
                {
                    temp = false;
                    try
                    {
                        select = $"Select Top(1) * FROM dbo.pessoa ORDER BY idtable1 DESC";
                        cmd = new SqlCommand(select, conn);
                        conn.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            pessoa.Nome = Convert.ToString(dr["nome"]);
                            pessoa.Fone = Convert.ToString(dr["Fone"]);
                            pessoa.TempoExperiencia = Convert.ToInt32(dr["TempoExperiencia"]);
                            pessoa.RamoAtividade1 = Convert.ToString(dr["RamoAtiv1"]);
                            listapessoa.Add(pessoa);
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
            bool temp = false;
            try
            {
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



            return temp;
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
            bool temp = false;
            try
            {
                string update = $"UPDATE dbo.pessoa SET Status = {escolha}  WHERE  idtable1 = {idPessoa}";
                cmd = new SqlCommand(update, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
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
            return temp;
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
            bool temp = false;
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

        /// <summary>
        /// funcao apenas para usar no azure quando quiseremos zerar as tabelas
        /// </summary>
        public static void SintaxParaZerarTabelasAzure()
        {
            //truncate table dbo.pj
            //truncate table dbo.pf
            //Delete From dbo.endereco
            //Delete From dbo.Empresa
            //Delete From dbo.pessoa
            //DBCC CHECKIDENT('[dbo].[endereco]', RESEED, 0)
            //DBCC CHECKIDENT( '[dbo].[Empresa]', RESEED, 0 )
            //DBCC CHECKIDENT( '[dbo].[pessoa]', RESEED, 0 )


            //update para usar no azure caso precisarmos update inf direto no banco
            //string update = $"Update dbo.Empresa Set Senha = '{senha}' Where Id = {idEmpresa}";
        }


        public static bool ViewScreenAdm(string login, out List<Empresa> listaempresa)
        {
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            //listapessoa = new List<Pessoa>();
            bool temp = false;
            listaempresa = new List<Empresa>();
            //Pessoa pessoa = new Pessoa();
            Empresa empresa = new Empresa();
            string nomeRazao, nomeFantasia, cnpj, dataFund, fone, cep, cidade, rua, bairro, tempoExp, senha, nomeContato;
            //string select = $"Select Top(1) a.*, b.*, c.* FROM dbo.pessoa a, dbo.endereco b, dbo.pf c ORDER BY idtable1 DESC";
            //string select = $"Select a.*, b.* FROM dbo.pessoa a, dbo.endereco b WHERE  a.RamoAtiv1 = '{ramoativ}' ORDER BY nome DESC";
            try
            {
                string select = $"Select * FROM dbo.Empresa WHERE  Login = '{login}'";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    empresa.IdEmpresa = Convert.ToInt32(dr["Id"]);
                    empresa.NomeRazaoSocial = Convert.ToString(dr["NomeEmpresa"]);
                    empresa.NomeFantasia = Convert.ToString(dr["NomeFantasia"]);
                    empresa.CNPJ = Convert.ToString(dr["CNPJ"]);
                    empresa.DataFundacao = Convert.ToString(dr["DataFundacao"]);
                    empresa.Email = Convert.ToString(dr["email"]);
                    empresa.Fone1 = Convert.ToString(dr["Fone1"]);
                    empresa.NomeContato = Convert.ToString(dr["ContatoNome"]);
                    empresa.Login = Convert.ToString(dr["Login"]);
                    empresa.Senha = Convert.ToString(dr["Senha"]);
                    empresa.Cep = Convert.ToString(dr["cep"]);
                    empresa.Cidade = Convert.ToString(dr["cidade"]);
                    empresa.Logradouro = Convert.ToString(dr["rua"]);
                    empresa.NumeroRua = Convert.ToString(dr["numeroRua"]);
                    empresa.ComplementoRua = Convert.ToString(dr["complRua"]);
                    empresa.Bairro = Convert.ToString(dr["bairro"]);
                }
                dr.Close();
                conn.Close();
                temp = true;

                //(NomeEmpresa, NomeFantasia, CNPJ, DataFundacao, email, Fone1, ContatoNome, Login, Senha, cep, cidade, rua, numeroRua, complRua, bairro)

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
                listaempresa.Add(empresa);
            }

            return temp;
        }


        /// <summary>
        /// Funcao que atualiza os dados de cadastro do Adm
        /// </summary>
        /// <param name="idEmpresa"></param>
        /// <param name="nomeRazao"></param>
        /// <param name="nomeFantasia"></param>
        /// <param name="cnpj"></param>
        /// <param name="dataFund"></param>
        /// <param name="email"></param>
        /// <param name="fone"></param>
        /// <param name="nomeContato"></param>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <param name="cep"></param>
        /// <param name="cidade"></param>
        /// <param name="rua"></param>
        /// <param name="numeroRua"></param>
        /// <param name="bairro"></param>
        /// <returns></returns>

        public static bool UpdateViewScreenAdm(int idEmpresa, string nomeRazao, string nomeFantasia, string cnpj, string dataFund, string email, string fone, string nomeContato, string login, string senha, string cep, string cidade, string rua, string numeroRua, string bairro)
        {
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            bool temp = false;
            Empresa empresa = new Empresa();
            try
            {
                string update = $"Update dbo.Empresa Set NomeEmpresa = '{nomeRazao}', NomeFantasia = '{nomeFantasia}' , CNPJ = '{cnpj}', DataFundacao '{dataFund}', email '{email}', Fone1 = '{fone}', ContatoNome '{nomeContato}' , Login = '{login}', Senha = '{senha}' , cep = '{cep}', cidade = '{cidade}', rua = '{rua}', numeroRua = ''{numeroRua}, bairro '{bairro}' Where Id = {idEmpresa}";
                cmd = new SqlCommand(update, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
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

            return temp;
        }


        /// <summary>
        /// Funcao para adicionar Ramos de Atividades no BD... Quando estiver no menu Adm....
        /// </summary>
        /// <param name="ramoInput"></param>
        /// <returns></returns>

        public static bool InserirRamosAtividadeAdm(string ramoInput)
        {
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            bool temp = false;
            string ramoBd;
            bool checarRamo = true;
            try
            {
                string select = $"Select * FROM dbo.RamoAtividade";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ramoBd = Convert.ToString(dr["NomeRano"]);
                    if (ramoBd == ramoInput)
                    {
                        checarRamo = false;
                    }
                }
                dr.Close();
                conn.Close();

                temp = true;


                if (checarRamo)
                {
                    try
                    {
                        temp = false;
                        string insert = $"Insert into dbo.RamoAtividade (NomeRamo) values ('{ramoInput}')";
                        cmd = new SqlCommand(insert, conn);
                        conn.Open();
                        cmd.ExecuteNonQuery();
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

            return temp;
        }


        public static bool SelectRamosAtivCadastrados(out List<RamoAtividade> listaRamos)
        {
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            bool temp = false;
            listaRamos = new List<RamoAtividade>();
            RamoAtividade ramo = new RamoAtividade();
            try
            {
                string select = $"Select * FROM dbo.RamoAtividade";
                cmd = new SqlCommand(select, conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ramo.Nome = Convert.ToString(dr["NomeRamo"]);
                    listaRamos.Add(ramo);
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

            return temp;
        }

        /// <summary>
        /// Executar esta função apenas para inserir uma empresa no BD.. 
        /// </summary>
        /// <returns></returns>
        public static bool InserirPrimeiraEmpresaManualmente()
        {
            bool temp = false;

            try
            {
                string insert = $"Insert into dbo.Empresa (NomeEmpresa, NomeFantasia, CNPJ, DataFundacao, email, Fone1, ContatoNome, Login, Senha, cep, cidade, rua, numeroRua, complRua, bairro) values ('Cia Teste S/A', 'Cia Teste', '00.123.123/0001-01', '01/01/2001', 'adm@ciateste.com.br', '(47) 3330-0001', 'José de Oliveira', 'ciateste', 'teste123', '89046-000', 'Blumenau', 'Rua XV de Novembro', '1520', 'Edifício Brasília', 'Centro')";
                cmd = new SqlCommand(insert, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
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



            return temp;
        }
        public static void InsertPFManualmente()
        {
            int status = 1;
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            //string teste = "pf444444";

            //string nome = "Manuel Juan Diego da Rosa";
            //string cpf = "390.147.339-41";
            //string email = "mmanueljuandiegodarosa@outlook.com";
            //string dataNasc = "23/08/1995";
            //string fone = "(47) 98692-2779";
            //string cep = "89110-976";
            //string cidade = "Gaspar";
            //string rua = "Anfilóquio Nunes Pires";
            //string bairro = "Bela Vista";
            //string tempoExp = "2";
            //string ramo = "Pedreiro";

            //string nome = "Thiago Yago Noah Alves";
            //string cpf = "582.208.609-87";
            //string email = "thiagoyagonoahalves@gmail.com";
            //string dataNasc = "16/08/2001";
            //string fone = "(48) 98873-5922";
            //string cep = "88906-096";
            //string cidade = "Araranguá";
            //string rua = "Rua São Luiz";
            //string bairro = "Jardim das Avenidas";
            //string tempoExp = "1";
            //string ramo = "Pedreiro";

            //string nome = "Edson Geraldo Nicolas Moraes";
            //string cpf = "373.057.029-35";
            //string email = "edsongeraldonicolas@hotmail.com";
            //string dataNasc = "04/05/1994";
            //string fone = "(47) 98785-7220";
            //string cep = "88307-580";
            //string cidade = "Itajaí";
            //string rua = "Rua Vereador José Carlos Mendonça";
            //string bairro = "Carvalho";
            //string tempoExp = "3";
            //string ramo = "Eletricista";

            //string nome = "Leandro Roberto Benício Moraes";
            //string cpf = "887.927.919-05";
            //string email = "lleandrorobertobeniciomoraes@gmail.com";
            //string dataNasc = "11/01/1986";
            //string fone = "(47) 99622-0596";
            //string cep = "89015-480";
            //string cidade = "Blumenau";
            //string rua = "Rua Cássio Medeiros";
            //string bairro = "Vorstadt";
            //string tempoExp = "8";
            //string ramo = "Pintor";

            //string nome = "Bryan Gael Silva";
            //string cpf = "230.302.359-10";
            //string email = "bryangaelsilva-74@fixacomunicacao.com.br";
            //string dataNasc = "04/12/1981";
            //string fone = "(48) 99943-0777";
            //string cep = "88701-070";
            //string cidade = "Tubarão";
            //string rua = "Rua Travessa Antônio Magalhães de Castro";
            //string bairro = "Centro";
            //string tempoExp = "6";
            //string ramo = "Encanador";

            //string nome = "Antonio Gabriel Benjamin";
            //string cpf = "215.796.259-08";
            //string email = "antoniogabrielbenjamin@gmail.com.br";
            //string dataNasc = "12/07/1992";
            //string fone = "(47) 99217-3489";
            //string cep = "89164-900";
            //string cidade = "Rio do Sul";
            //string rua = "Rua Benjamin Constant";
            //string bairro = "Centro";
            //string tempoExp = "4";
            //string ramo = "Marceneiro";

            //string nome = "Débora Daiane Fernandes";
            //string cpf = "698.465.079-84";
            //string email = "deboradaianefernandes_@vick1.com.br";
            //string dataNasc = "09/04/1990";
            //string fone = "(47) 99598-4849";
            //string cep = "89285-165";
            //string cidade = "São Bento do Sul";
            //string rua = "Rua Maria Knuppel Weiss";
            //string bairro = "Mato Preto";
            //string tempoExp = "7";
            //string ramo = "Arquiteto";

            //string nome = "Fábio Tomás Thales Pinto";
            //string cpf = "552.774.079-36";
            //string email = "fabiotomasthalespinto-80@gdsambiental.com.br";
            //string dataNasc = "25/07/1984";
            //string fone = "(48) 99376-0721";
            //string cep = "88806-430";
            //string cidade = "Criciúma";
            //string rua = "Rua Roberto Cândido";
            //string bairro = "Mineira Nova";
            //string tempoExp = "7";
            //string ramo = "Carpinteiro";

            //string nome = "Maya Nair Fabiana da Mota";
            //string cpf = "347.284.009-99";
            //string email = "marmitasdamaya@gmail.com.br";
            //string dataNasc = "23/06/1970";
            //string fone = "(47) 98414-3199";
            //string cep = "88351-605";
            //string cidade = "Brusque";
            //string rua = "Rua SP - 071";
            //string bairro = "São Pedro";
            //string tempoExp = "14";
            //string ramo = "Restaurante";

            //string nome = "Renato Cauã Hugo Araújo";
            //string cpf = "114.093.469-43";
            //string email = "renatocauahugoaraujo-95@gmail.com";
            //string dataNasc = "03/10/1979";
            //string fone = "(47) 99580-3570";
            //string cep = "89160-091";
            //string cidade = "Rio do Sul";
            //string rua = "Rua Maria Camila Machado";
            //string bairro = "Centro";
            //string tempoExp = "8";
            //string ramo = "Barbearia";

            //string nome = "Thiago Thomas Oliver Melo";
            //string cpf = "906.198.809-88";
            //string email = "mecanicasulcar@hotmail.com";
            //string dataNasc = "11/08/1985";
            //string fone = "(48) 98179-6253";
            //string cep = "88817-107";
            //string cidade = "Criciúma";
            //string rua = "Rua Floresta";
            //string bairro = "Vila Floresta";
            //string tempoExp = "7";
            //string ramo = "Mecanica";

            //string nome = "Rayssa Marlene Nascimento";
            //string cpf = "146.373.129-90";
            //string email = "rayssatattosc@hotmail.com";
            //string dataNasc = "08/03/1982";
            //string fone = "(47) 99712-8319";
            //string cep = "89209-037";
            //string cidade = "Joinville";
            //string rua = "Rua Douglas Willian Martins";
            //string bairro = "João Costa";
            //string tempoExp = "10";
            //string ramo = "Tatuador";

            string nome = "Isis Milena Jesus";
            string cpf = "904.595.139-82";
            string email = "isisflowers@outlook.com";
            string dataNasc = "01/10/1975";
            string fone = "(47) 99465-1243";
            string cep = "88345-572";
            string cidade = "Camboriú";
            string rua = "Rua Jesuíno Anastácio Pereira";
            string bairro = "Santa Regina";
            string tempoExp = "19";
            string ramo = "Floricultura";

            bool temp = false;

            try
            {
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

        public static void InsertPJManualmente()
        {
            int status = 1;
            //status = 0 - Inativo             //status = 1 - Pendente             //status = 2 - Ativo
            //string teste = "pj555555";
            //string nome = "Oliveira Empreiteira Mão de Obra";
            //string cnpj = "00.650.830/0001-21";
            //string email = "oliveiraemp@gmail.com";
            //string dataFund = "01/02/1995";
            //string fone = "(47) 99225-1313";
            //string cep = "89046-000";
            //string cidade = "Blumenau";
            //string rua = "Rua XV de Novembro";
            //string bairro = "Centro";
            //string tempoExp = "8";
            //string ramo = "Pedreiro";

            //string nome = "RT Empreiteira Ltda";
            //string cnpj = "00.620.986/0001-35";
            //string email = "rt@gmail.com";
            //string dataFund = "05/08/1999";
            //string fone = "(48) 99112-1458";
            //string cep = "89040-001";
            //string cidade = "Blumenau";
            //string rua = "Rua dos Caçadores";
            //string bairro = "Velha";
            //string tempoExp = "12";
            //string ramo = "Pedreiro";

            //string nome = "Pintores Blumenau Ltda";
            //string cnpj = "00.520.741/0001-32";
            //string email = "pintores@gmail.com";
            //string dataFund = "05/01/2018";
            //string fone = "(47) 99114-5252";
            //string cep = "89045-003";
            //string cidade = "Blumenau";
            //string rua = "Rua Divinopolis";
            //string bairro = "Velha";
            //string tempoExp = "3";
            //string ramo = "Pintor";


            //string nome = "Seibt Servicos Hidraulicos";
            //string cnpj = "00.320.358/0001-22";
            //string email = "seibt@gmail.com";
            //string dataFund = "05/06/1990";
            //string fone = "(47) 99222-4578";
            //string cep = "89045-000";
            //string cidade = "Blumenau";
            //string rua = "Rua Jose Reuter";
            //string bairro = "Velha";
            //string tempoExp = "25";
            //string ramo = "Encanador";


            //string nome = "Arte e Design Ltda";
            //string cnpj = "00.421.523/0005-20";
            //string email = "artedesign@gmail.com";
            //string dataFund = "26/01/2012";
            //string fone = "(47) 92244-0445";
            //string cep = "89046-080";
            //string cidade = "Blumenau";
            //string rua = "Rua das Larajeiras";
            //string bairro = "Salto do Norte";
            //string tempoExp = "9";
            //string ramo = "Arquiteto";


            //string nome = "Mazzi Carpintaria em Geral Ltda";
            //string cnpj = "00.141.785/0002-20";
            //string email = "mazzi@gmail.com";
            //string dataFund = "08/04/2009";
            //string fone = "(47) 99254-2525";
            //string cep = "89045-003";
            //string cidade = "Blumenau";
            //string rua = "Rua da Comunidade";
            //string bairro = "Vila Itoupava";
            //string tempoExp = "12";
            //string ramo = "Carpinteiro";

            //string nome = "Restaurante e Lanchonete Garcia";
            //string cnpj = "00.525.624/0001-23";
            //string email = "restaurante@gmail.com";
            //string dataFund = "06/09/2015";
            //string fone = "(47) 99224-2545";
            //string cep = "89080-220";
            //string cidade = "Blumenau";
            //string rua = "Rua 4 de Janeiro";
            //string bairro = "Garcia";
            //string tempoExp = "6";
            //string ramo = "Restaurante";


            //string nome = "Rock Barbearia Ltda";
            //string cnpj = "00.520.102/0001-20";
            //string email = "restaurante@gmail.com";
            //string dataFund = "06/09/2015";
            //string fone = "(47) 99224-2545";
            //string cep = "89080-220";
            //string cidade = "Blumenau";
            //string rua = "Rua 4 de Janeiro";
            //string bairro = "Garcia";
            //string tempoExp = "6";
            //string ramo = "Barbearia";

            //string nome = "Recanto Cascaneia";
            //string cnpj = "00.204.552/0001-22";
            //string email = "cascaneia@gmail.com";
            //string dataFund = "12/12/1997";
            //string fone = "(47) 96454-1212";
            //string cep = "89045-000";
            //string cidade = "Gaspar";
            //string rua = "Rua Belchior";
            //string bairro = "Belchior";
            //string tempoExp = "20";
            //string ramo = "Parque Aquatico";

            //string nome = "Cascata Carolina";
            //string cnpj = "00.325.412/0001-22";
            //string email = "cascata@gmail.com";
            //string dataFund = "08/01/2002";
            //string fone = "(47) 99156-9191";
            //string cep = "89000-000";
            //string cidade = "Gaspar";
            //string rua = "Rua Belchior";
            //string bairro = "Belchior";
            //string tempoExp = "15";
            //string ramo = "Parque Aquatico";

            //string nome = "Tinta na Pele Tatoo";
            //string cnpj = "00.222.101/0002-20";
            //string email = "tintanapele@gmail.com";
            //string dataFund = "05/02/1997";
            //string fone = "(47) 99119-1010";
            //string cep = "89032-002";
            //string cidade = "Blumenau";
            //string rua = "Rua Sao Paulo";
            //string bairro = "Itoupava Seca";
            //string tempoExp = "24";
            //string ramo = "Tatuador";

            //string nome = "Floricultura e Presentes Sonia";
            //string cnpj = "08.457.852/0003-52";
            //string email = "floricultura@gmail.com";
            //string dataFund = "06/01/2007";
            //string fone = "(47) 99121-0130";
            //string cep = "89020-525";
            //string cidade = "Blumenau";
            //string rua = "Rua XV de Novembro";
            //string bairro = "Centro";
            //string tempoExp = "12";
            //string ramo = "Floricultura";


            bool temp = false;

            try
            {
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
}
