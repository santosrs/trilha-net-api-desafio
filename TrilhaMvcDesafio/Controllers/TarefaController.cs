using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrilhaMvcDesafio.Models;
using Microsoft.AspNetCore.Mvc;
using TrilhaMvcDesafio.Context;

namespace TrilhaMvcDesafio.Controllers
{
    
    public class TarefaController : Controller
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tarefa = _context.Tarefas.ToList();
            return View(tarefa); // Retorna a View com a lista de tarefas
        }

        // Exibe o formulário de criação
        
        public IActionResult Criar()
        {
            return View(); // Retorna a View do formulário de criação
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tarefa);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));

            }
            return View(tarefa);

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            
        }
         public IActionResult Detalhes(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

            public IActionResult Editar(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Editar(Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);

             if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Excluir(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(tarefa);

        }

        [HttpPost]
        public IActionResult Excluir(Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
             var tarefa = _context.Tarefas.Find(id);

        if (tarefa == null)
        {
            // Se não encontrar, exibe uma mensagem de erro
            ViewBag.Mensagem = "Tarefa não encontrada.";
            return View();
        }

        // Se encontrar, retorna a view com o modelo da tarefa
        return View(tarefa);
        }





        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
             var tarefa = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();

            return View(tarefa);   
            
    
        }

        
        public IActionResult ObterPorData(DateTime data, DateTime data1)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data >= data && x.Data <= data1).ToList();
            
            return View(tarefa);
        }

        
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Status == status).ToList();
            return View(tarefa);
        }
        
  
    }
}
