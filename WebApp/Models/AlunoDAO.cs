using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebApp.Models
{
    public class AlunoDAO
    {
        //string StringConexao = ConfigurationManager.AppSettings["ConnectionString"] ;
        private string StringConexao = ConfigurationManager.ConnectionStrings["ConexaoDB"].ConnectionString;
        private IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(StringConexao);
            conexao.Open();
        }

        public List<Aluno> ListarAlunosDB()
        {
            try
            {
                var listaAlunos = new List<Aluno>();

                IDbCommand selctCmd = conexao.CreateCommand();
                selctCmd.CommandText = "select * from Alunos";
                IDataReader resultado = selctCmd.ExecuteReader();

                while (resultado.Read())
                {
                    var aluno = new Aluno
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Nome = Convert.ToString(resultado["Nome"]),
                        SobreNome = Convert.ToString(resultado["SobreNome"]),
                        Telefone = Convert.ToString(resultado["Telefone"]),
                        Data = Convert.ToString(resultado["Data"]),
                        RA = Convert.ToInt32(resultado["RA"])
                    };

                    listaAlunos.Add(aluno);
                }

                return listaAlunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }

        }

        public List<Aluno> ListarAlunosDB(int id)
        {
            try
            {
                var listaAlunos = new List<Aluno>();

                IDbCommand selctCmd = conexao.CreateCommand();
                selctCmd.CommandText = $"select * from Alunos where Id = {id}";
                IDataReader resultado = selctCmd.ExecuteReader();

                while (resultado.Read())
                {
                    var aluno = new Aluno
                    {
                        Id = Convert.ToInt32(resultado["Id"]),
                        Nome = Convert.ToString(resultado["Nome"]),
                        SobreNome = Convert.ToString(resultado["SobreNome"]),
                        Telefone = Convert.ToString(resultado["Telefone"]),
                        Data = Convert.ToString(resultado["Data"]),
                        RA = Convert.ToInt32(resultado["RA"])
                    };

                    listaAlunos.Add(aluno);
                }

                return listaAlunos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void InserirAlunoDB(Aluno aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "insert into Alunos (Nome, SobreNome, Telefone, Data, RA) values (@Nome, @SobreNome, @Telefone, @Data, @RA)";

                IDbDataParameter paramNome = new SqlParameter("Nome", aluno.Nome);
                IDbDataParameter paramSobreNome = new SqlParameter("SobreNome", aluno.SobreNome);
                IDbDataParameter paramTelefone = new SqlParameter("Telefone", aluno.Telefone);
                IDbDataParameter paramData = new SqlParameter("Data", aluno.Data);
                IDbDataParameter paramRA = new SqlParameter("RA", aluno.RA);

                insertCmd.Parameters.Add(paramNome);
                insertCmd.Parameters.Add(paramSobreNome);
                insertCmd.Parameters.Add(paramTelefone);
                insertCmd.Parameters.Add(paramData);
                insertCmd.Parameters.Add(paramRA);

                insertCmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void AtualizarAlunoDB(Aluno aluno)
        {
            try
            {
                IDbCommand updateCmd = conexao.CreateCommand();
                updateCmd.CommandText = "update Alunos set Nome = @Nome, SobreNome = @SobreNome, Telefone = @Telefone, Data = @Data, RA = @RA where ID = @ID";

                IDbDataParameter paramNome = new SqlParameter("Nome", aluno.Nome);
                IDbDataParameter paramSobreNome = new SqlParameter("SobreNome", aluno.SobreNome);
                IDbDataParameter paramTelefone = new SqlParameter("Telefone", aluno.Telefone);
                IDbDataParameter paramData = new SqlParameter("Data", aluno.Data);
                IDbDataParameter paramRA = new SqlParameter("RA", aluno.RA);
                IDbDataParameter paramId = new SqlParameter("ID", aluno.Id);

                updateCmd.Parameters.Add(paramNome);
                updateCmd.Parameters.Add(paramSobreNome);
                updateCmd.Parameters.Add(paramTelefone);
                updateCmd.Parameters.Add(paramData);
                updateCmd.Parameters.Add(paramRA);
                updateCmd.Parameters.Add(paramId);

                updateCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }

        public void DeletarAlunoDB(int id)
        {
            try
            {
                IDbCommand deleteCmd = conexao.CreateCommand();
                deleteCmd.CommandText = "delete from Alunos where ID = @ID";

                IDbDataParameter paramId = new SqlParameter("ID", id);
                deleteCmd.Parameters.Add(paramId);

                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conexao.Close();
            }
        }
    }
}