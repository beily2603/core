
using Microsoft.AspNetCore.Mvc;
using lesson1.Models;
using lesson1.Interfaces;
using Microsoft.AspNetCore.Authorization;
using lesson1.Services;

namespace lesson1.Controllers;

[ApiController]
[Route("[controller]")]

public class TaskController : ControllerBase
{

private ToDoInterface<task> task;

public TaskController(ToDoInterface<task> task){
    this.task=task;

}
    [HttpGet]
    [Authorize(Policy = "User")]
    public IEnumerable<task> Get()
    {
       return task.GetAll().Where(job=>job.userId==TaskService.getId(Request.Headers.Authorization!));
    
    }
    [Authorize(Policy = "User")]
    [HttpGet("{id}")]
    public ActionResult<task> Get(int id)
    { 
        var t = task.Get(id);
            if (t == null||t.userId!=TaskService.getId(Request.Headers.Authorization!))
                return NotFound();
             return t;
      
    }
   
   
    [HttpPost]
    [Authorize(Policy = "User")]
    public ActionResult Post(task task){
          task.userId=TaskService.getId(Request.Headers.Authorization!);
          this.task.Add(task);
          return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
    }
       [HttpPut("{id}")]
       [Authorize(Policy = "User")]
        public ActionResult Put(int id, task newTask)
        {
            System.Console.WriteLine(id+"**************"+newTask.Name);
            task t=task.Get(id);
            System.Console.WriteLine(t.userId+"-----------");
              System.Console.WriteLine(TaskService.getId(Request.Headers.Authorization!));
            if(t==null||t.userId!=TaskService.getId(Request.Headers.Authorization!)){
                     System.Console.WriteLine("bad");
 return BadRequest();
            }
                          
                  if(!task.Update(id,newTask)) {
                      System.Console.WriteLine("bad");
                       return NotFound(); 
                  }
                      
                System.Console.WriteLine("find");
                return NoContent();
     }
         

     
        

        [HttpDelete("{id}")]
        [Authorize(Policy = "User")]
        public ActionResult Delete (int id)
        {
            if(task.GetAll().FirstOrDefault(t => t.Id == id)!.userId==TaskService.getId(Request.Headers.Authorization!))
            if (!task.Delete(id))
                return NotFound();
            return NoContent();            
        }

        
}
