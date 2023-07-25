using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Controllers;
    
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly TodosService _todosService;

        public TodosController(TodosService todosService) =>
            _todosService = todosService;

        [HttpGet]
        public async Task<List<ToDoItem>> Get() =>
            await _todosService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<ToDoItem>> Get(string id)
        {
            var todo = await _todosService.GetAsync(id);

            if (todo is null)
            {
                return NotFound();
            }

            return todo;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ToDoItem newTodo)
        {
            await _todosService.CreateAsync(newTodo);

            return CreatedAtAction(nameof(Get), new { id = newTodo.Id }, newTodo);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ToDoItem updatedTodo)
        {
            var todo = await _todosService.GetAsync(updatedTodo.Id);

            if (todo is null)
            {
                return NotFound();
            }

            updatedTodo.Id = todo.Id;

            await _todosService.UpdateAsync(updatedTodo.Id, updatedTodo);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
        var todo = await _todosService.GetAsync(id);

            if (todo is null)
            {
                return NotFound();
            }

            await _todosService.RemoveAsync(id);

            return NoContent();
        }
    }

