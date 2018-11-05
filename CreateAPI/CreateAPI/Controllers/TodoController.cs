using CreateAPI.Data;
using CreateAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private TodoDbContext _context;

        public TodoController(TodoDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all todos
        /// </summary>
        /// <returns>Todos</returns>
        public ActionResult<IEnumerable<Todo>> Get()
        {
            return _context.Todos;
        }

        /// <summary>
        /// Gets one todo
        /// </summary>
        /// <param name="id">id of todo</param>
        /// <returns>Todo</returns>
        [HttpGet("{id}")]
        public ActionResult<Todo> Get(int id)
        {
            var todo = _context.Todos.FirstOrDefault(x => x.ID == id);
            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        /// <summary>
        /// Adds todo
        /// </summary>
        /// <param name="todo">Todo</param>
        /// <returns>The added todo</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Todo todo)
        {
            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return RedirectToAction("Get", new { id = todo.ID });
        }

        /// <summary>
        /// Updates todo
        /// </summary>
        /// <param name="id">Id of todo</param>
        /// <param name="todo">Updated todo</param>
        /// <returns>Updated todo</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Todo todo)
        {
            var result = _context.Todos.FirstOrDefault(x => x.ID == id);

            if (result != null)
            {
                _context.Todos.Update(todo);
            }
            else
            {
                await Post(todo);
            }

            return RedirectToAction("Get", new { id = todo.ID });
        }

        /// <summary>
        /// Deletes a todo
        /// </summary>
        /// <param name="id">Id of todo to delete</param>
        /// <returns>Ok</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _context.Todos.FirstOrDefault(x => x.ID == id);

            if (result != null)
            {
                _context.Todos.Remove(result);
            }

            return Ok();
        }

    }
}
