using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonicaMatricula.Aplicacao;
using MonicaMatricula.Dominio;

namespace MonicaMatricula.UI.Web.Controllers
{
    public class CursoController : Controller
    {

        public ActionResult Index()
        {
            var aplicacao = new CursoAplicacao();
            var lista = aplicacao.ListarTodos();
            return View(lista);
        }

        public ActionResult Detalhes(int id)
        {
            var aplicacao = new CursoAplicacao();
            var curso = aplicacao.ListarPorId(id);
            if (curso == null)
                return HttpNotFound();
            return View(curso);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(Curso curso)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new CursoAplicacao();
                aplicacao.Salvar(curso);
                return RedirectToAction("Index");
            }

            return View(curso);
        }

        public ActionResult Editar(int id)
        {
            var aplicacao = new CursoAplicacao();
            var curso = aplicacao.ListarPorId(id);
            if (curso == null)
                return HttpNotFound();

            return View(curso);
        }

        [HttpPost]
        public ActionResult Editar(Curso curso)
        {
            if (ModelState.IsValid)
            {
                var aplicacao = new CursoAplicacao();
                aplicacao.Salvar(curso);
                return RedirectToAction("Index");
            }

            return View(curso);
        }

        public ActionResult Excluir(int id)
        {
            var aplicacao = new CursoAplicacao();
            var curso = aplicacao.ListarPorId(id);
            if (curso == null)
                return HttpNotFound();

            return View(curso);
        }

        [HttpPost, ActionName("Excluir")]
        public ActionResult ExcluirConfirmado(int id)
        {
            var aplicacao = new CursoAplicacao();
            aplicacao.Excluir(id);
            return RedirectToAction("Index");
        }

    }
}
