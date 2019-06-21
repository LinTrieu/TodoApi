using System; 
using System.Collections.Generic; 
using System.Linq; 
using TodoApi.Models; 
using System.Threading;
using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc;
// using Newtonsoft.Json;

namespace TodoApi.Controllers {     
    [Route("api/[controller]")]     
    [ApiController]     
    public class TodoController : Controller     
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
        // public ActionResult<List<TodoItem>> Index() 
        // GetAllTodos method
        [HttpGet]
        public IActionResult Index() 
        {    
            List<TodoItem> todoitems = _context.TodoItems.ToList();
            return View(todoitems);
        }
        
        // GetById Method
        // [HttpGet("{id}", Name = "GetTodo")] 
        // public ActionResult<TodoItem> GetById(long id) 
        // {    
        //     var item = _context.TodoItems.Find(id);     
        //     if (item == null)    
        //     {         
        //         return NotFound();     
        //     }

        //     ViewData["Todo"] = item.Name;
        //     return View(); 
        // }
        
        
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

        [HttpPut("{id}", Name = "UpdateTodo")]

        public async Task<ActionResult<TodoItem>> UpdateTodo(long id, string name) 
        {
            var item = _context.TodoItems.Find(id);
            item.Name = name;

            await _context.SaveChangesAsync();

            return item;
        }

        [HttpDelete("{id}", Name = "DeleteTodo")]
        public async Task<ActionResult<string>> DeleteTodoItem(long id) {
            var item = _context.TodoItems.Find(id); 
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
            return "Successfully deleted";
        }
    } 
}