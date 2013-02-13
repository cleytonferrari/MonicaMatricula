using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using MonicaMatricula.Dominio;
using MonicaMatricula.Repositorio;

namespace MonicaMatricula.Aplicacao
{
    public class DisciplinaAplicacao
    {
        private readonly Contexto contexto = new Contexto();

        private void Inserir(Disciplina disciplina)
        {
            var strQuery = " ";
            strQuery += " INSERT INTO DISCIPLINA (Nome, ConteudoProgramatico) ";
            strQuery += string.Format(" VALUES ('{0}','{1}') ",
                disciplina.Nome, disciplina.ConteudoProgramatico);
            contexto.ExecutaComando(strQuery);
        }

        private void Alterar(Disciplina disciplina)
        {
            var strQuery = " ";
            strQuery += " UPDATE DISCIPLINA SET ";
            strQuery += string.Format(" Nome = '{0}', ", disciplina.Nome);
            strQuery += string.Format(" ConteudoProgramatico = '{0}' ", disciplina.ConteudoProgramatico);
            strQuery += string.Format(" WHERE DisciplinaId = {0}", disciplina.DisciplinaId);
            contexto.ExecutaComando(strQuery);
        }

        public void Salvar(Disciplina disciplina)
        {
            if (disciplina.DisciplinaId > 0)
                Alterar(disciplina);
            else
                Inserir(disciplina);
        }

        public void Excluir(int id)
        {
            var strQuery = string.Format(" DELETE FROM DISCIPLINA WHERE DisciplinaId = {0} ", id);
            contexto.ExecutaComando(strQuery);
        }

        public List<Disciplina> ListarTodos()
        {
            var strQuery = " SELECT * FROM DISCIPLINA ";
            var retorno = contexto.ExecutaComandoComRetorno(strQuery);
            return TransformaReaderEmListaDeObjeto(retorno);
        }

        public Disciplina ListarPorId(int id)
        {
            var strQuery = " SELECT * FROM DISCIPLINA WHERE DisciplinaId = " + id;
            var retorno = contexto.ExecutaComandoComRetorno(strQuery);
            return TransformaReaderEmListaDeObjeto(retorno).FirstOrDefault();
        }

        private List<Disciplina> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var disciplina = new List<Disciplina>();
            while (reader.Read())
            {
                var tempObjeto = new Disciplina()
                {
                    DisciplinaId = int.Parse(reader["DisciplinaId"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    ConteudoProgramatico = reader["ConteudoProgramatico"].ToString()

                };
                disciplina.Add(tempObjeto);
            }
            return disciplina;
        }
    }
}
