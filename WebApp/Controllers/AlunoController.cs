using App.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApp.Models;

namespace WebApp.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Aluno")]
    public class AlunoController : ApiController
    {

        [HttpGet]
        [Route("Listar")]
        public IHttpActionResult Listar()
        {
            try
            {
                var aluno = new AlunoModel();

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
                var aluno = new AlunoModel();
                IEnumerable<AlunoDTO> alunos = aluno.ListarAlunos().Where(x => x.Data == data || x.Nome.Contains(nome));

                if (!alunos.Any())
                    return NotFound();

                return Ok(alunos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("Listar/{id:int}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var aluno = new AlunoModel();

                return Ok(aluno.ListarAlunosDB(id).FirstOrDefault());

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        public IHttpActionResult Post(AlunoDTO alunoDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var aluno = new AlunoModel();

                aluno.InserirDB(alunoDTO);

                return Ok(aluno.ListarAlunosDB());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody]AlunoDTO alunoDTO)
        {
            try
            {
                alunoDTO.Id = id;

                var aluno = new AlunoModel();
                aluno.AtualizarDB(alunoDTO);

                return Ok(aluno.ListarAlunosDB(id).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var aluno = new AlunoModel();

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
