using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic; 
using System.Linq; 
using System; 
using TodoApi.Models; 
using System.Threading;
using System.Threading.Tasks; 
namespace TodoApi.Controllers {     
    [Route("api/[controller]")]     
    [ApiController]     
    public class TodoController : ControllerBase     
    {        
        private readonly TodoContext _context;          
        public TodoController(TodoContext context)         
        {             _context = context;              
            if (_context.TodoItems.Count() == 0)             
            {                 
             _context.TodoItems.Add(new TodoItem { Name = "Item1" });                 
             _context.SaveChanges();             
            }         
        }
        
        [HttpGet] 
        public ActionResult<List<TodoItem>> GetAll() 
        {     
            return _context.TodoItems.ToList(); 
        } 
        
        [HttpGet("{id}", Name = "GetTodo")] 
        public ActionResult<TodoItem> GetById(long id) 
        {    
            var item = _context.TodoItems.Find(id);     
            if (item == null)    
            {         
                return NotFound();     
            }     
            return item; 
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPost("/delete")]   
        public ActionResult DeleteTodoItem(long id) {
            Console.WriteLine("*****");
            var item = _context.TodoItems.Find(id); 
            _context.TodoItems.Remove(item);

         
            _context.SaveChanges();
            return NoContent();
        }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteTodo(long id)
        // {
        //     var todoItem = await _context.TodoItems.FindAsync(id);
        //     if (todoItem == null)
        //     {
        //         return NotFound();
        //     }
        //     _context.TodoItems.Remove(todoItem);
        //     await _context.SaveChangesAsync();
        //     return NoContent();
        // }

        // [HttpDelete("{id}", Name = "DeleteTodo")]
        // public async Task<ActionResult<string>> DeleteTodoItem(long id) {
        //     var item = _context.TodoItems.Find(id); 
        //     _context.TodoItems.Remove(item);

        //     await _context.SaveChangesAsync();
        //     return "Successfully deleted";
        // }
    } 
}