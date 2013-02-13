using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonicaMatricula.Dominio;
using MonicaMatricula.Repositorio;

namespace MonicaMatricula.Aplicacao
{
    public class ProfessorAplicacao
    {
        private readonly Contexto contexto = new Contexto();

        private void Inserir(Professor professor)
        {
            var strQuery = " ";
            strQuery += " INSERT INTO PROFESSOR (Nome, Habilidades, Salario) ";
            strQuery += string.Format(" VALUES ('{0}','{1}', '{2}') ",
                professor.Nome, professor.Habilidades, professor.Salario.ToString(CultureInfo.InvariantCulture));
            contexto.ExecutaComando(strQuery);
        }

        private void Alterar(Professor professor)
        {
            var strQuery = " ";
            strQuery += " UPDATE PROFESSOR SET ";
            strQuery += string.Format(" Nome = '{0}', ", professor.Nome);
            strQuery += string.Format(" Habilidades = '{0}', ", professor.Habilidades);
            strQuery += string.Format(" Salario = '{0}' ", professor.Salario.ToString(CultureInfo.InvariantCulture));
            strQuery += string.Format(" WHERE ProfessorId = {0}", professor.ProfessorId);
            contexto.ExecutaComando(strQuery);
        }

        public void Salvar(Professor professor)
        {
            if (professor.ProfessorId > 0)
                Alterar(professor);
            else
                Inserir(professor);
        }

        public void Excluir(int id)
        {
            var strQuery = string.Format(" DELETE FROM PROFESSOR WHERE ProfessorId = {0} ", id);
            contexto.ExecutaComando(strQuery);
        }

        public List<Professor> ListarTodos()
        {
            var strQuery = " SELECT * FROM PROFESSOR ";
            var retorno = contexto.ExecutaComandoComRetorno(strQuery);
            return TransformaReaderEmListaDeObjeto(retorno);
        }

        public Professor ListarPorId(int id)
        {
            var strQuery = " SELECT * FROM PROFESSOR WHERE ProfessorId = " + id;
            var retorno = contexto.ExecutaComandoComRetorno(strQuery);
            return TransformaReaderEmListaDeObjeto(retorno).FirstOrDefault();
        }

        private List<Professor> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var professor = new List<Professor>();
            while (reader.Read())
            {
                var tempObjeto = new Professor()
                {
                    ProfessorId = int.Parse(reader["ProfessorId"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Habilidades = reader["Habilidades"].ToString(),
                    Salario = Decimal.Parse(reader["Salario"].ToString())

                };
                professor.Add(tempObjeto);
            }
            return professor;
        }
    }
}
