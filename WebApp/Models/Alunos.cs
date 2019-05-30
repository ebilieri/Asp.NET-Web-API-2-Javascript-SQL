using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Alunos
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }
        public int RA { get; set; }

        public List<Alunos> ListaAlunos()
        {
            Alunos aluno = new Alunos();
            aluno.Id = 1;
            aluno.Nome = "Emerson";
            aluno.SobreNome = "Bilieri";
            aluno.Telefone = "123478";
            aluno.RA = 1345;

            Alunos aluno1 = new Alunos();
            aluno1.Id = 2;
            aluno1.Nome = "Emerson2";
            aluno1.SobreNome = "Bilieri";
            aluno1.Telefone = "123478";
            aluno1.RA = 13452;

            List<Alunos> listaAlunos = new List<Alunos>();
            listaAlunos.Add(aluno);
            listaAlunos.Add(aluno1);
           
            return listaAlunos;
        }
    }
}