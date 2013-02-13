using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonicaMatricula.Aplicacao;
using MonicaMatricula.Dominio;
using MonicaMatricula.Dominio.ViewModel;

namespace MonicaMatricula.UI.Web.Controllers
{
    public class ProfessorDisciplinaController : Controller
    {
        //
        // GET: /ProfessorDisciplina/

        public ActionResult Index()
        {
            var aplicacao = new ProfessorDisciplinaAplicacao();
            var lista = aplicacao.ListarTodos();
            return View(lista);
        }

        public ActionResult Editar(int id)
        {
            var aplicacao = new ProfessorDisciplinaAplicacao();
            var professor = aplicacao.ListarPorId(id);
            if (professor == null)
                return HttpNotFound();

            ViewBag.ListaProfessorDisciplina = PreencherDisciplinaDoProfessor(professor);

            return View(professor);
        }

        [HttpPost]
        public ActionResult Editar(Professor professor, int[] disciplinaSelecionadas)
        {
            if (ModelState.IsValid)
            {
                professor.Disciplinas = new Collection<Disciplina>();
                foreach (var permissaoSelecionada in disciplinaSelecionadas)
                    professor.Disciplinas.Add(new Disciplina { DisciplinaId = permissaoSelecionada });

                var aplicacao = new ProfessorDisciplinaAplicacao();
                aplicacao.Salvar(professor);
                return RedirectToAction("Index");
            }
            ViewBag.ListaProfessorDisciplina = PreencherDisciplinaDoProfessor(professor);
            return View(professor);
        }

        private List<ProfessorDisciplinaViewModel> PreencherDisciplinaDoProfessor(Professor professor)
        {
            var todasAsDisciplinas = new DisciplinaAplicacao().ListarTodos();
            var professorDisciplinasId = new HashSet<int>(professor.Disciplinas.Select(c => c.DisciplinaId));
            var viewModel = new List<ProfessorDisciplinaViewModel>();
            foreach (var disciplina in todasAsDisciplinas)
            {
                viewModel.Add(new ProfessorDisciplinaViewModel()
                {
                    Disciplina = disciplina,
                    Associado = professorDisciplinasId.Contains(disciplina.DisciplinaId)
                });
            }
            return viewModel;
        }
    }
}
