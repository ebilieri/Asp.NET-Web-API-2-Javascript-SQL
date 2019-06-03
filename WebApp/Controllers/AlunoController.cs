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
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {
        // GET: api/Aluno
        //public IEnumerable<Aluno> Get()
        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                Aluno aluno = new Aluno();

                return Ok(aluno.ListarAlunosDB());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route(@"ListarPorDataNome/{data:regex([0-9]{4}\-[0-9]{2})}/{nome:minlength(5)}")]
        public IHttpActionResult ListarPorDataNome(string data, string nome)
        {
            try
            {
                Aluno aluno = new Aluno();
                IEnumerable<Aluno> alunos = aluno.ListarAlunos().Where(x => x.Data == data || x.Nome.Contains(nome));

                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Aluno/5
        [HttpGet]
        [Route("Listar/{id:int}")]
        public Aluno Get(int id)
        {
            Aluno aluno = new Aluno();

            return aluno.ListarAlunosDB(id).FirstOrDefault();
        }

        // POST: api/Aluno
        [HttpPost]
        public IHttpActionResult Post(Aluno aluno)
        {
            try
            {
                aluno.InserirDB(aluno);

                return Ok(aluno.ListarAlunosDB());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // PUT: api/Aluno/5
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]Aluno aluno)
        {
            try
            {
                aluno.Id = id;

                aluno.AtualizarDB(aluno);

                return Ok(aluno.ListarAlunosDB(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Aluno/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Aluno aluno = new Aluno();
                aluno.DeletarDB(id);
                return Ok("Deletado com sucesso");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
