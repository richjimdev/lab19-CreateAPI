using CreateAPI.Data;
using CreateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private TodoDbContext _context;

        public TodoListController(TodoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all todo lists
        /// </summary>
        /// <returns>Toodo lists</returns>
        public ActionResult<IEnumerable<TodoList>> Get()
        {
            return _context.TodoLists;
        }

        /// <summary>
        /// Gets a todo list
        /// </summary>
        /// <param name="id">id of todo list</param>
        /// <returns>Todo list</returns>
        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id)
        {
            var listTodos = _context.TodoLists.FirstOrDefault(x => x.ID == id);

            if (listTodos == null)
            {
                return NotFound();
            }

            listTodos.Todos = _context.Todos.Where(t => t.ID == listTodos.ID).ToList();

            return Ok(listTodos);
        }

        /// <summary>
        /// Adds todo list
        /// </summary>
        /// <param name="todoList">Todo list</param>
        /// <returns>Todolist</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodoList todoList)
        {
            await _context.TodoLists.AddAsync(todoList);
            await _context.SaveChangesAsync();

            foreach (var item in todoList.Todos)
            {
                item.ID = todoList.ID;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Get", new { id = todoList.ID });
        }

        /// <summary>
        /// Update a todo list
        /// </summary>
        /// <param name="id">Id of list</param>
        /// <param name="todoList">todolist</param>
        /// <returns>nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TodoList todoList)
        {
            var result = _context.TodoLists.FirstOrDefault(x => x.ID == id);

            if (result != null)
            {
                result.Title = todoList.Title;
                await _context.SaveChangesAsync();
            }
            else
            {
                await Post(todoList);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a list
        /// </summary>
        /// <param name="id">list id</param>
        /// <returns>nothing</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.TodoLists.FirstOrDefault(x => x.ID == id);

            if (result != null)
            {
                _context.TodoLists.Remove(result);
                _context.SaveChanges();
            }

            return NoContent();
        }
    }
}
