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
    public class CursoAplicacao
    {
        private Contexto contexto;

        private void Inserir(Curso curso)
        {
            var strQuery = " ";
            strQuery += " INSERT INTO CURSO (Nome, Objetivo, CargaHoraria) ";
            strQuery += string.Format(" VALUES ('{0}','{1}', '{2}') ",
                curso.Nome, curso.Objetivo, curso.CargaHoraria);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Curso curso)
        {
            var strQuery = " ";
            strQuery += " UPDATE CURSO SET ";
            strQuery += string.Format(" Nome = '{0}', ", curso.Nome);
            strQuery += string.Format(" Objetivo = '{0}', ", curso.Objetivo);
            strQuery += string.Format(" CargaHoraria = '{0}' ", curso.CargaHoraria);
            strQuery += string.Format(" WHERE CursoId = {0}", curso.CursoId);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Curso curso)
        {
            if (curso.CursoId > 0)
                Alterar(curso);
            else
                Inserir(curso);
        }

        public void Excluir(int id)
        {
            var strQuery = string.Format(" DELETE FROM CURSO WHERE CursoId = {0} ", id);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Curso> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                const string strQuery = " SELECT * FROM CURSO ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retorno);
            }
        }

        public Curso ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM CURSO WHERE CursoId = " + id;
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retorno).FirstOrDefault();
            }
        }

        private List<Curso> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var curso = new List<Curso>();
            while (reader.Read())
            {
                var tempObjeto = new Curso()
                {
                    CursoId = int.Parse(reader["CursoId"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Objetivo = reader["Objetivo"].ToString(),
                    CargaHoraria = int.Parse(reader["CargaHoraria"].ToString())

                };
                curso.Add(tempObjeto);
            }
            reader.Close();
            return curso;
        }
    }
}
