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
                var alunoModel = new AlunoModel();

                return Ok(alunoModel.Listar());
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
                var alunoModel = new AlunoModel();
                IEnumerable<AlunoDTO> alunos = alunoModel.Listar().Where(x => x.Data == data || x.Nome.Contains(nome));

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
                var alunoModel = new AlunoModel();

                return Ok(alunoModel.Listar(id).FirstOrDefault());

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
                var alunoModel = new AlunoModel();

                alunoModel.Inserir(alunoDTO);

                return Ok(alunoModel.Listar());
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

                var alunoModel = new AlunoModel();
                alunoModel.Atualizar(alunoDTO);

                return Ok(alunoModel.Listar(id).FirstOrDefault());
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
                var alunoModel = new AlunoModel();

                alunoModel.Deletar(id);

                return Ok("Deletado com sucesso");
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
