using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public class AlunoModel
    {
        
        #region DB

        public List<AlunoDTO> Listar()
        {
            try
            {
                var alunoDao = new AlunoDAO();
                return alunoDao.ListarAlunos();
            }
            catch (Exception ex)
            {
                throw new Exception($" Erro ao listar Alunos: {ex.Message }");
            }
        }

        public List<AlunoDTO> Listar(int id)
        {
            try
            {
                var alunoDao = new AlunoDAO();
                return alunoDao.ListarAlunos(id);
            }
            catch (Exception ex)
            {
                throw new Exception($" Erro ao listar Alunos: {ex.Message }");
            }
        }

        public void Inserir(AlunoDTO alunoDto)
        {
            try
            {
                var alunoDao = new AlunoDAO();
                alunoDao.InserirAluno(alunoDto);
            }
            catch (Exception ex)
            {
                throw new Exception($" Erro ao inserir Alunos: {ex.Message }");
            }
        }

        public void Atualizar(AlunoDTO alunoDto)
        {
            try
            {
                var alunoDao = new AlunoDAO();
                alunoDao.AtualizarAluno(alunoDto);
            }
            catch (Exception ex)
            {
                throw new Exception($" Erro ao atualizar Alunos: {ex.Message }");
            }
        }        

        public void Deletar(int id)
        {
            try
            {
                var alunoDao = new AlunoDAO();
                alunoDao.DeletarAluno(id);
            }
            catch (Exception ex)
            {
                throw new Exception($" Erro ao deletar Alunos: {ex.Message }");
            }
        }

        #endregion




        
    }
}