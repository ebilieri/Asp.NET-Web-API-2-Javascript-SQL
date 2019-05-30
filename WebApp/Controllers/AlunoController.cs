using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        public IEnumerable<Alunos> Get()
        {
            Alunos aluno = new Alunos();

            return aluno.ListaAlunos();
        }

        // GET: api/Aluno/5
        public Alunos Get(int id)
        {
            Alunos aluno = new Alunos();

            return aluno.ListaAlunos().Where(x => x.Id == id).FirstOrDefault();
        }

        // POST: api/Aluno
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Aluno/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Aluno/5
        public void Delete(int id)
        {
        }
    }
}
