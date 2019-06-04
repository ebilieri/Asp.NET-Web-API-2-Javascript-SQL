using App.Domain;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;

namespace WebApp.Models
{
    public class AlunoJSON
    {
        public List<AlunoDTO> ListarAlunos()
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");

            var json = File.ReadAllText(caminhoArquivo);

            var listaAlunos = JsonConvert.DeserializeObject<List<AlunoDTO>>(json);

            return listaAlunos;
        }

        public bool ReescreverArquivo(List<AlunoDTO> listaAlunos)
        {
            var caminhoArquivo = HostingEnvironment.MapPath(@"~/App_Data/Base.json");

            var json = JsonConvert.SerializeObject(listaAlunos, Formatting.Indented);

            File.WriteAllText(caminhoArquivo, json);

            return true;
        }

        public AlunoDTO Inserir(AlunoDTO aluno)
        {
            var listaAlunos = ListarAlunos();
            var maxId = listaAlunos.Max(x => x.Id);
            aluno.Id = maxId + 1;
            listaAlunos.Add(aluno);

            ReescreverArquivo(listaAlunos);

            return aluno;
        }

        public AlunoDTO Atualizar(int id, AlunoDTO aluno)
        {
            var listaAlunos = ListarAlunos();
            var itemIndex = listaAlunos.FindIndex(x => x.Id == aluno.Id);

            if (itemIndex >= 0)
            {
                aluno.Id = id;
                listaAlunos[itemIndex] = aluno;
            }
            else
            {
                return null;
            }

            ReescreverArquivo(listaAlunos);

            return aluno;
        }

        public bool Deletar(int id)
        {
            var listaAlunos = ListarAlunos();
            var itemIndex = listaAlunos.FindIndex(x => x.Id == id);

            if (itemIndex >= 0)
            {
                listaAlunos.RemoveAt(itemIndex);
            }
            else
            {
                return false;
            }

            ReescreverArquivo(listaAlunos);

            return true;
        }
    }
}