using App.Domain;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace App.Repository
{
    public class AlunoDAO
    {
        //string StringConexao = ConfigurationManager.AppSettings["ConnectionString"] ;
        private readonly string  StringConexao = ConfigurationManager.ConnectionStrings["ConexaoDB"].ConnectionString;
        private readonly IDbConnection conexao;

        public AlunoDAO()
        {
            conexao = new SqlConnection(StringConexao);
            conexao.Open();
        }

        public List<AlunoDTO> ListarAlunos()
        {
            try
            {
                var listaAlunos = new List<AlunoDTO>();

                IDbCommand selctCmd = conexao.CreateCommand();
                selctCmd.CommandText = "select * from Alunos";
                IDataReader resultado = selctCmd.ExecuteReader();

                while (resultado.Read())
                {
                    var aluno = new AlunoDTO
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

        public List<AlunoDTO> ListarAlunos(int id)
        {
            try
            {
                var listaAlunos = new List<AlunoDTO>();

                IDbCommand selctCmd = conexao.CreateCommand();
                selctCmd.CommandText = $"select * from Alunos where Id = {id}";
                IDataReader resultado = selctCmd.ExecuteReader();

                while (resultado.Read())
                {
                    var aluno = new AlunoDTO
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

        public void InserirAluno(AlunoDTO aluno)
        {
            try
            {
                IDbCommand insertCmd = conexao.CreateCommand();
                insertCmd.CommandText = "insert into Alunos (Nome, SobreNome, Telefone, Data, RA) values (@Nome, @SobreNome, @Telefone, @Data, @RA)";

                IDbDataParameter paramNome = new SqlParameter("Nome", aluno.Nome);
                IDbDataParameter paramSobreNome = new SqlParameter("SobreNome", aluno.SobreNome);
                IDbDataParameter paramTelefone = new SqlParameter("Telefone", aluno.Telefone);
                IDbDataParameter paramData = new SqlParameter("Data", aluno.Data ?? "");
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

        public void AtualizarAluno(AlunoDTO aluno)
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

        public void DeletarAluno(int id)
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