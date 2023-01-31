using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.TrilaApiDesafio.Domain.Models;
using TrilhaApiDesafio.TrilaApiDesafio.Infrastructure.Context;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("obter-todos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var model = _context.Tarefas.ToList();

            return Ok(model);
        }

        [HttpGet("obter-por-id/{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            // caso contrário retornar OK com a tarefa encontrada
            var model = _context.Tarefas.Find(id);

            if (id == null)
                return NotFound();

            return Ok(model);
        }

        [HttpGet("obter-por-titulo/{titulo}")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tituloTarefa = _context.Tarefas.Where(x => x.Titulo == titulo);

            return Ok(tituloTarefa);
        }

        [HttpGet("obter-por-data/{data}")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);

            return Ok(tarefa);
        }

        [HttpGet("obter-por-status/{status}")]
        public IActionResult ObterPorStatus(EStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Status == status);

            return Ok(tarefa);
        }

        [HttpPost("criar-tarefa")]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("atualizar-tarefa/{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _context.Entry(tarefaBanco).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(tarefaBanco);
        }

        [HttpDelete("remover-tarefa/{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Remove(id);
            _context.SaveChanges();

            return NoContent();
        }
    }
}