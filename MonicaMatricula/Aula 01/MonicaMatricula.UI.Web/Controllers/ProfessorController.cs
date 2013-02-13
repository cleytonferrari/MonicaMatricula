using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonicaMatricula.Aplicacao;
using MonicaMatricula.Dominio;

namespace MonicaMatricula.UI.Web.Controllers
{
    public class ProfessorController : Controller
    {

        public ActionResult Index()
        {
            var aplicacao = new ProfessorAplicacao();
            var lista = aplicacao.ListarTodos();
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            var aplicacao = new ProfessorAplicacao();
            var professor = aplicacao.ListarPorId(id);
            if (professor == null)
                return HttpNotFound();
            return View(professor);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Professor professor)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new ProfessorAplicacao();
                aplicacao.Salvar(professor);
                return RedirectToAction("Index");
            }

            return View(professor);
        }

        public ActionResult Editar(int id)
        {
            var aplicacao = new ProfessorAplicacao();
            var professor = aplicacao.ListarPorId(id);
            if (professor == null)
                return HttpNotFound();

            return View(professor);
        }

        [HttpPost]
        public ActionResult Editar(Professor professor)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new ProfessorAplicacao();
                aplicacao.Salvar(professor);
                return RedirectToAction("Index");
            }

            return View(professor);
        }

        public ActionResult Excluir(int id)
        {
            var aplicacao = new ProfessorAplicacao();
            var professor = aplicacao.ListarPorId(id);
            if (professor == null)
                return HttpNotFound();

            return View(professor);
        }

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmado(int id)
        {
            var aplicacao = new ProfessorAplicacao();
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}
