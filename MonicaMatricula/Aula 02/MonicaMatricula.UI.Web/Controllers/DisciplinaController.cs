using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonicaMatricula.Aplicacao;
using MonicaMatricula.Dominio;

namespace MonicaMatricula.UI.Web.Controllers
{
    public class DisciplinaController : Controller
    {

        public ActionResult Index()
        {
            var aplicacao = new DisciplinaAplicacao();
            var lista = aplicacao.ListarTodos();
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            var aplicacao = new DisciplinaAplicacao();
            var disciplina = aplicacao.ListarPorId(id);
            if (disciplina == null)
                return HttpNotFound();
            return View(disciplina);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new DisciplinaAplicacao();
                aplicacao.Salvar(disciplina);
                return RedirectToAction("Index");
            }

            return View(disciplina);
        }

        public ActionResult Editar(int id)
        {
            var aplicacao = new DisciplinaAplicacao();
            var disciplina = aplicacao.ListarPorId(id);
            if (disciplina == null)
                return HttpNotFound();

            return View(disciplina);
        }

        [HttpPost]
        public ActionResult Editar(Disciplina disciplina)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new DisciplinaAplicacao();
                aplicacao.Salvar(disciplina);
                return RedirectToAction("Index");
            }

            return View(disciplina);
        }

        public ActionResult Excluir(int id)
        {
            var aplicacao = new DisciplinaAplicacao();
            var disciplina = aplicacao.ListarPorId(id);
            if (disciplina == null)
                return HttpNotFound();

            return View(disciplina);
        }

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmado(int id)
        {
            var aplicacao = new DisciplinaAplicacao();
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}
