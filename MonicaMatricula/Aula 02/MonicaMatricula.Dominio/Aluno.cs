using System;
using System.ComponentModel.DataAnnotations;

namespace MonicaMatricula.Dominio
{
    public class Aluno
    {
        public int AlunoId { get; set; }

        [Required(ErrorMessage = "Nome do Aluno é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Nome da Mãe é obrigatório.")]
        [Display(Name = "Mãe")]
        public string Mae { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Data de Nascimento é obrigatória.")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }
    }
}
