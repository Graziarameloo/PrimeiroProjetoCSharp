using Agendamento_de_Consulta.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Agendamento_de_Consulta.Models.Agendamento;



namespace Agendamento_de_Consulta.Controllers


{
    public class PessoaController : Controller


    {
        private readonly IPessoasDAL pes;
        public PessoaController(IPessoasDAL pessoas)
        {
            pes = pessoas;
        }
        public IActionResult Index()
        {
            List<PESSOAS> listaPESSOAS = new List<PESSOAS>();
            listaPESSOAS = pes.GetAllPESSOAS().ToList();

            return View(listaPESSOAS);
        }
        [HttpGet]
        public IActionResult details(int id)

        {
            if (id == null)
            {
                return NotFound();
            }

            PESSOAS pessoas = pes.GetPessoas(id);
            if (pessoas == null)
            {
                return NotFound();
            }
            return View(pessoas);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult create([Bind] PESSOAS pessoas)
        {
            if (ModelState.IsValid)

            {
                pessoas.End.IdEndereco = pes.AddEndereco(pessoas);
                pessoas.Tel.IdTel = pes.AddTelefone(pessoas);
                pessoas.Id = pes.AddPessoas(pessoas);

                pes.AddPessoaTelefone(pessoas.Id, pessoas.Tel.IdTel);
                return RedirectToAction("index");
            }

            //retornar a view com o modelo de PESSOAS para exibir os erros de validação
            return View(pessoas);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }


            PESSOAS pessoa = pes.GetPessoas(id);

            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id, PESSOAS pessoas)
        {
            if (id != pessoas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                pessoas.Id = pes.UpdatePessoas(pessoas);
                pessoas.End.IdEndereco = pes.UpdateEndereco(pessoas);
                pessoas.Tel.IdTel = pes.UpdateTelefone(pessoas);


                pes.UpdatePessoaTelefone(pessoas.Id, pessoas.Tel.IdTel);
                return RedirectToAction("index");
            }

            // Retornar a View com o objeto PESSOAS e o ModelState inválido
            return View(pessoas);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }


            PESSOAS pessoas = pes.GetPessoas(id);

            if (pessoas == null)
            {
                return NotFound();
            }
            return View(pessoas);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id, PESSOAS pessoas)

        {
            pes.DeletePessoaTelefone(pessoas.Id, pessoas.Tel.IdTel); ;
            pes.DeletePessoas(id);
            pes.DeleteEndereco(pessoas.End.IdEndereco);
            pes.DeleteTelefone(pessoas.Tel.IdTel);

            return RedirectToAction("index");
        }
    }
}
