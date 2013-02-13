using System.ComponentModel.DataAnnotations;

namespace MonicaMatricula.Dominio
{
    public class Disciplina
    {
        public int DisciplinaId { get; set; }

        [Required(ErrorMessage = "Nome da Disciplina é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Conteudo Programatico é obrigatório.")]
        [Display(Name = "Conteúdo Programático")]
        public string ConteudoProgramatico { get; set; }
    }
}
