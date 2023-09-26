using System.Data;
using Agendamento_de_Consulta.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using Agendamento_de_Consulta.Models.Agendamento;
using NuGet.Protocol.Plugins;
using System.Runtime.ConstrainedExecution;

namespace Agendamento_de_Consulta.Models.Agendamento
{
    public class PessoasDAL : IPessoasDAL

    {
        string connectionString = @"Data Source=DESKTOP-HPPKB09\MSSQLSERVER01;Initial Catalog=DbConsulta;Integrated Security=True;TrustServerCertificate=True;";
        public IEnumerable<PESSOAS> GetAllPESSOAS()
        {
            List<PESSOAS> lstpessoa = new List<PESSOAS>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string consulta = "SELECT P.ID, P.NOME, P.CPF, E.IdEndereco, E.LOGRADOURO, E.NUMERO NUMEROCASA, E.CEP, E.BAIRRO, E.CIDADE, E.ESTADO,  T.NUMERO as   NUMERO, DDD, t.IdTel, TT.IdTT, TT.TIPO  FROM PESSOA P INNER JOIN ENDERECO E ON E.IdEndereco = P.ENDERECO LEFT JOIN PESSOA_TELEFONE PT ON PT.ID_PESSOA = P.ID LEFT JOIN TELEFONE T ON T.IdTel = PT.ID_TELEFONE LEFT JOIN TELEFONE_TIPO TT ON TT.IdTT = T.TIPO";
                SqlCommand cmd = new SqlCommand(consulta, con);
                cmd.CommandType = CommandType.Text;

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    PESSOAS pessoas = new PESSOAS();
                    Endereco end = new Endereco();
                    TELEFONE Tel = new TELEFONE();



                    pessoas.Id = Convert.ToInt32(rdr["ID"]);
                    pessoas.Nome = rdr["NOME"].ToString();
                    pessoas.CPF = Convert.ToInt64(rdr["CPF"]);
                    pessoas.End.IdEndereco = Convert.ToInt32(rdr["IdEndereco"]);
                    pessoas.End.LOGRADOURO = rdr["LOGRADOURO"].ToString();
                    pessoas.End.NUMEROCASA = Convert.ToInt32(rdr["NUMEROCASA"]);
                    pessoas.End.CEP = Convert.ToInt32(rdr["CEP"]);
                    pessoas.End.BAIRRO = rdr["BAIRRO"].ToString();
                    pessoas.End.CIDADE = rdr["CIDADE"].ToString();
                    pessoas.End.ESTADO = rdr["ESTADO"].ToString();
                    pessoas.Tel.IdTel = Convert.ToInt32(rdr["IdTel"]);
                    pessoas.Tel.DDD = Convert.ToInt16(rdr["DDD"]);
                    pessoas.Tel.NUMERO = Convert.ToInt32(rdr["NUMERO"]);
                    pessoas.Tel.Teltip.Tipo = (string)rdr["TIPO"].ToString();






                    lstpessoa.Add(pessoas);
                }

