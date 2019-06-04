using System.ComponentModel.DataAnnotations;

namespace App.Domain
{
    public class AlunoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é preenchimemto obrigatório")]
        [StringLength(50, ErrorMessage = "Nome deve ter no mínimo 2 e no máxima 50 caracteres", MinimumLength = 2)]
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Telefone { get; set; }

        [RegularExpression(@"[0-9]{4}\-[0-9]{2}", ErrorMessage = "O data deve estar no formaro YYYY-MM")]
        public string Data { get; set; }

        [Required(ErrorMessage = "O RA é de preenchimento obrigatório")]
        [Range(1, 9999, ErrorMessage = "O intervalo para cadastro de RA vai 1 a 9999")]
        public int RA { get; set; }
    }
}