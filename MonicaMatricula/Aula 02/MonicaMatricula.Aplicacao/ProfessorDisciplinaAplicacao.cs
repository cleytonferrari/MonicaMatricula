using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonicaMatricula.Dominio;
using MonicaMatricula.Repositorio;

namespace MonicaMatricula.Aplicacao
{
    public class ProfessorDisciplinaAplicacao
    {
        private Contexto contexto;


        public void Salvar(Professor professor)
        {
            ExcluirDisciplinasDoProfessor(professor.ProfessorId);
            InserirDisciplinasDoProfessor(professor.ProfessorId, professor.Disciplinas.Select(c => c.DisciplinaId).ToArray());
        }

        public void InserirDisciplinasDoProfessor(int professorId, int[] disciplinasIds)
        {
            var strQuery = " ";
            
            foreach (var disciplinaId in disciplinasIds)
            {
                strQuery += " INSERT INTO PROFESSOR_DISCIPLINA (ProfessorId, DisciplinaId) ";
                strQuery += string.Format(" VALUES ('{0}','{1}'); ",
                    professorId, disciplinaId);   
            }

            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);    
            }
        }

        public void ExcluirDisciplinasDoProfessor(int professorId)
        {
            var strQuery = string.Format(" DELETE FROM PROFESSOR_DISCIPLINA WHERE ProfessorId = {0} ", professorId);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Professor> ListarTodos()
        {
            var listaDeProfessores = new ProfessorAplicacao().ListarTodos();
            foreach (var professor in listaDeProfessores)
            {
                var professorDisciplinas = ListarDisciplinaPorProfessorId(professor.ProfessorId);
                foreach (var professorDisciplina in professorDisciplinas)
                {
                    var disciplina = new DisciplinaAplicacao().ListarPorId(professorDisciplina.DisciplinaId);
                    if (disciplina != null)
                        professor.Disciplinas.Add(disciplina);

                }
            }
            return listaDeProfessores;

        }

        public Professor ListarPorId(int id)
        {
            var professor = new ProfessorAplicacao().ListarPorId(id);

            var professorDisciplinas = ListarDisciplinaPorProfessorId(professor.ProfessorId);
            foreach (var professorDisciplina in professorDisciplinas)
            {
                var disciplina = new DisciplinaAplicacao().ListarPorId(professorDisciplina.DisciplinaId);
                if (disciplina != null)
                    professor.Disciplinas.Add(disciplina);

            }

            return professor;
        }

        public IEnumerable<ProfessorDisciplina> ListarDisciplinaPorProfessorId(int id)
        {
            var strQuery = " SELECT * FROM PROFESSOR_DISCIPLINA WHERE ProfessorId = " + id;
            using (contexto = new Contexto())
            {
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retorno);
            }
        }

        private IEnumerable<ProfessorDisciplina> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var professor = new List<ProfessorDisciplina>();
            while (reader.Read())
            {
                var tempObjeto = new ProfessorDisciplina()
                {
                    ProfessorId = int.Parse(reader["ProfessorId"].ToString()),
                    DisciplinaId = int.Parse(reader["DisciplinaId"].ToString())
                };
                professor.Add(tempObjeto);
            }
            reader.Close();
            return professor;
        }


    }
}
