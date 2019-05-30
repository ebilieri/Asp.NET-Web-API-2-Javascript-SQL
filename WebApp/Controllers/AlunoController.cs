using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*","*","*")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        public IEnumerable<Aluno> Get()
        {
            Aluno aluno = new Aluno();

            return aluno.ListarAlunos();
        }

        // GET: api/Aluno/5
        public Aluno Get(int id)
        {
            Aluno aluno = new Aluno();

            return aluno.ListarAlunos().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Aluno
        public List<Aluno> Post(Aluno aluno)
        {            
            aluno.Inserir(aluno);            

            return aluno.ListarAlunos();
        }

        // PUT: api/Aluno/5
        public Aluno Put(int id, [FromBody]Aluno aluno)
        {
            return aluno.Atualizar(id, aluno);
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
            Aluno aluno = new Aluno();
            aluno.Deletar(id);
        }
    }
}
