using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonicaMatricula.Aplicacao;
using MonicaMatricula.Dominio;

namespace MonicaMatricula.UI.Web.Controllers
{
    public class AlunoController : Controller
    {
        
        public ActionResult Index()
        {
            var aplicacao = new AlunoAplicacao();
            var lista = aplicacao.ListarTodos();
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            var aplicacao = new AlunoAplicacao();
            var aluno = aplicacao.ListarPorId(id);
            if (aluno == null)
                return HttpNotFound();
            return View(aluno);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new AlunoAplicacao();
                aplicacao.Salvar(aluno);
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        public ActionResult Editar(int id)
        {
            var aplicacao = new AlunoAplicacao();
            var aluno = aplicacao.ListarPorId(id);
            if (aluno == null)
                return HttpNotFound();

            return View(aluno);
        }

        [HttpPost]
        public ActionResult Editar(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new AlunoAplicacao();
                aplicacao.Salvar(aluno);
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        public ActionResult Excluir(int id)
        {
            var aplicacao = new AlunoAplicacao();
            var aluno = aplicacao.ListarPorId(id);
            if (aluno == null)
                return HttpNotFound();

            return View(aluno);
        }

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmado(int id)
        {
            var aplicacao = new AlunoAplicacao();
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        }
    }
}
