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
    public class AlunoAplicacao
    {
        private Contexto contexto;

        private void Inserir(Aluno aluno)
        {
            var strQuery = " ";
            strQuery += " INSERT INTO ALUNO (Nome, Mae, DataNascimento) ";
            strQuery += string.Format(" VALUES ('{0}','{1}', '{2}') ",
                aluno.Nome, aluno.Mae, aluno.DataNascimento);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        private void Alterar(Aluno aluno)
        {
            var strQuery = " ";
            strQuery += " UPDATE ALUNO SET ";
            strQuery += string.Format(" Nome = '{0}', ", aluno.Nome);
            strQuery += string.Format(" Mae = '{0}', ", aluno.Mae);
            strQuery += string.Format(" DataNascimento = '{0}' ", aluno.DataNascimento);
            strQuery += string.Format(" WHERE AlunoId = {0}", aluno.AlunoId);
            using (contexto = new Contexto())
            {
                contexto.ExecutaComando(strQuery);
            }
        }

        public void Salvar(Aluno aluno)
        {
            if (aluno.AlunoId > 0)
                Alterar(aluno);
            else
                Inserir(aluno);
        }

        public void Excluir(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = string.Format(" DELETE FROM ALUNO WHERE AlunoId = {0} ", id);
                contexto.ExecutaComando(strQuery);
            }
        }

        public List<Aluno> ListarTodos()
        {
            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM ALUNO ";
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retorno);
            }
        }

        public Aluno ListarPorId(int id)
        {
            using (contexto = new Contexto())
            {
                var strQuery = " SELECT * FROM ALUNO WHERE AlunoId = " + id;
                var retorno = contexto.ExecutaComandoComRetorno(strQuery);
                return TransformaReaderEmListaDeObjeto(retorno).FirstOrDefault();
            }
        }

        private List<Aluno> TransformaReaderEmListaDeObjeto(SqlDataReader reader)
        {
            var aluno = new List<Aluno>();
            while (reader.Read())
            {
                var tempObjeto = new Aluno()
                {
                    AlunoId = int.Parse(reader["AlunoId"].ToString()),
                    Nome = reader["Nome"].ToString(),
                    Mae = reader["Mae"].ToString(),
                    DataNascimento = DateTime.Parse(reader["DataNascimento"].ToString())
                    
                };
                aluno.Add(tempObjeto);
            }
            reader.Close();
            return aluno;
        }
    }
}