                con.Close();
            }
            return lstpessoa;

        }

        int IPessoasDAL.AddEndereco(PESSOAS pessoas)
        {
            using (SqlConnection con = new SqlConnection(connectionString))


            {
                string comandoSQL = "INSERT INTO ENDERECO (LOGRADOURO, NUMERO, CEP, BAIRRO, CIDADE, ESTADO) " +

                                    "VALUES (@Logradouro, @NumeroCasa, @CEP, @Bairro, @Cidade, @Estado); SELECT CAST(scope_identity() AS int)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);


                cmd.Parameters.AddWithValue("@Logradouro", pessoas.End.LOGRADOURO);
                cmd.Parameters.AddWithValue("@NumeroCasa", pessoas.End.NUMEROCASA);
                cmd.Parameters.AddWithValue("@CEP", pessoas.End.CEP);
                cmd.Parameters.AddWithValue("@Bairro", pessoas.End.BAIRRO);
                cmd.Parameters.AddWithValue("@Cidade", pessoas.End.CIDADE);
                cmd.Parameters.AddWithValue("@Estado", pessoas.End.ESTADO);

                con.Open();
                int idEndereco = (int)cmd.ExecuteScalar();
                pessoas.End.IdEndereco = idEndereco;
                con.Close();
                return pessoas.End.IdEndereco;

            }
        }



        int IPessoasDAL.AddPessoas(PESSOAS pessoas)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "INSERT INTO Pessoa (NOME, CPF, ENDERECO) VALUES (@Nome, @CPF, @IdEndereco); SELECT CAST(scope_identity() AS int)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@CPF", pessoas.CPF);
                cmd.Parameters.AddWithValue("@IdEndereco", pessoas.End.IdEndereco);


                con.Open();
                int idPessoa = (int)cmd.ExecuteScalar();
                pessoas.Id = idPessoa;
                con.Close();

                return idPessoa;
            }
        }




        int IPessoasDAL.AddTelefone(PESSOAS pessoa)
        {
            using (SqlConnection con = new SqlConnection(connectionString))

            {
                string comandoSQL = "INSERT INTO TELEFONE (DDD, NUMERO, TIPO ) VALUES (@DDD, @Numero, @TIPO ); SELECT CAST(scope_identity()  AS int )";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@DDD", pessoa.Tel.DDD);
                cmd.Parameters.AddWithValue("@Numero", pessoa.Tel.NUMERO);
                cmd.Parameters.AddWithValue("@TIPO", pessoa.Tel.Teltip.Tipo);


                con.Open();
                int idTel = (int)cmd.ExecuteScalar();
                pessoa.Tel.IdTel = idTel;
                con.Close();

                return idTel;

            }
        }

        public void AddPessoaTelefone(int idPessoa, int idTelefone)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "INSERT INTO PESSOA_TELEFONE (ID_PESSOA, ID_TELEFONE) VALUES (@ID, @idTel)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", idPessoa);
                cmd.Parameters.AddWithValue("@idTel", idTelefone);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public int UpdateEndereco(PESSOAS pessoas)
        {

            using (SqlConnection con = new SqlConnection(connectionString))


            {
                string comandoSQL = "UPDATE ENDERECO SET  LOGRADOURO = @Logradouro,  NUMERO = @NumeroCasa, CEP = @CEP, BAIRRO = @Bairro, CIDADE = @Cidade, ESTADO = @Estado WHERE  IdEndereco = @IdEndereco";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);


                cmd.Parameters.AddWithValue("@IdEndereco", pessoas.End.IdEndereco);
                cmd.Parameters.AddWithValue("@Logradouro", pessoas.End.LOGRADOURO);
                cmd.Parameters.AddWithValue("@NumeroCasa", pessoas.End.NUMEROCASA);
                cmd.Parameters.AddWithValue("@CEP", pessoas.End.CEP);
                cmd.Parameters.AddWithValue("@Bairro", pessoas.End.BAIRRO);
                cmd.Parameters.AddWithValue("@Cidade", pessoas.End.CIDADE);
                cmd.Parameters.AddWithValue("@Estado", pessoas.End.ESTADO);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return pessoas.End.IdEndereco;

            }
        }

        public int UpdatePessoaTelefone(int idPessoa, int idTelefone)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "SELECT * FROM  PESSOA_TELEFONE ";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", idPessoa);
                cmd.Parameters.AddWithValue("@idTel", idTelefone);



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return idPessoa;

            }
        }



        public int UpdateTelefone(PESSOAS pessoas)
        {
            string tipoTelefone = "";

            switch (pessoas.Tel.Teltip.Tipo)
            {
                case "Celular":
                    tipoTelefone = "1";
                    break;
                case "Trabalho":
                    tipoTelefone = "2";
                    break;
                case "Casa":
                    tipoTelefone = "3";
                    break;
                case "Recado":
                    tipoTelefone = "4";
                    break;
                default:
                    tipoTelefone = "Valor inválido";
                    break;
            }

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string comandoSQL = "UPDATE TELEFONE SET DDD = @DDD, NUMERO = @Numero, TIPO = @TIPO  WHERE IdTel = @IdTel ";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@IdTel", pessoas.Tel.IdTel);
                cmd.Parameters.AddWithValue("@DDD", pessoas.Tel.DDD);
                cmd.Parameters.AddWithValue("@Numero", pessoas.Tel.NUMERO);
                cmd.Parameters.AddWithValue("@TIPO", tipoTelefone);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return pessoas.Tel.IdTel;
            }
        }

        public int UpdatePessoas(PESSOAS pessoas)
        {


            using (SqlConnection con = new SqlConnection(connectionString))
            {




                string comandoSQL = "UPDATE PESSOA SET NOME = @Nome, CPF = @CPF, ENDERECO = @IdEndereco WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", pessoas.Id);
                cmd.Parameters.AddWithValue("@Nome", pessoas.Nome);
                cmd.Parameters.AddWithValue("@CPF", pessoas.CPF);
                cmd.Parameters.AddWithValue("@IdEndereco", pessoas.End.IdEndereco);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return pessoas.Id;

            }
        }

        public PESSOAS GetPessoas(int? id)
        {
            PESSOAS pessoas = new PESSOAS();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT P.ID , P.NOME, P.CPF, E.IdEndereco, E.LOGRADOURO, E.NUMERO NUMEROCASA, E.CEP, E.BAIRRO, E.CIDADE, E.ESTADO,  T.NUMERO as NUMERO, DDD, t.IdTel, TT.IdTT, TT.TIPO FROM PESSOA P INNER JOIN ENDERECO E ON E.IdEndereco = P.ENDERECO LEFT JOIN PESSOA_TELEFONE PT ON PT.ID_PESSOA = P.ID LEFT JOIN TELEFONE T ON T.IdTel = PT.ID_TELEFONE LEFT JOIN TELEFONE_TIPO TT ON TT.IdTT = T.TIPO WHERE ID =" + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);


                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {

                    pessoas.Id = Convert.ToInt32(rdr["ID"]);
                    pessoas.Nome = rdr["NOME"].ToString();
                    pessoas.CPF = Convert.ToInt64(rdr["CPF"]);
                    pessoas.End.IdEndereco = Convert.ToInt32(rdr["IdEndereco"]);
                    pessoas.End.LOGRADOURO = rdr["LOGRADOURO"].ToString();
                    pessoas.End.NUMEROCASA = Convert.ToInt32(rdr["NUMEROCASA"]);
                    pessoas.End.CEP = Convert.ToInt32(rdr["CEP"]);
                    pessoas.End.BAIRRO = rdr["BAIRRO"].ToString();
                    pessoas.End.CIDADE = rdr["CIDADE"].ToString();
                    pessoas.End.ESTADO = rdr["ESTADO"].ToString();
                    pessoas.Tel.IdTel = Convert.ToInt32(rdr["IdTel"]);
                    pessoas.Tel.DDD = Convert.ToInt16(rdr["DDD"]);
                    pessoas.Tel.NUMERO = Convert.ToInt32(rdr["NUMERO"]);
                    pessoas.Tel.Teltip.Tipo = (string)rdr["TIPO"].ToString();

                }
            }

            return pessoas;
        }

        public void DeletePessoaTelefone(int idPessoas, int idTelefone)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string comandoSQL = "DELETE FROM PESSOA_TELEFONE  WHERE ID_PESSOA = @ID and ID_TELEFONE = @IdTel";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", idPessoas);
                cmd.Parameters.AddWithValue("@IdTel", idTelefone);



                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();



            }
        }

        public void DeletePessoas(int? id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "DELETE FROM PESSOA WHERE ID = @ID";
                SqlCommand cmd = new SqlCommand(comandoSQL, connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", id);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }

        public void DeleteEndereco(int? id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string comandoSQL = "DELETE  FROM ENDERECO WHERE IdEndereco = @IdEndereco";
                SqlCommand cmd = new SqlCommand(comandoSQL, connection);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@IdEndereco", id);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

            }
        }



        public void DeleteTelefone(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {

                string comandoSQL = "DELETE from TELEFONE  WHERE IdTel = @IdTel ";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@IdTel", id);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }


    }
}

