using System.ComponentModel.DataAnnotations;

namespace MonicaMatricula.Dominio
{
    public class Curso
    {
        public int CursoId { get; set; }
        [Required(ErrorMessage = "Nome do Curso é obrigatório.")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Objetivo do Curso é obrigatório.")]
        [Display(Name = "Objetivo")]
        public string Objetivo { get; set; }

        [Required(ErrorMessage = "Carga Horária é obrigatório.")]
        [Display(Name = "Carga Horária")]
        public int CargaHoraria { get; set; }
    }
}
